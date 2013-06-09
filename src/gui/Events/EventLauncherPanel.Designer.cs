namespace Naovigate.GUI.Events
{
    partial class EventLauncherPanel
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
            this.boundingBox = new System.Windows.Forms.GroupBox();
            this.parameterPanel = new Naovigate.GUI.Events.ParameterPanel();
            this.internalEventLauncher = new Naovigate.GUI.Events.InternalEventLauncher();
            this.naoEventLauncher = new Naovigate.GUI.Events.NaoEventLauncher();
            this.goalEventLauncher = new Naovigate.GUI.Events.GoalEventLauncher();
            this.boundingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // boundingBox
            // 
            this.boundingBox.AutoSize = true;
            this.boundingBox.Controls.Add(this.parameterPanel);
            this.boundingBox.Controls.Add(this.internalEventLauncher);
            this.boundingBox.Controls.Add(this.naoEventLauncher);
            this.boundingBox.Controls.Add(this.goalEventLauncher);
            this.boundingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boundingBox.Location = new System.Drawing.Point(0, 0);
            this.boundingBox.Name = "boundingBox";
            this.boundingBox.Size = new System.Drawing.Size(233, 237);
            this.boundingBox.TabIndex = 0;
            this.boundingBox.TabStop = false;
            this.boundingBox.Text = "Events";
            // 
            // parameterPanel
            // 
            this.parameterPanel.AutoSize = true;
            this.parameterPanel.Location = new System.Drawing.Point(6, 127);
            this.parameterPanel.MinimumSize = new System.Drawing.Size(200, 90);
            this.parameterPanel.Name = "parameterPanel";
            this.parameterPanel.Size = new System.Drawing.Size(208, 91);
            this.parameterPanel.TabIndex = 3;
            // 
            // internalEventLauncher
            // 
            this.internalEventLauncher.AutoSize = true;
            this.internalEventLauncher.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.internalEventLauncher.Location = new System.Drawing.Point(6, 91);
            this.internalEventLauncher.Name = "internalEventLauncher";
            this.internalEventLauncher.Size = new System.Drawing.Size(221, 30);
            this.internalEventLauncher.TabIndex = 2;
            // 
            // naoEventLauncher
            // 
            this.naoEventLauncher.AutoSize = true;
            this.naoEventLauncher.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.naoEventLauncher.Location = new System.Drawing.Point(6, 55);
            this.naoEventLauncher.Name = "naoEventLauncher";
            this.naoEventLauncher.Size = new System.Drawing.Size(208, 30);
            this.naoEventLauncher.TabIndex = 1;
            // 
            // goalEventLauncher
            // 
            this.goalEventLauncher.AutoSize = true;
            this.goalEventLauncher.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.goalEventLauncher.Location = new System.Drawing.Point(6, 19);
            this.goalEventLauncher.Name = "goalEventLauncher";
            this.goalEventLauncher.Size = new System.Drawing.Size(208, 30);
            this.goalEventLauncher.TabIndex = 0;
            // 
            // EventLauncherPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.boundingBox);
            this.Name = "EventLauncherPanel";
            this.Size = new System.Drawing.Size(233, 237);
            this.boundingBox.ResumeLayout(false);
            this.boundingBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boundingBox;
        private Events.NaoEventLauncher naoEventLauncher;
        private Events.GoalEventLauncher goalEventLauncher;
        private Events.InternalEventLauncher internalEventLauncher;
        private Events.ParameterPanel parameterPanel;
    }
}
