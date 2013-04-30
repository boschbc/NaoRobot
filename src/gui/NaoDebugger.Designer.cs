using System.Windows.Forms;
using System.Collections.Generic;

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
            this.mainTabs = new System.Windows.Forms.TabControl();
            this.simulateTab = new System.Windows.Forms.TabPage();
            this.eventLauncherPanel = new Naovigate.GUI.EventLauncherPanel();
            this.stateTab = new System.Windows.Forms.TabPage();
            this.stateMonitorPanel = new Naovigate.GUI.StateMonitorPanel();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.cameraMonitor = new Naovigate.GUI.CameraMonitor();
            this.mainTabs.SuspendLayout();
            this.simulateTab.SuspendLayout();
            this.stateTab.SuspendLayout();
            this.cameraTab.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTabs
            // 
            this.mainTabs.Controls.Add(this.simulateTab);
            this.mainTabs.Controls.Add(this.stateTab);
            this.mainTabs.Controls.Add(this.cameraTab);
            this.mainTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTabs.Location = new System.Drawing.Point(0, 0);
            this.mainTabs.Name = "mainTabs";
            this.mainTabs.SelectedIndex = 0;
            this.mainTabs.Size = new System.Drawing.Size(213, 177);
            this.mainTabs.TabIndex = 0;
            // 
            // simulateTab
            // 
            this.simulateTab.Controls.Add(this.eventLauncherPanel);
            this.simulateTab.Location = new System.Drawing.Point(4, 22);
            this.simulateTab.Name = "simulateTab";
            this.simulateTab.Padding = new System.Windows.Forms.Padding(3);
            this.simulateTab.Size = new System.Drawing.Size(205, 151);
            this.simulateTab.TabIndex = 0;
            this.simulateTab.Text = "Simulate";
            this.simulateTab.UseVisualStyleBackColor = true;
            // 
            // eventLauncherPanel
            // 
            this.eventLauncherPanel.AutoSize = true;
            this.eventLauncherPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventLauncherPanel.Location = new System.Drawing.Point(3, 3);
            this.eventLauncherPanel.Name = "eventLauncherPanel";
            this.eventLauncherPanel.Size = new System.Drawing.Size(199, 136);
            this.eventLauncherPanel.TabIndex = 0;
            // 
            // stateTab
            // 
            this.stateTab.Controls.Add(this.stateMonitorPanel);
            this.stateTab.Location = new System.Drawing.Point(4, 22);
            this.stateTab.Name = "stateTab";
            this.stateTab.Padding = new System.Windows.Forms.Padding(3);
            this.stateTab.Size = new System.Drawing.Size(205, 151);
            this.stateTab.TabIndex = 1;
            this.stateTab.Text = "State";
            this.stateTab.UseVisualStyleBackColor = true;
            // 
            // stateMonitorPanel
            // 
            this.stateMonitorPanel.AutoSize = true;
            this.stateMonitorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.stateMonitorPanel.Location = new System.Drawing.Point(3, 3);
            this.stateMonitorPanel.Name = "stateMonitorPanel";
            this.stateMonitorPanel.Size = new System.Drawing.Size(199, 32);
            this.stateMonitorPanel.TabIndex = 1;
            // 
            // cameraTab
            // 
            this.cameraTab.Controls.Add(this.cameraMonitor);
            this.cameraTab.Location = new System.Drawing.Point(4, 22);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Size = new System.Drawing.Size(205, 151);
            this.cameraTab.TabIndex = 2;
            this.cameraTab.Text = "Camera";
            this.cameraTab.UseVisualStyleBackColor = true;
            // 
            // cameraMonitor
            // 
            this.cameraMonitor.Location = new System.Drawing.Point(0, 0);
            this.cameraMonitor.Name = "cameraMonitor";
            this.cameraMonitor.TabIndex = 0;
            // 
            // NaoDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(213, 177);
            this.Controls.Add(this.mainTabs);
            this.Name = "NaoDebugger";
            this.Text = "NaoDebugger";
            this.mainTabs.ResumeLayout(false);
            this.simulateTab.ResumeLayout(false);
            this.simulateTab.PerformLayout();
            this.stateTab.ResumeLayout(false);
            this.stateTab.PerformLayout();
            this.cameraTab.ResumeLayout(false);
            this.cameraTab.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl mainTabs;
        private TabPage simulateTab;
        private TabPage stateTab;
        private TabPage cameraTab;
        private EventLauncherPanel eventLauncherPanel;
        private StateMonitorPanel stateMonitorPanel;
        private CameraMonitor cameraMonitor;
    }
}