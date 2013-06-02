namespace Naovigate.GUI.State
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.locationMonitor = new Naovigate.GUI.State.LocationMonitor();
            this.temperatureMonitor = new Naovigate.GUI.State.TemperatureMonitor();
            this.batteryMonitor = new Naovigate.GUI.State.BatteryMonitor();
            this.tableLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.ColumnCount = 2;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel.Controls.Add(this.locationMonitor, 1, 0);
            this.tableLayoutPanel.Controls.Add(this.temperatureMonitor, 1, 1);
            this.tableLayoutPanel.Controls.Add(this.batteryMonitor, 1, 2);
            this.tableLayoutPanel.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 3;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(163, 78);
            this.tableLayoutPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 19);
            this.label1.TabIndex = 3;
            this.label1.Text = "Location: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Left;
            this.label2.Location = new System.Drawing.Point(3, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Temperature:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Left;
            this.label3.Location = new System.Drawing.Point(3, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 40);
            this.label3.TabIndex = 5;
            this.label3.Text = "Battery:";
            // 
            // locationMonitor
            // 
            this.locationMonitor.AutoSize = true;
            this.locationMonitor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.locationMonitor.Dock = System.Windows.Forms.DockStyle.Right;
            this.locationMonitor.Location = new System.Drawing.Point(101, 3);
            this.locationMonitor.Name = "locationMonitor";
            this.locationMonitor.Size = new System.Drawing.Size(59, 13);
            this.locationMonitor.TabIndex = 0;
            // 
            // temperatureMonitor
            // 
            this.temperatureMonitor.AutoSize = true;
            this.temperatureMonitor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.temperatureMonitor.Dock = System.Windows.Forms.DockStyle.Right;
            this.temperatureMonitor.Location = new System.Drawing.Point(101, 22);
            this.temperatureMonitor.Name = "temperatureMonitor";
            this.temperatureMonitor.Size = new System.Drawing.Size(59, 13);
            this.temperatureMonitor.TabIndex = 1;
            // 
            // batteryMonitor
            // 
            this.batteryMonitor.AutoSize = true;
            this.batteryMonitor.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.batteryMonitor.Dock = System.Windows.Forms.DockStyle.Right;
            this.batteryMonitor.Location = new System.Drawing.Point(84, 41);
            this.batteryMonitor.Name = "batteryMonitor";
            this.batteryMonitor.Size = new System.Drawing.Size(76, 34);
            this.batteryMonitor.TabIndex = 2;
            // 
            // StateMonitorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel);
            this.Name = "StateMonitorPanel";
            this.Size = new System.Drawing.Size(163, 78);
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private LocationMonitor locationMonitor;
        private TemperatureMonitor temperatureMonitor;
        private BatteryMonitor batteryMonitor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

    }
}
