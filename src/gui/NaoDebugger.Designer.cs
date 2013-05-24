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
            this.cameraBox = new System.Windows.Forms.GroupBox();
            this.stateBox = new System.Windows.Forms.GroupBox();
            this.goalSimulatorBox = new System.Windows.Forms.GroupBox();
            this.goalSimulator1 = new Naovigate.GUI.GoalSimulator();
            this.eventLauncherPanel = new Naovigate.GUI.EventLauncherPanel();
            this.stateMonitorPanel = new Naovigate.GUI.StateMonitorPanel();
            this.cameraMonitor = new Naovigate.GUI.CameraMonitor();
            this.cameraBox.SuspendLayout();
            this.stateBox.SuspendLayout();
            this.goalSimulatorBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // cameraBox
            // 
            this.cameraBox.AutoSize = true;
            this.cameraBox.Controls.Add(this.cameraMonitor);
            this.cameraBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.cameraBox.Location = new System.Drawing.Point(338, 0);
            this.cameraBox.Name = "cameraBox";
            this.cameraBox.Size = new System.Drawing.Size(335, 368);
            this.cameraBox.TabIndex = 0;
            this.cameraBox.TabStop = false;
            this.cameraBox.Text = "Camera";
            // 
            // stateBox
            // 
            this.stateBox.AutoSize = true;
            this.stateBox.Controls.Add(this.stateMonitorPanel);
            this.stateBox.Location = new System.Drawing.Point(12, 131);
            this.stateBox.Name = "stateBox";
            this.stateBox.Size = new System.Drawing.Size(200, 128);
            this.stateBox.TabIndex = 2;
            this.stateBox.TabStop = false;
            this.stateBox.Text = "State";
            // 
            // goalSimulatorBox
            // 
            this.goalSimulatorBox.AutoSize = true;
            this.goalSimulatorBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.goalSimulatorBox.Controls.Add(this.goalSimulator1);
            this.goalSimulatorBox.Location = new System.Drawing.Point(12, 265);
            this.goalSimulatorBox.Name = "goalSimulatorBox";
            this.goalSimulatorBox.Size = new System.Drawing.Size(218, 93);
            this.goalSimulatorBox.TabIndex = 4;
            this.goalSimulatorBox.TabStop = false;
            this.goalSimulatorBox.Text = "GOAL";
            // 
            // goalSimulator1
            // 
            this.goalSimulator1.AutoSize = true;
            this.goalSimulator1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.goalSimulator1.Location = new System.Drawing.Point(6, 19);
            this.goalSimulator1.Name = "goalSimulator1";
            this.goalSimulator1.Size = new System.Drawing.Size(206, 55);
            this.goalSimulator1.TabIndex = 0;
            // 
            // eventLauncherPanel
            // 
            this.eventLauncherPanel.AutoSize = true;
            this.eventLauncherPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.eventLauncherPanel.Location = new System.Drawing.Point(12, 12);
            this.eventLauncherPanel.Name = "eventLauncherPanel";
            this.eventLauncherPanel.Size = new System.Drawing.Size(146, 113);
            this.eventLauncherPanel.TabIndex = 3;
            // 
            // stateMonitorPanel
            // 
            this.stateMonitorPanel.AutoSize = true;
            this.stateMonitorPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.stateMonitorPanel.Location = new System.Drawing.Point(6, 19);
            this.stateMonitorPanel.Name = "stateMonitorPanel";
            this.stateMonitorPanel.Size = new System.Drawing.Size(163, 90);
            this.stateMonitorPanel.TabIndex = 1;
            // 
            // cameraMonitor
            // 
            this.cameraMonitor.Location = new System.Drawing.Point(9, 19);
            this.cameraMonitor.Name = "cameraMonitor";
            this.cameraMonitor.Size = new System.Drawing.Size(320, 276);
            this.cameraMonitor.TabIndex = 0;
            // 
            // NaoDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(673, 368);
            this.Controls.Add(this.goalSimulatorBox);
            this.Controls.Add(this.eventLauncherPanel);
            this.Controls.Add(this.stateBox);
            this.Controls.Add(this.cameraBox);
            this.Name = "NaoDebugger";
            this.Text = "NaoDebugger";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.NaoDebugger_FormClosed);
            this.cameraBox.ResumeLayout(false);
            this.stateBox.ResumeLayout(false);
            this.stateBox.PerformLayout();
            this.goalSimulatorBox.ResumeLayout(false);
            this.goalSimulatorBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox cameraBox;
        private CameraMonitor cameraMonitor;
        private StateMonitorPanel stateMonitorPanel;
        private System.Windows.Forms.GroupBox stateBox;
        private EventLauncherPanel eventLauncherPanel;
        private System.Windows.Forms.GroupBox goalSimulatorBox;
        private GoalSimulator goalSimulator1;
    }
}