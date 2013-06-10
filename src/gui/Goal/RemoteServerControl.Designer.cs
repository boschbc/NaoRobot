namespace Naovigate.GUI.Goal
{
    partial class RemoteServerControl
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
            this.button = new System.Windows.Forms.Button();
            this.ipChooser = new Naovigate.GUI.Util.IPChooser();
            this.boundingBox = new System.Windows.Forms.GroupBox();
            this.boundingBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // button
            // 
            this.button.AutoSize = true;
            this.button.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.button.Location = new System.Drawing.Point(3, 45);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(229, 23);
            this.button.TabIndex = 3;
            this.button.Text = "Connect";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // ipChooser
            // 
            this.ipChooser.AutoSize = true;
            this.ipChooser.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ipChooser.Dock = System.Windows.Forms.DockStyle.Top;
            this.ipChooser.IP = "127.0.0.1";
            this.ipChooser.Location = new System.Drawing.Point(3, 16);
            this.ipChooser.Name = "ipChooser";
            this.ipChooser.Size = new System.Drawing.Size(229, 29);
            this.ipChooser.TabIndex = 4;
            // 
            // boundingBox
            // 
            this.boundingBox.AutoSize = true;
            this.boundingBox.Controls.Add(this.ipChooser);
            this.boundingBox.Controls.Add(this.button);
            this.boundingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boundingBox.Location = new System.Drawing.Point(0, 0);
            this.boundingBox.Name = "boundingBox";
            this.boundingBox.Size = new System.Drawing.Size(235, 71);
            this.boundingBox.TabIndex = 5;
            this.boundingBox.TabStop = false;
            this.boundingBox.Text = "Connect to Goal Server";
            // 
            // RemoteServerControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.boundingBox);
            this.MinimumSize = new System.Drawing.Size(235, 20);
            this.Name = "RemoteServerControl";
            this.Size = new System.Drawing.Size(235, 71);
            this.boundingBox.ResumeLayout(false);
            this.boundingBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button;
        private Util.IPChooser ipChooser;
        private System.Windows.Forms.GroupBox boundingBox;

    }
}
