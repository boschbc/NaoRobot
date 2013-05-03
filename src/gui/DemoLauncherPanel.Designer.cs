namespace Naovigate.GUI
{
    partial class DemoLauncherPanel
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
            this.launchSonarDemo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // launchSonarDemo
            // 
            this.launchSonarDemo.Location = new System.Drawing.Point(3, 3);
            this.launchSonarDemo.Name = "launchSonarDemo";
            this.launchSonarDemo.Size = new System.Drawing.Size(75, 23);
            this.launchSonarDemo.TabIndex = 0;
            this.launchSonarDemo.Text = "Sonar Demo";
            this.launchSonarDemo.UseVisualStyleBackColor = true;
            this.launchSonarDemo.Click += new System.EventHandler(this.launchSonarDemo_Click);
            // 
            // DemoLauncherPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.launchSonarDemo);
            this.Name = "DemoLauncherPanel";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button launchSonarDemo;
    }
}
