namespace Naovigate.GUI
{
    partial class CameraMonitor
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
            this.components = new System.ComponentModel.Container();
            this.imageContainer = new System.Windows.Forms.PictureBox();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.cameraEnabler = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // imageContainer
            // 
            this.imageContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.imageContainer.Location = new System.Drawing.Point(0, 0);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(320, 240);
            this.imageContainer.TabIndex = 0;
            this.imageContainer.TabStop = false;
            // 
            // cameraEnabler
            // 
            this.cameraEnabler.Location = new System.Drawing.Point(3, 246);
            this.cameraEnabler.Name = "cameraEnabler";
            this.cameraEnabler.Size = new System.Drawing.Size(104, 24);
            this.cameraEnabler.TabIndex = 1;
            this.cameraEnabler.Text = "Enable Camera";
            this.cameraEnabler.UseVisualStyleBackColor = true;
            // 
            // CameraMonitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cameraEnabler);
            this.Controls.Add(this.imageContainer);
            this.Name = "CameraMonitor";
            this.Size = new System.Drawing.Size(320, 276);
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox imageContainer;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.CheckBox cameraEnabler;
    }
}
