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
            this.monitorTable = new System.Windows.Forms.TableLayoutPanel();
            this.locationMonitor = new Naovigate.GUI.LocationMonitor();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.monitorTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // monitorTable
            // 
            this.monitorTable.AutoSize = true;
            this.monitorTable.ColumnCount = 1;
            this.monitorTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.monitorTable.Controls.Add(this.locationMonitor, 0, 0);
            this.monitorTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.monitorTable.Location = new System.Drawing.Point(0, 0);
            this.monitorTable.Name = "monitorTable";
            this.monitorTable.RowCount = 1;
            this.monitorTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.monitorTable.Size = new System.Drawing.Size(150, 150);
            this.monitorTable.TabIndex = 0;
            // 
            // locationMonitor
            // 
            this.locationMonitor.AutoSize = true;
            this.locationMonitor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.locationMonitor.Location = new System.Drawing.Point(3, 3);
            this.locationMonitor.Name = "locationMonitor";
            this.locationMonitor.Size = new System.Drawing.Size(100, 26);
            this.locationMonitor.TabIndex = 0;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // StateMonitorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.monitorTable);
            this.Name = "StateMonitorPanel";
            this.monitorTable.ResumeLayout(false);
            this.monitorTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel monitorTable;
        private LocationMonitor locationMonitor;
        private System.Windows.Forms.Timer updateTimer;
    }
}
