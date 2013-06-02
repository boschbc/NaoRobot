namespace Naovigate.GUI.State
{
    partial class EventQueueMonitor
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
            this.naoLabel = new System.Windows.Forms.Label();
            this.naoEventLabel = new System.Windows.Forms.Label();
            this.goalLabel = new System.Windows.Forms.Label();
            this.goalEventLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // naoLabel
            // 
            this.naoLabel.AutoSize = true;
            this.naoLabel.Location = new System.Drawing.Point(-3, 0);
            this.naoLabel.Name = "naoLabel";
            this.naoLabel.Size = new System.Drawing.Size(93, 13);
            this.naoLabel.TabIndex = 0;
            this.naoLabel.Text = "EventQueue.Nao:";
            // 
            // naoEventLabel
            // 
            this.naoEventLabel.AutoSize = true;
            this.naoEventLabel.Location = new System.Drawing.Point(96, 0);
            this.naoEventLabel.Name = "naoEventLabel";
            this.naoEventLabel.Size = new System.Drawing.Size(55, 13);
            this.naoEventLabel.TabIndex = 1;
            this.naoEventLabel.Text = "Pending...";
            // 
            // goalLabel
            // 
            this.goalLabel.AutoSize = true;
            this.goalLabel.Location = new System.Drawing.Point(-3, 13);
            this.goalLabel.Name = "goalLabel";
            this.goalLabel.Size = new System.Drawing.Size(95, 13);
            this.goalLabel.TabIndex = 2;
            this.goalLabel.Text = "EventQueue.Goal:";
            // 
            // goalEventLabel
            // 
            this.goalEventLabel.AutoSize = true;
            this.goalEventLabel.Location = new System.Drawing.Point(96, 13);
            this.goalEventLabel.Name = "goalEventLabel";
            this.goalEventLabel.Size = new System.Drawing.Size(55, 13);
            this.goalEventLabel.TabIndex = 3;
            this.goalEventLabel.Text = "Pending...";
            // 
            // EventQueueMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.goalEventLabel);
            this.Controls.Add(this.goalLabel);
            this.Controls.Add(this.naoEventLabel);
            this.Controls.Add(this.naoLabel);
            this.Name = "EventQueueMonitor";
            this.Size = new System.Drawing.Size(154, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label naoLabel;
        private System.Windows.Forms.Label naoEventLabel;
        private System.Windows.Forms.Label goalLabel;
        private System.Windows.Forms.Label goalEventLabel;

    }
}
