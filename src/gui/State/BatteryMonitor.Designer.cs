namespace Naovigate.GUI.State
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
            this.SuspendLayout();
            // 
            // batteryGauge
            // 
            this.batteryGauge.Location = new System.Drawing.Point(0, 0);
            this.batteryGauge.MaximumSize = new System.Drawing.Size(100, 15);
            this.batteryGauge.Name = "batteryGauge";
            this.batteryGauge.Size = new System.Drawing.Size(74, 15);
            this.batteryGauge.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.batteryGauge.TabIndex = 2;
            // 
            // BatteryMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.batteryGauge);
            this.Name = "BatteryMonitor";
            this.Size = new System.Drawing.Size(77, 18);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar batteryGauge;

    }
}
