namespace Naovigate.GUI.Util
{
    partial class StartLocationChooser
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
            this.startLocation = new System.Windows.Forms.NumericUpDown();
            this.label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.startLocation)).BeginInit();
            this.SuspendLayout();
            // 
            // startLocation
            // 
            this.startLocation.Location = new System.Drawing.Point(53, 3);
            this.startLocation.Name = "startLocation";
            this.startLocation.Size = new System.Drawing.Size(35, 20);
            this.startLocation.TabIndex = 0;
            this.startLocation.ValueChanged += new System.EventHandler(this.startLocation_ValueChanged);
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 5);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(44, 13);
            this.label.TabIndex = 1;
            this.label.Text = "Start at:";
            // 
            // StartLocationChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label);
            this.Controls.Add(this.startLocation);
            this.Name = "StartLocationChooser";
            this.Size = new System.Drawing.Size(91, 26);
            ((System.ComponentModel.ISupportInitialize)(this.startLocation)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown startLocation;
        private System.Windows.Forms.Label label;
    }
}
