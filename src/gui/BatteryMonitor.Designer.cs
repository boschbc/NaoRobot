namespace Naovigate.GUI
{
    partial class BatteryMonitor
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
            this.batteryGauge = new System.Windows.Forms.ProgressBar();
            this.label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // batteryGauge
            // 
            this.batteryGauge.Location = new System.Drawing.Point(52, 9);
            this.batteryGauge.MaximumSize = new System.Drawing.Size(100, 15);
            this.batteryGauge.Name = "batteryGauge";
            this.batteryGauge.Size = new System.Drawing.Size(100, 15);
            this.batteryGauge.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.batteryGauge.TabIndex = 0;
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(3, 9);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(43, 13);
            this.label.TabIndex = 1;
            this.label.Text = "Battery:";
            // 
            // BatteryMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label);
            this.Controls.Add(this.batteryGauge);
            this.Name = "BatteryMonitor";
            this.Size = new System.Drawing.Size(161, 32);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar batteryGauge;
        private System.Windows.Forms.Label label;
    }
}
