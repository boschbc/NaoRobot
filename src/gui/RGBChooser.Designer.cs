namespace Naovigate.GUI
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
            this.RedMin = new System.Windows.Forms.TrackBar();
            this.BlueMin = new System.Windows.Forms.TrackBar();
            this.GreenMin = new System.Windows.Forms.TrackBar();
            this.BlueMax = new System.Windows.Forms.TrackBar();
            this.GreenMax = new System.Windows.Forms.TrackBar();
            this.RedMax = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.RedMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenMax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedMax)).BeginInit();
            this.SuspendLayout();
            // 
            // RedMin
            // 
            this.RedMin.Location = new System.Drawing.Point(0, 3);
            this.RedMin.Maximum = 255;
            this.RedMin.Name = "RedMin";
            this.RedMin.Size = new System.Drawing.Size(104, 45);
            this.RedMin.TabIndex = 0;
            // 
            // BlueMin
            // 
            this.BlueMin.Location = new System.Drawing.Point(0, 102);
            this.BlueMin.Maximum = 255;
            this.BlueMin.Name = "BlueMin";
            this.BlueMin.Size = new System.Drawing.Size(104, 45);
            this.BlueMin.TabIndex = 1;
            // 
            // GreenMin
            // 
            this.GreenMin.Location = new System.Drawing.Point(0, 54);
            this.GreenMin.Maximum = 255;
            this.GreenMin.Name = "GreenMin";
            this.GreenMin.Size = new System.Drawing.Size(104, 45);
            this.GreenMin.TabIndex = 2;
            // 
            // BlueMax
            // 
            this.BlueMax.Location = new System.Drawing.Point(110, 102);
            this.BlueMax.Maximum = 255;
            this.BlueMax.Name = "BlueMax";
            this.BlueMax.Size = new System.Drawing.Size(104, 45);
            this.BlueMax.TabIndex = 3;
            this.BlueMax.Tag = "";
            // 
            // GreenMax
            // 
            this.GreenMax.Location = new System.Drawing.Point(110, 53);
            this.GreenMax.Maximum = 255;
            this.GreenMax.Name = "GreenMax";
            this.GreenMax.Size = new System.Drawing.Size(104, 45);
            this.GreenMax.TabIndex = 4;
            // 
            // RedMax
            // 
            this.RedMax.Location = new System.Drawing.Point(110, 3);
            this.RedMax.Maximum = 255;
            this.RedMax.Name = "RedMax";
            this.RedMax.Size = new System.Drawing.Size(104, 45);
            this.RedMax.TabIndex = 5;
            // 
            // RGBChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.RedMax);
            this.Controls.Add(this.GreenMax);
            this.Controls.Add(this.BlueMax);
            this.Controls.Add(this.GreenMin);
            this.Controls.Add(this.BlueMin);
            this.Controls.Add(this.RedMin);
            this.Name = "RGBChooser";
            this.Size = new System.Drawing.Size(217, 150);
            ((System.ComponentModel.ISupportInitialize)(this.RedMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BlueMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GreenMax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RedMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar RedMin;
        private System.Windows.Forms.TrackBar BlueMin;
        private System.Windows.Forms.TrackBar GreenMin;
        private System.Windows.Forms.TrackBar BlueMax;
        private System.Windows.Forms.TrackBar GreenMax;
        private System.Windows.Forms.TrackBar RedMax;

    }
}
