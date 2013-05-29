namespace Naovigate.GUI
{
    partial class GoalSimulator
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
            this.commandBox = new System.Windows.Forms.TextBox();
            this.simulateButton = new System.Windows.Forms.Button();
            this.eventQueueMonitor1 = new Naovigate.GUI.EventQueueMonitor();
            this.SuspendLayout();
            // 
            // commandBox
            // 
            this.commandBox.Location = new System.Drawing.Point(3, 33);
            this.commandBox.Name = "commandBox";
            this.commandBox.Size = new System.Drawing.Size(200, 20);
            this.commandBox.TabIndex = 0;
            // 
            // simulateButton
            // 
            this.simulateButton.Location = new System.Drawing.Point(104, 59);
            this.simulateButton.Name = "simulateButton";
            this.simulateButton.Size = new System.Drawing.Size(99, 23);
            this.simulateButton.TabIndex = 1;
            this.simulateButton.Text = "Simulate Stream";
            this.simulateButton.UseVisualStyleBackColor = true;
            this.simulateButton.Click += new System.EventHandler(this.simulateButton_Click);
            // 
            // eventQueueMonitor1
            // 
            this.eventQueueMonitor1.AutoSize = true;
            this.eventQueueMonitor1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.eventQueueMonitor1.Location = new System.Drawing.Point(3, 3);
            this.eventQueueMonitor1.Name = "eventQueueMonitor1";
            this.eventQueueMonitor1.Size = new System.Drawing.Size(154, 26);
            this.eventQueueMonitor1.TabIndex = 2;
            // 
            // GoalSimulator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.eventQueueMonitor1);
            this.Controls.Add(this.simulateButton);
            this.Controls.Add(this.commandBox);
            this.Name = "GoalSimulator";
            this.Size = new System.Drawing.Size(206, 85);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox commandBox;
        private System.Windows.Forms.Button simulateButton;
        private EventQueueMonitor eventQueueMonitor1;
    }
}
