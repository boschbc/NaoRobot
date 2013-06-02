namespace Naovigate.GUI.Util
{
    partial class IPChooser
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ip2 = new System.Windows.Forms.NumericUpDown();
            this.ip3 = new System.Windows.Forms.NumericUpDown();
            this.ip4 = new System.Windows.Forms.NumericUpDown();
            this.ip1 = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ip2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip1)).BeginInit();
            this.SuspendLayout();
            // 
            // ip2
            // 
            this.ip2.Location = new System.Drawing.Point(61, 6);
            this.ip2.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ip2.Name = "ip2";
            this.ip2.Size = new System.Drawing.Size(50, 20);
            this.ip2.TabIndex = 1;
            this.ip2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ip3
            // 
            this.ip3.Location = new System.Drawing.Point(117, 6);
            this.ip3.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ip3.Name = "ip3";
            this.ip3.Size = new System.Drawing.Size(50, 20);
            this.ip3.TabIndex = 2;
            this.ip3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ip4
            // 
            this.ip4.Location = new System.Drawing.Point(173, 6);
            this.ip4.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ip4.Name = "ip4";
            this.ip4.Size = new System.Drawing.Size(50, 20);
            this.ip4.TabIndex = 3;
            this.ip4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip4.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // ip1
            // 
            this.ip1.Location = new System.Drawing.Point(5, 6);
            this.ip1.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ip1.Name = "ip1";
            this.ip1.Size = new System.Drawing.Size(50, 20);
            this.ip1.TabIndex = 0;
            this.ip1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ip1.Value = new decimal(new int[] {
            127,
            0,
            0,
            0});
            // 
            // IPChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.ip2);
            this.Controls.Add(this.ip3);
            this.Controls.Add(this.ip4);
            this.Controls.Add(this.ip1);
            this.Name = "IPChooser";
            this.Size = new System.Drawing.Size(226, 29);
            ((System.ComponentModel.ISupportInitialize)(this.ip2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ip1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown ip2;
        private System.Windows.Forms.NumericUpDown ip3;
        private System.Windows.Forms.NumericUpDown ip4;
        private System.Windows.Forms.NumericUpDown ip1;
    }
}
