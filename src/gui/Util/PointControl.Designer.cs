namespace Naovigate.GUI.Util
{
    partial class PointControl
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
            this.x = new System.Windows.Forms.NumericUpDown();
            this.y = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.x)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).BeginInit();
            this.SuspendLayout();
            // 
            // x
            // 
            this.x.Location = new System.Drawing.Point(0, 3);
            this.x.Name = "x";
            this.x.Size = new System.Drawing.Size(39, 20);
            this.x.TabIndex = 0;
            this.x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // y
            // 
            this.y.Location = new System.Drawing.Point(45, 3);
            this.y.Name = "y";
            this.y.Size = new System.Drawing.Size(39, 20);
            this.y.TabIndex = 1;
            this.y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PointControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.y);
            this.Controls.Add(this.x);
            this.Name = "PointControl";
            this.Size = new System.Drawing.Size(87, 26);
            ((System.ComponentModel.ISupportInitialize)(this.x)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown x;
        private System.Windows.Forms.NumericUpDown y;
    }
}
