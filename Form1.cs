using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace CoinCounterApp
{
    // VISUAL LOGIC: shows the images, calls CoinCounter, shows the results
    public partial class Form1 : Form
    {
        private Bitmap _source;
        private ScanResult _result;

        private static readonly CoinType[] Order =
            { CoinType.Sentimo5, CoinType.Sentimo10, CoinType.Sentimo25, CoinType.Peso1, CoinType.Peso5 };

        public Form1()
        {
            InitializeComponent();
        }

        // load the provided image right away
        private void Form1_Load(object sender, EventArgs e)
        {
            SetSource(new Bitmap(Properties.Resources.CoinImage));
        }

        // shows a new source image on the left and clears the previous results
        // (this was originally for supporting a "load image" button)
        private void SetSource(Bitmap bmp)
        {
            Image oldSource = picOriginal.Image;
            Image oldProcessed = picProcessed.Image;

            _source = bmp;
            _result = null;
            picOriginal.Image = _source;
            picProcessed.Image = null;
            lvResults.Items.Clear();
            lblTotal.Text = "Image loaded. Click \"Count Coins\".";
            btnCount.Enabled = true;

            if (oldSource != null) oldSource.Dispose();
            if (oldProcessed != null) oldProcessed.Dispose();
        }

        private void btnCount_Click(object sender, EventArgs e)
        {
            if (_source == null) return;

            Cursor = Cursors.WaitCursor;
            try
            {
                _result = CoinCounter.Analyze(_source);
                ShowResults();
            }
            finally { Cursor = Cursors.Default; }
        }

        private void chkShowDiameter_CheckedChanged(object sender, EventArgs e)
        {
            if (_result != null) ShowResults();
        }

        // fills the list, the total label, and the processed (right) picture
        private void ShowResults()
        {
            lvResults.Items.Clear();
            foreach (CoinType t in Order)
            {
                int n = _result.Count(t);
                ListViewItem item = new ListViewItem(CoinCounter.NameOf(t));
                item.SubItems.Add(n.ToString());
                item.SubItems.Add((n * CoinCounter.ValueOf(t)).ToString("N2"));
                lvResults.Items.Add(item);
            }

            ListViewItem totalRow = new ListViewItem("TOTAL");
            totalRow.SubItems.Add(_result.Coins.Count.ToString());
            totalRow.SubItems.Add(_result.Total.ToString("N2"));
            totalRow.Font = new Font(lvResults.Font, FontStyle.Bold);
            lvResults.Items.Add(totalRow);

            lblTotal.Text = "Coins: " + _result.Coins.Count + Environment.NewLine +
                            "Total: \u20B1" + _result.Total.ToString("N2");

            Image old = picProcessed.Image;
            picProcessed.Image = Annotate(_source, _result, chkShowDiameter.Checked);
            if (old != null) old.Dispose();
        }

        // draws a circle + label on every detected coin (on a copy of the source)
        private Bitmap Annotate(Bitmap src, ScanResult res, bool showDiameter)
        {
            Bitmap canvas = new Bitmap(src.Width, src.Height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(canvas))
            {
                g.DrawImage(src, 0, 0, src.Width, src.Height);
                g.SmoothingMode = SmoothingMode.AntiAlias;

                float fontPx = Math.Max(10f, src.Width / 55f);
                using (Font font = new Font("Segoe UI", fontPx, FontStyle.Bold, GraphicsUnit.Pixel))
                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    foreach (CoinInfo c in res.Coins)
                    {
                        Color col = ColorFor(c.Type);
                        float r = (float)c.Diameter / 2f;
                        using (Pen pen = new Pen(col, 3f))
                            g.DrawEllipse(pen, c.Center.X - r, c.Center.Y - r, 2 * r, 2 * r);

                        string text = ShortLabel(c.Type);
                        if (showDiameter) text += " (" + (int)c.Diameter + ")";

                        SizeF size = g.MeasureString(text, font);
                        RectangleF box = new RectangleF(c.Center.X - size.Width / 2, c.Center.Y - size.Height / 2,
                                                        size.Width, size.Height);
                        using (Brush bg = new SolidBrush(Color.FromArgb(200, Color.White)))
                            g.FillRectangle(bg, box);
                        using (Brush fg = new SolidBrush(col))
                            g.DrawString(text, font, fg, c.Center, sf);
                    }
                }
            }
            return canvas;
        }

        private static Color ColorFor(CoinType t)
        {
            switch (t)
            {
                case CoinType.Sentimo5: return Color.Crimson;
                case CoinType.Sentimo10: return Color.DarkOrange;
                case CoinType.Sentimo25: return Color.ForestGreen;
                case CoinType.Peso1: return Color.RoyalBlue;
                default: return Color.Purple;
            }
        }

        private static string ShortLabel(CoinType t)
        {
            switch (t)
            {
                case CoinType.Sentimo5: return "5c";
                case CoinType.Sentimo10: return "10c";
                case CoinType.Sentimo25: return "25c";
                case CoinType.Peso1: return "P1";
                default: return "P5";
            }
        }
    }
}
