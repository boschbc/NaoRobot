namespace Naovigate.GUI
{
    partial class StateMonitorPanel
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
            this.components = new System.ComponentModel.Container();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.locationMonitor = new Naovigate.GUI.LocationMonitor();
            this.batteryMonitor = new Naovigate.GUI.BatteryMonitor();
            this.SuspendLayout();
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // locationMonitor
            // 
            this.locationMonitor.AutoSize = true;
            this.locationMonitor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.locationMonitor.Location = new System.Drawing.Point(0, 0);
            this.locationMonitor.Name = "locationMonitor";
            this.locationMonitor.Size = new System.Drawing.Size(100, 26);
            this.locationMonitor.TabIndex = 2;
            // 
            // batteryMonitor
            // 
            this.batteryMonitor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.batteryMonitor.Location = new System.Drawing.Point(3, 32);
            this.batteryMonitor.Name = "batteryMonitor";
            this.batteryMonitor.Size = new System.Drawing.Size(160, 33);
            this.batteryMonitor.TabIndex = 3;
            // 
            // StateMonitorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.locationMonitor);
            this.Controls.Add(this.batteryMonitor);
            this.Name = "StateMonitorPanel";
            this.Size = new System.Drawing.Size(166, 150);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer updateTimer;
        private LocationMonitor locationMonitor;
        private BatteryMonitor batteryMonitor;
    }
}
