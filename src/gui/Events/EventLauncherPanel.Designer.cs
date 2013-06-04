namespace Naovigate.GUI.Panels
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
            this.naoEventLauncher1 = new Naovigate.GUI.Events.NaoEventLauncher();
            this.goalEventLauncher1 = new Naovigate.GUI.Events.GoalEventLauncher();
            this.internalEventLauncher1 = new Naovigate.GUI.Events.InternalEventLauncher();
            this.boundingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // boundingBox
            // 
            this.boundingBox.AutoSize = true;
            this.boundingBox.Controls.Add(this.internalEventLauncher1);
            this.boundingBox.Controls.Add(this.naoEventLauncher1);
            this.boundingBox.Controls.Add(this.goalEventLauncher1);
            this.boundingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boundingBox.Location = new System.Drawing.Point(0, 0);
            this.boundingBox.Name = "boundingBox";
            this.boundingBox.Size = new System.Drawing.Size(220, 140);
            this.boundingBox.TabIndex = 0;
            this.boundingBox.TabStop = false;
            this.boundingBox.Text = "Events";
            // 
            // naoEventLauncher1
            // 
            this.naoEventLauncher1.AutoSize = true;
            this.naoEventLauncher1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.naoEventLauncher1.Location = new System.Drawing.Point(6, 55);
            this.naoEventLauncher1.Name = "naoEventLauncher1";
            this.naoEventLauncher1.Size = new System.Drawing.Size(208, 30);
            this.naoEventLauncher1.TabIndex = 1;
            // 
            // goalEventLauncher1
            // 
            this.goalEventLauncher1.AutoSize = true;
            this.goalEventLauncher1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.goalEventLauncher1.Location = new System.Drawing.Point(6, 19);
            this.goalEventLauncher1.Name = "goalEventLauncher1";
            this.goalEventLauncher1.Size = new System.Drawing.Size(208, 30);
            this.goalEventLauncher1.TabIndex = 0;
            // 
            // internalEventLauncher1
            // 
            this.internalEventLauncher1.AutoSize = true;
            this.internalEventLauncher1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.internalEventLauncher1.Location = new System.Drawing.Point(6, 91);
            this.internalEventLauncher1.Name = "internalEventLauncher1";
            this.internalEventLauncher1.Size = new System.Drawing.Size(208, 30);
            this.internalEventLauncher1.TabIndex = 2;
            // 
            // EventLauncherPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.boundingBox);
            this.Name = "EventLauncherPanel";
            this.Size = new System.Drawing.Size(220, 140);
            this.boundingBox.ResumeLayout(false);
            this.boundingBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boundingBox;
        private Events.NaoEventLauncher naoEventLauncher1;
        private Events.GoalEventLauncher goalEventLauncher1;
        private Events.InternalEventLauncher internalEventLauncher1;
    }
}
