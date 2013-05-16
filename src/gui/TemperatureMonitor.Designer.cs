namespace Naovigate.GUI
{
    partial class TemperatureMonitor
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
            this.labelTitle = new System.Windows.Forms.Label();
            this.labelAlert = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(-3, 0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(70, 13);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Temperature:";
            // 
            // labelAlert
            // 
            this.labelAlert.AutoSize = true;
            this.labelAlert.Location = new System.Drawing.Point(65, 0);
            this.labelAlert.Name = "labelAlert";
            this.labelAlert.Size = new System.Drawing.Size(53, 13);
            this.labelAlert.TabIndex = 1;
            this.labelAlert.Text = "Unknown";
            // 
            // TemperatureMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.labelAlert);
            this.Controls.Add(this.labelTitle);
            this.Name = "TemperatureMonitor";
            this.Size = new System.Drawing.Size(121, 13);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Label labelAlert;
    }
}
