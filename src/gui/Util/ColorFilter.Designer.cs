namespace Naovigate.GUI.Util
{
    partial class ColorFilter
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
            this.maxColorLabel = new System.Windows.Forms.Label();
            this.minColorLabel = new System.Windows.Forms.Label();
            this.maxRGB = new Naovigate.GUI.Util.RGBChooser();
            this.minRGB = new Naovigate.GUI.Util.RGBChooser();
            this.SuspendLayout();
            // 
            // maxColorLabel
            // 
            this.maxColorLabel.AutoSize = true;
            this.maxColorLabel.Location = new System.Drawing.Point(3, 35);
            this.maxColorLabel.Name = "maxColorLabel";
            this.maxColorLabel.Size = new System.Drawing.Size(30, 13);
            this.maxColorLabel.TabIndex = 2;
            this.maxColorLabel.Text = "Max:";
            // 
            // minColorLabel
            // 
            this.minColorLabel.AutoSize = true;
            this.minColorLabel.Location = new System.Drawing.Point(3, 3);
            this.minColorLabel.Name = "minColorLabel";
            this.minColorLabel.Size = new System.Drawing.Size(27, 13);
            this.minColorLabel.TabIndex = 3;
            this.minColorLabel.Text = "Min:";
            // 
            // maxRGB
            // 
            this.maxRGB.AutoSize = true;
            this.maxRGB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.maxRGB.Location = new System.Drawing.Point(36, 35);
            this.maxRGB.Name = "maxRGB";
            this.maxRGB.Size = new System.Drawing.Size(141, 26);
            this.maxRGB.TabIndex = 1;
            // 
            // minRGB
            // 
            this.minRGB.AutoSize = true;
            this.minRGB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.minRGB.Location = new System.Drawing.Point(36, 3);
            this.minRGB.Name = "minRGB";
            this.minRGB.Size = new System.Drawing.Size(141, 26);
            this.minRGB.TabIndex = 0;
            // 
            // ColorFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.minColorLabel);
            this.Controls.Add(this.maxColorLabel);
            this.Controls.Add(this.maxRGB);
            this.Controls.Add(this.minRGB);
            this.Name = "ColorFilter";
            this.Size = new System.Drawing.Size(180, 64);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RGBChooser minRGB;
        private RGBChooser maxRGB;
        private System.Windows.Forms.Label maxColorLabel;
        private System.Windows.Forms.Label minColorLabel;

    }
}
