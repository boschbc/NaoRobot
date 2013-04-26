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
            this.stateTab = new System.Windows.Forms.TabPage();
            this.cameraTab = new System.Windows.Forms.TabPage();
            this.eventLauncherPanel1 = new Naovigate.GUI.EventLauncherPanel();
            this.stateMonitorPanel1 = new Naovigate.GUI.StateMonitorPanel();
            this.mainTabs.SuspendLayout();
            this.simulateTab.SuspendLayout();
            this.stateTab.SuspendLayout();
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
            this.simulateTab.Controls.Add(this.eventLauncherPanel1);
            this.simulateTab.Location = new System.Drawing.Point(4, 22);
            this.simulateTab.Name = "simulateTab";
            this.simulateTab.Padding = new System.Windows.Forms.Padding(3);
            this.simulateTab.Size = new System.Drawing.Size(205, 151);
            this.simulateTab.TabIndex = 0;
            this.simulateTab.Text = "Simulate";
            this.simulateTab.UseVisualStyleBackColor = true;
            // 
            // stateTab
            // 
            this.stateTab.Controls.Add(this.stateMonitorPanel1);
            this.stateTab.Location = new System.Drawing.Point(4, 22);
            this.stateTab.Name = "stateTab";
            this.stateTab.Padding = new System.Windows.Forms.Padding(3);
            this.stateTab.Size = new System.Drawing.Size(532, 450);
            this.stateTab.TabIndex = 1;
            this.stateTab.Text = "State";
            this.stateTab.UseVisualStyleBackColor = true;
            // 
            // cameraTab
            // 
            this.cameraTab.Location = new System.Drawing.Point(4, 22);
            this.cameraTab.Name = "cameraTab";
            this.cameraTab.Size = new System.Drawing.Size(532, 450);
            this.cameraTab.TabIndex = 2;
            this.cameraTab.Text = "Camera";
            this.cameraTab.UseVisualStyleBackColor = true;
            // 
            // eventLauncherPanel1
            // 
            this.eventLauncherPanel1.AutoSize = true;
            this.eventLauncherPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.eventLauncherPanel1.Location = new System.Drawing.Point(3, 3);
            this.eventLauncherPanel1.Name = "eventLauncherPanel1";
            this.eventLauncherPanel1.Size = new System.Drawing.Size(199, 136);
            this.eventLauncherPanel1.TabIndex = 0;
            // 
            // stateMonitorPanel1
            // 
            this.stateMonitorPanel1.AutoSize = true;
            this.stateMonitorPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.stateMonitorPanel1.Location = new System.Drawing.Point(3, 3);
            this.stateMonitorPanel1.Name = "stateMonitorPanel1";
            this.stateMonitorPanel1.Size = new System.Drawing.Size(526, 32);
            this.stateMonitorPanel1.TabIndex = 1;
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
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl mainTabs;
        private TabPage simulateTab;
        private TabPage stateTab;
        private TabPage cameraTab;
        private EventLauncherPanel eventLauncherPanel1;
        private StateMonitorPanel stateMonitorPanel1;
    }
}