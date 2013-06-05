namespace Naovigate.GUI
{
    partial class NaoDebugger
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.eventsTab = new System.Windows.Forms.TabPage();
            this.eventLauncherPanel1 = new Naovigate.GUI.Panels.EventLauncherPanel();
            this.goalTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.localServerPanel1 = new Naovigate.GUI.Goal.LocalServerPanel();
            this.remoteServerControl1 = new Naovigate.GUI.Goal.RemoteServerControl();
            this.stateTab = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.stateMonitorPanel = new Naovigate.GUI.State.StateMonitorPanel();
            this.naoConnection = new Naovigate.GUI.State.NaoConnection();
            this.liveCamera = new Naovigate.GUI.LiveCamera.LiveCamera();
            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.eventsTab.SuspendLayout();
            this.goalTab.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.stateTab.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl);
            this.panel1.Controls.Add(this.liveCamera);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(614, 422);
            this.panel1.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.eventsTab);
            this.tabControl.Controls.Add(this.goalTab);
            this.tabControl.Controls.Add(this.stateTab);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(265, 422);
            this.tabControl.TabIndex = 2;
            // 
            // eventsTab
            // 
            this.eventsTab.Controls.Add(this.eventLauncherPanel1);
            this.eventsTab.Location = new System.Drawing.Point(4, 22);
            this.eventsTab.Name = "eventsTab";
            this.eventsTab.Padding = new System.Windows.Forms.Padding(3);
            this.eventsTab.Size = new System.Drawing.Size(257, 396);
            this.eventsTab.TabIndex = 1;
            this.eventsTab.Text = "Events";
            this.eventsTab.UseVisualStyleBackColor = true;
            // 
            // eventLauncherPanel1
            // 
            this.eventLauncherPanel1.AutoSize = true;
            this.eventLauncherPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.eventLauncherPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventLauncherPanel1.Location = new System.Drawing.Point(3, 3);
            this.eventLauncherPanel1.Name = "eventLauncherPanel1";
            this.eventLauncherPanel1.Size = new System.Drawing.Size(251, 237);
            this.eventLauncherPanel1.TabIndex = 0;
            // 
            // goalTab
            // 
            this.goalTab.Controls.Add(this.tableLayoutPanel2);
            this.goalTab.ForeColor = System.Drawing.SystemColors.ControlText;
            this.goalTab.Location = new System.Drawing.Point(4, 22);
            this.goalTab.Name = "goalTab";
            this.goalTab.Size = new System.Drawing.Size(257, 396);
            this.goalTab.TabIndex = 2;
            this.goalTab.Text = "Goal";
            this.goalTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.localServerPanel1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.remoteServerControl1, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(257, 396);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // localServerPanel1
            // 
            this.localServerPanel1.AutoSize = true;
            this.localServerPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.localServerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localServerPanel1.Location = new System.Drawing.Point(3, 3);
            this.localServerPanel1.MinimumSize = new System.Drawing.Size(170, 40);
            this.localServerPanel1.Name = "localServerPanel1";
            this.localServerPanel1.Size = new System.Drawing.Size(251, 40);
            this.localServerPanel1.TabIndex = 0;
            // 
            // remoteServerControl1
            // 
            this.remoteServerControl1.AutoSize = true;
            this.remoteServerControl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.remoteServerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.remoteServerControl1.Location = new System.Drawing.Point(3, 49);
            this.remoteServerControl1.MinimumSize = new System.Drawing.Size(235, 20);
            this.remoteServerControl1.Name = "remoteServerControl1";
            this.remoteServerControl1.Size = new System.Drawing.Size(251, 71);
            this.remoteServerControl1.TabIndex = 1;
            // 
            // stateTab
            // 
            this.stateTab.Controls.Add(this.tableLayoutPanel1);
            this.stateTab.Location = new System.Drawing.Point(4, 22);
            this.stateTab.Name = "stateTab";
            this.stateTab.Padding = new System.Windows.Forms.Padding(3);
            this.stateTab.Size = new System.Drawing.Size(257, 396);
            this.stateTab.TabIndex = 0;
            this.stateTab.Text = "State";
            this.stateTab.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.stateMonitorPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.naoConnection, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(251, 390);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // stateMonitorPanel
            // 
            this.stateMonitorPanel.Active = false;
            this.stateMonitorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stateMonitorPanel.Location = new System.Drawing.Point(3, 80);
            this.stateMonitorPanel.Name = "stateMonitorPanel";
            this.stateMonitorPanel.Size = new System.Drawing.Size(245, 189);
            this.stateMonitorPanel.TabIndex = 0;
            // 
            // naoConnection
            // 
            this.naoConnection.AutoSize = true;
            this.naoConnection.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.naoConnection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naoConnection.Location = new System.Drawing.Point(3, 3);
            this.naoConnection.MinimumSize = new System.Drawing.Size(190, 20);
            this.naoConnection.Name = "naoConnection";
            this.naoConnection.Size = new System.Drawing.Size(245, 71);
            this.naoConnection.TabIndex = 1;
            // 
            // liveCamera
            // 
            this.liveCamera.Active = false;
            this.liveCamera.AutoSize = true;
            this.liveCamera.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.liveCamera.ImageSource = null;
            this.liveCamera.Location = new System.Drawing.Point(264, 8);
            this.liveCamera.Name = "liveCamera";
            this.liveCamera.Size = new System.Drawing.Size(338, 397);
            this.liveCamera.TabIndex = 0;
            // 
            // NaoDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 422);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MinimumSize = new System.Drawing.Size(620, 450);
            this.Name = "NaoDebugger";
            this.Text = "NaoDebugger";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.eventsTab.ResumeLayout(false);
            this.eventsTab.PerformLayout();
            this.goalTab.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.stateTab.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private LiveCamera.LiveCamera liveCamera;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage stateTab;
        private System.Windows.Forms.TabPage eventsTab;
        private System.Windows.Forms.TabPage goalTab;
        private State.StateMonitorPanel stateMonitorPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private State.NaoConnection naoConnection;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private Goal.LocalServerPanel localServerPanel1;
        private Goal.RemoteServerControl remoteServerControl1;
        private Panels.EventLauncherPanel eventLauncherPanel1;

    }
}