namespace Naovigate.GUI.LiveCamera
{
    partial class CameraEnhancerPanel
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
            this.boundingBox = new System.Windows.Forms.GroupBox();
            this.maxLabel = new System.Windows.Forms.Label();
            this.minLabel = new System.Windows.Forms.Label();
            this.minRGB = new Naovigate.GUI.Util.RGBChooser();
            this.maxRGB = new Naovigate.GUI.Util.RGBChooser();
            this.boundingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // boundingBox
            // 
            this.boundingBox.Controls.Add(this.maxLabel);
            this.boundingBox.Controls.Add(this.minLabel);
            this.boundingBox.Controls.Add(this.minRGB);
            this.boundingBox.Controls.Add(this.maxRGB);
            this.boundingBox.Location = new System.Drawing.Point(3, 3);
            this.boundingBox.Name = "boundingBox";
            this.boundingBox.Size = new System.Drawing.Size(200, 100);
            this.boundingBox.TabIndex = 0;
            this.boundingBox.TabStop = false;
            this.boundingBox.Text = "Enhancement";
            // 
            // maxLabel
            // 
            this.maxLabel.AutoSize = true;
            this.maxLabel.Location = new System.Drawing.Point(12, 21);
            this.maxLabel.Name = "maxLabel";
            this.maxLabel.Size = new System.Drawing.Size(30, 13);
            this.maxLabel.TabIndex = 7;
            this.maxLabel.Text = "Max:";
            // 
            // minLabel
            // 
            this.minLabel.AutoSize = true;
            this.minLabel.Location = new System.Drawing.Point(12, 53);
            this.minLabel.Name = "minLabel";
            this.minLabel.Size = new System.Drawing.Size(27, 13);
            this.minLabel.TabIndex = 6;
            this.minLabel.Text = "Min:";
            // 
            // minRGB
            // 
            this.minRGB.AutoSize = true;
            this.minRGB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.minRGB.Location = new System.Drawing.Point(48, 53);
            this.minRGB.Name = "minRGB";
            this.minRGB.Size = new System.Drawing.Size(141, 26);
            this.minRGB.TabIndex = 5;
            // 
            // maxRGB
            // 
            this.maxRGB.AutoSize = true;
            this.maxRGB.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.maxRGB.Location = new System.Drawing.Point(48, 21);
            this.maxRGB.Name = "maxRGB";
            this.maxRGB.Size = new System.Drawing.Size(141, 26);
            this.maxRGB.TabIndex = 4;
            // 
            // CameraEnhancerPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.boundingBox);
            this.Name = "CameraEnhancerPanel";
            this.Size = new System.Drawing.Size(206, 106);
            this.boundingBox.ResumeLayout(false);
            this.boundingBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox boundingBox;
        private System.Windows.Forms.Label maxLabel;
        private System.Windows.Forms.Label minLabel;
        private Util.RGBChooser minRGB;
        private Util.RGBChooser maxRGB;

    }
}
