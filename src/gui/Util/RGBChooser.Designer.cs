namespace Naovigate.GUI.Util
{
    partial class RGBChooser
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
            this.red = new System.Windows.Forms.NumericUpDown();
            this.green = new System.Windows.Forms.NumericUpDown();
            this.blue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.red)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue)).BeginInit();
            this.SuspendLayout();
            // 
            // red
            // 
            this.red.BackColor = System.Drawing.Color.White;
            this.red.ForeColor = System.Drawing.Color.Red;
            this.red.Location = new System.Drawing.Point(3, 3);
            this.red.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.red.Name = "red";
            this.red.Size = new System.Drawing.Size(41, 20);
            this.red.TabIndex = 0;
            this.red.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // green
            // 
            this.green.BackColor = System.Drawing.Color.White;
            this.green.ForeColor = System.Drawing.Color.Green;
            this.green.Location = new System.Drawing.Point(50, 3);
            this.green.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.green.Name = "green";
            this.green.Size = new System.Drawing.Size(41, 20);
            this.green.TabIndex = 1;
            this.green.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // blue
            // 
            this.blue.BackColor = System.Drawing.Color.White;
            this.blue.ForeColor = System.Drawing.Color.Blue;
            this.blue.Location = new System.Drawing.Point(97, 3);
            this.blue.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.blue.Name = "blue";
            this.blue.Size = new System.Drawing.Size(41, 20);
            this.blue.TabIndex = 2;
            this.blue.Value = new decimal(new int[] {
            255,
            0,
            0,
            0});
            // 
            // RGBChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.blue);
            this.Controls.Add(this.green);
            this.Controls.Add(this.red);
            this.Name = "RGBChooser";
            this.Size = new System.Drawing.Size(141, 26);
            ((System.ComponentModel.ISupportInitialize)(this.red)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown red;
        private System.Windows.Forms.NumericUpDown green;
        private System.Windows.Forms.NumericUpDown blue;

    }
}
