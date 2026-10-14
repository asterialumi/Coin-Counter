using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace CoinCounterApp
{
    public enum CoinType { Sentimo5, Sentimo10, Sentimo25, Peso1, Peso5 }

    // one detected coin
    public class CoinInfo
    {
        public CoinType Type;
        public double Diameter; // in pixels, hole filled in
        public PointF Center;
        public bool HasHole;
    }

    public class ScanResult
    {
        public List<CoinInfo> Coins = new List<CoinInfo>();

        public int Count(CoinType type)
        {
            int n = 0;
            foreach (CoinInfo c in Coins)
                if (c.Type == type) n++;
            return n;
        }

        public decimal Total
        {
            get
            {
                decimal sum = 0;
                foreach (CoinInfo c in Coins) sum += CoinCounter.ValueOf(c.Type);
                return sum;
            }
        }
    }

    // IMAGE-PROCESSING LOGIC: grayscale -> otsu's threshold -> compute blobs
    public static class CoinCounter
    {
        // constants for the coin sizes
        private const double ReferenceWidth = 776.0;
        private const double MinCoinArea = 1200;      // blobs smaller than this are noise
        private const double HoleMaxDiameter = 61;    // only the smallest coins can be the 5 sentimo
        private const double Diam10Max = 69;          // < 69  -> 10 sentimo
        private const double Diam25Max = 82;          // < 82  -> 25 sentimo
        private const double Diam1Max = 94;           // < 94  -> 1 peso, else 5 peso

        public static decimal ValueOf(CoinType t)
        {
            switch (t)
            {
                case CoinType.Sentimo5: return 0.05m;
                case CoinType.Sentimo10: return 0.10m;
                case CoinType.Sentimo25: return 0.25m;
                case CoinType.Peso1: return 1m;
                default: return 5m;
            }
        }

        public static string NameOf(CoinType t)
        {
            switch (t)
            {
                case CoinType.Sentimo5: return "5 sentimo";
                case CoinType.Sentimo10: return "10 sentimo";
                case CoinType.Sentimo25: return "25 sentimo";
                case CoinType.Peso1: return "1 peso";
                default: return "5 peso";
            }
        }

        // main logic for image processing
        public static ScanResult Analyze(Bitmap bmp)
        {
            int w = bmp.Width, h = bmp.Height;
            byte[] gray = ToGrayscale(bmp);
            int thr = OtsuThreshold(gray);
            int bgLevel = BackgroundLevel(gray, thr);
            double scale = w / ReferenceWidth;

            int[] labels = new int[w * h];   // 0 = not visited
            int[] stack = new int[w * h];
            ScanResult result = new ScanResult();
            int label = 0;

            for (int y = 0; y < h; y++)
            {
                for (int x = 0; x < w; x++)
                {
                    int start = y * w + x;
                    if (gray[start] > thr || labels[start] != 0) continue;

                    label++;
                    Blob blob = FloodFill(gray, thr, labels, stack, w, h, start, label);
                    if (blob.Area < MinCoinArea * scale * scale) continue;

                    result.Coins.Add(BuildCoin(blob, gray, labels, w, h, bgLevel, scale));
                }
            }
            return result;
        }

        // STEP 1: Grayscale
        // convert rgb pixels to brightness
        private static byte[] ToGrayscale(Bitmap bmp)
        {
            int w = bmp.Width, h = bmp.Height;
            byte[] gray = new byte[w * h];
            BitmapData data = bmp.LockBits(new Rectangle(0, 0, w, h),
                ImageLockMode.ReadOnly, PixelFormat.Format24bppRgb);
            try
            {
                byte[] row = new byte[Math.Abs(data.Stride)];
                for (int y = 0; y < h; y++)
                {
                    System.Runtime.InteropServices.Marshal.Copy(
                        new IntPtr(data.Scan0.ToInt64() + (long)y * data.Stride), row, 0, w * 3);
                    for (int x = 0; x < w; x++)
                    {
                        int b = row[x * 3], g = row[x * 3 + 1], r = row[x * 3 + 2];
                        gray[y * w + x] = (byte)(0.299 * r + 0.587 * g + 0.114 * b);
                    }
                }
            }
            finally { bmp.UnlockBits(data); }
            return gray;
        }

        // STEP 2: Otsu's Threshold
        // build a brightness histogram, test threshold values from 0-255,
        // and choose the threshold that maximizes between-class variance
        private static int OtsuThreshold(byte[] gray)
        {
            int[] hist = new int[256];
            foreach (byte g in gray) hist[g]++;

            long total = gray.Length;
            double sumAll = 0;
            for (int i = 0; i < 256; i++) sumAll += i * (double)hist[i];

            double sumB = 0, best = -1;
            long wB = 0;
            int threshold = 128;
            for (int t = 0; t < 256; t++)
            {
                wB += hist[t];
                if (wB == 0) continue;
                long wF = total - wB;
                if (wF == 0) break;

                sumB += t * (double)hist[t];
                double mB = sumB / wB;
                double mF = (sumAll - sumB) / wF;
                double between = (double)wB * wF * (mB - mF) * (mB - mF);
                if (between > best) { best = between; threshold = t; }
            }
            return threshold;
        }

        // (most common brightness above the threshold = the paper background)
        private static int BackgroundLevel(byte[] gray, int thr)
        {
            int[] hist = new int[256];
            foreach (byte g in gray) hist[g]++;
            int bestBin = 255;
            for (int i = thr + 1; i < 256; i++)
                if (hist[i] > hist[bestBin] || bestBin <= thr) bestBin = i;
            return bestBin;
        }

        // STEP 3: connected components
        // run through each pixel, and perform a 4-neighbor flood fill on unvisited coin pixels
        // record as "Blob", and ignore blobs smaller than MinCoinArea
        private class Blob
        {
            public int Label, Area;
            public int MinX = int.MaxValue, MinY = int.MaxValue, MaxX = -1, MaxY = -1;
        }

        private static Blob FloodFill(byte[] gray, int thr, int[] labels, int[] stack,
                                      int w, int h, int start, int label)
        {
            Blob b = new Blob();
            b.Label = label;
            int sp = 0;
            stack[sp++] = start;
            labels[start] = label;

            while (sp > 0)
            {
                int p = stack[--sp];
                int px = p % w, py = p / w;
                b.Area++;
                if (px < b.MinX) b.MinX = px;
                if (px > b.MaxX) b.MaxX = px;
                if (py < b.MinY) b.MinY = py;
                if (py > b.MaxY) b.MaxY = py;

                if (px > 0 && labels[p - 1] == 0 && gray[p - 1] <= thr) { labels[p - 1] = label; stack[sp++] = p - 1; }
                if (px < w - 1 && labels[p + 1] == 0 && gray[p + 1] <= thr) { labels[p + 1] = label; stack[sp++] = p + 1; }
                if (py > 0 && labels[p - w] == 0 && gray[p - w] <= thr) { labels[p - w] = label; stack[sp++] = p - w; }
                if (py < h - 1 && labels[p + w] == 0 && gray[p + w] <= thr) { labels[p + w] = label; stack[sp++] = p + w; }
            }
            return b;
        }

        // STEP 4: measure the blob and decide the denomination
        private static CoinInfo BuildCoin(Blob b, byte[] gray, int[] labels,
                                          int w, int h, int bgLevel, double scale)
        {
            // padded bounding box
            // flood the outside so anything left over is a hole
            int ox = b.MinX - 1, oy = b.MinY - 1;
            int rw = b.MaxX - b.MinX + 3, rh = b.MaxY - b.MinY + 3;
            bool[] seen = new bool[rw * rh];
            int[] st = new int[rw * rh];
            int sp = 0;
            st[sp++] = 0;
            seen[0] = true;

            while (sp > 0)
            {
                int p = st[--sp];
                int rx = p % rw, ry = p / rw;
                for (int d = 0; d < 4; d++)
                {
                    int nx = rx + (d == 0 ? -1 : d == 1 ? 1 : 0);
                    int ny = ry + (d == 2 ? -1 : d == 3 ? 1 : 0);
                    if (nx < 0 || ny < 0 || nx >= rw || ny >= rh) continue;
                    int np = ny * rw + nx;
                    if (seen[np] || IsCoinPixel(labels, w, h, ox + nx, oy + ny, b.Label)) continue;
                    seen[np] = true;
                    st[sp++] = np;
                }
            }

            int holeArea = 0;
            double holeGray = 0, holeX = 0, holeY = 0;
            for (int ry = 0; ry < rh; ry++)
            {
                for (int rx = 0; rx < rw; rx++)
                {
                    if (seen[ry * rw + rx] || IsCoinPixel(labels, w, h, ox + rx, oy + ry, b.Label)) continue;
                    holeArea++;
                    holeGray += gray[(oy + ry) * w + (ox + rx)];
                    holeX += ox + rx;
                    holeY += oy + ry;
                }
            }

            double filledArea = b.Area + holeArea;
            double diameter = 2.0 * Math.Sqrt(filledArea / Math.PI);
            float cx = (b.MinX + b.MaxX) / 2f;
            float cy = (b.MinY + b.MaxY) / 2f;

            // a real hole: small coin, 3-20% of area, bright like the paper, near the centre
            // if a real hole is found, it's Sentimo5
            bool hasHole = false;
            if (holeArea > 0 && diameter < HoleMaxDiameter * scale)
            {
                double ratio = holeArea / filledArea;
                double meanGray = holeGray / holeArea;
                double off = Math.Sqrt(Math.Pow(holeX / holeArea - cx, 2) + Math.Pow(holeY / holeArea - cy, 2));
                hasHole = ratio > 0.03 && ratio < 0.20
                          && meanGray >= bgLevel - 40
                          && off < 0.25 * (diameter / 2.0);
            }

            double dNorm = diameter / scale;
            CoinType type;
            if (hasHole) type = CoinType.Sentimo5;
            else if (dNorm < Diam10Max) type = CoinType.Sentimo10;
            else if (dNorm < Diam25Max) type = CoinType.Sentimo25;
            else if (dNorm < Diam1Max) type = CoinType.Peso1;
            else type = CoinType.Peso5;

            CoinInfo coin = new CoinInfo();
            coin.Type = type;
            coin.Diameter = diameter;
            coin.Center = new PointF(cx, cy);
            coin.HasHole = hasHole;
            return coin;
        }

        private static bool IsCoinPixel(int[] labels, int w, int h, int x, int y, int label)
        {
            if (x < 0 || y < 0 || x >= w || y >= h) return false;
            return labels[y * w + x] == label;
        }
    }
}
