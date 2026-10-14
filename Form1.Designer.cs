namespace CoinCounterApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.tblImages = new System.Windows.Forms.TableLayoutPanel();
            this.lblOriginal = new System.Windows.Forms.Label();
            this.lblProcessed = new System.Windows.Forms.Label();
            this.picOriginal = new System.Windows.Forms.PictureBox();
            this.picProcessed = new System.Windows.Forms.PictureBox();
            this.pnlSide = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lvResults = new System.Windows.Forms.ListView();
            this.colDenom = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colSubtotal = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chkShowDiameter = new System.Windows.Forms.CheckBox();
            this.btnCount = new System.Windows.Forms.Button();
            this.tblImages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).BeginInit();
            this.pnlSide.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblImages
            // 
            this.tblImages.ColumnCount = 2;
            this.tblImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblImages.Controls.Add(this.lblOriginal, 0, 0);
            this.tblImages.Controls.Add(this.lblProcessed, 1, 0);
            this.tblImages.Controls.Add(this.picOriginal, 0, 1);
            this.tblImages.Controls.Add(this.picProcessed, 1, 1);
            this.tblImages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblImages.Location = new System.Drawing.Point(0, 0);
            this.tblImages.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tblImages.Name = "tblImages";
            this.tblImages.RowCount = 2;
            this.tblImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tblImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblImages.Size = new System.Drawing.Size(807, 618);
            this.tblImages.TabIndex = 0;
            // 
            // lblOriginal
            // 
            this.lblOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblOriginal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblOriginal.Location = new System.Drawing.Point(2, 0);
            this.lblOriginal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblOriginal.Name = "lblOriginal";
            this.lblOriginal.Size = new System.Drawing.Size(399, 26);
            this.lblOriginal.TabIndex = 0;
            this.lblOriginal.Text = "Original";
            this.lblOriginal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProcessed
            // 
            this.lblProcessed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblProcessed.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblProcessed.Location = new System.Drawing.Point(405, 0);
            this.lblProcessed.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblProcessed.Name = "lblProcessed";
            this.lblProcessed.Size = new System.Drawing.Size(400, 26);
            this.lblProcessed.TabIndex = 1;
            this.lblProcessed.Text = "Processed";
            this.lblProcessed.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picOriginal
            // 
            this.picOriginal.BackColor = System.Drawing.Color.White;
            this.picOriginal.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picOriginal.Location = new System.Drawing.Point(2, 28);
            this.picOriginal.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picOriginal.Name = "picOriginal";
            this.picOriginal.Size = new System.Drawing.Size(399, 588);
            this.picOriginal.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picOriginal.TabIndex = 2;
            this.picOriginal.TabStop = false;
            // 
            // picProcessed
            // 
            this.picProcessed.BackColor = System.Drawing.Color.White;
            this.picProcessed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picProcessed.Location = new System.Drawing.Point(405, 28);
            this.picProcessed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picProcessed.Name = "picProcessed";
            this.picProcessed.Size = new System.Drawing.Size(400, 588);
            this.picProcessed.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProcessed.TabIndex = 3;
            this.picProcessed.TabStop = false;
            // 
            // pnlSide
            // 
            this.pnlSide.Controls.Add(this.lblTotal);
            this.pnlSide.Controls.Add(this.lvResults);
            this.pnlSide.Controls.Add(this.chkShowDiameter);
            this.pnlSide.Controls.Add(this.btnCount);
            this.pnlSide.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSide.Location = new System.Drawing.Point(807, 0);
            this.pnlSide.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlSide.Name = "pnlSide";
            this.pnlSide.Size = new System.Drawing.Size(243, 618);
            this.pnlSide.TabIndex = 1;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTotal.Location = new System.Drawing.Point(9, 246);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(225, 73);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "Loading...";
            // 
            // lvResults
            // 
            this.lvResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colDenom,
            this.colCount,
            this.colSubtotal});
            this.lvResults.FullRowSelect = true;
            this.lvResults.GridLines = true;
            this.lvResults.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvResults.HideSelection = false;
            this.lvResults.Location = new System.Drawing.Point(9, 71);
            this.lvResults.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.lvResults.MultiSelect = false;
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(226, 163);
            this.lvResults.TabIndex = 1;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // colDenom
            // 
            this.colDenom.Text = "Denomination";
            this.colDenom.Width = 125;
            // 
            // colCount
            // 
            this.colCount.Text = "Count";
            this.colCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colCount.Width = 70;
            // 
            // colSubtotal
            // 
            this.colSubtotal.Text = "Subtotal (P)";
            this.colSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.colSubtotal.Width = 90;
            // 
            // chkShowDiameter
            // 
            this.chkShowDiameter.AutoSize = true;
            this.chkShowDiameter.Location = new System.Drawing.Point(10, 43);
            this.chkShowDiameter.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.chkShowDiameter.Name = "chkShowDiameter";
            this.chkShowDiameter.Size = new System.Drawing.Size(170, 17);
            this.chkShowDiameter.TabIndex = 2;
            this.chkShowDiameter.Text = "Show measured diameters (px)";
            this.chkShowDiameter.UseVisualStyleBackColor = true;
            this.chkShowDiameter.CheckedChanged += new System.EventHandler(this.chkShowDiameter_CheckedChanged);
            // 
            // btnCount
            // 
            this.btnCount.Enabled = false;
            this.btnCount.Location = new System.Drawing.Point(9, 3);
            this.btnCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnCount.Name = "btnCount";
            this.btnCount.Size = new System.Drawing.Size(225, 31);
            this.btnCount.TabIndex = 3;
            this.btnCount.Text = "Count Coins";
            this.btnCount.UseVisualStyleBackColor = true;
            this.btnCount.Click += new System.EventHandler(this.btnCount_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1050, 618);
            this.Controls.Add(this.tblImages);
            this.Controls.Add(this.pnlSide);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MinimumSize = new System.Drawing.Size(754, 495);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Philippine Coin Counter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tblImages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picOriginal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picProcessed)).EndInit();
            this.pnlSide.ResumeLayout(false);
            this.pnlSide.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.TableLayoutPanel tblImages;
        private System.Windows.Forms.Label lblOriginal;
        private System.Windows.Forms.Label lblProcessed;
        private System.Windows.Forms.PictureBox picOriginal;
        private System.Windows.Forms.PictureBox picProcessed;
        private System.Windows.Forms.Panel pnlSide;
        private System.Windows.Forms.Button btnCount;
        private System.Windows.Forms.CheckBox chkShowDiameter;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader colDenom;
        private System.Windows.Forms.ColumnHeader colCount;
        private System.Windows.Forms.ColumnHeader colSubtotal;
        private System.Windows.Forms.Label lblTotal;
    }
}
