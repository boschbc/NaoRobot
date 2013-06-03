namespace Naovigate.GUI.LiveCamera
{
    partial class LiveCamera
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
            this.cameraEnhanced = new System.Windows.Forms.CheckBox();
            this.cameraEnhancer = new Naovigate.GUI.LiveCamera.CameraEnhancerPanel();
            this.cameraEnabled = new System.Windows.Forms.CheckBox();
            this.imageContainer = new System.Windows.Forms.PictureBox();
            this.boundingBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).BeginInit();
            this.SuspendLayout();
            // 
            // boundingBox
            // 
            this.boundingBox.AutoSize = true;
            this.boundingBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.boundingBox.Controls.Add(this.cameraEnhanced);
            this.boundingBox.Controls.Add(this.cameraEnhancer);
            this.boundingBox.Controls.Add(this.cameraEnabled);
            this.boundingBox.Controls.Add(this.imageContainer);
            this.boundingBox.Location = new System.Drawing.Point(3, 3);
            this.boundingBox.Name = "boundingBox";
            this.boundingBox.Size = new System.Drawing.Size(332, 391);
            this.boundingBox.TabIndex = 2;
            this.boundingBox.TabStop = false;
            this.boundingBox.Text = "Video";
            // 
            // cameraEnhanced
            // 
            this.cameraEnhanced.AutoSize = true;
            this.cameraEnhanced.Location = new System.Drawing.Point(6, 289);
            this.cameraEnhanced.Name = "cameraEnhanced";
            this.cameraEnhanced.Size = new System.Drawing.Size(69, 17);
            this.cameraEnhanced.TabIndex = 4;
            this.cameraEnhanced.Text = "Enhance";
            this.cameraEnhanced.UseVisualStyleBackColor = true;
            this.cameraEnhanced.CheckedChanged += new System.EventHandler(this.cameraEnhanced_CheckedChanged);
            // 
            // cameraEnhancer
            // 
            this.cameraEnhancer.AutoSize = true;
            this.cameraEnhancer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cameraEnhancer.Location = new System.Drawing.Point(120, 266);
            this.cameraEnhancer.Name = "cameraEnhancer";
            this.cameraEnhancer.Size = new System.Drawing.Size(206, 106);
            this.cameraEnhancer.TabIndex = 5;
            this.cameraEnhancer.Target = null;
            // 
            // cameraEnabled
            // 
            this.cameraEnabled.AutoSize = true;
            this.cameraEnabled.Location = new System.Drawing.Point(6, 266);
            this.cameraEnabled.Name = "cameraEnabled";
            this.cameraEnabled.Size = new System.Drawing.Size(59, 17);
            this.cameraEnabled.TabIndex = 3;
            this.cameraEnabled.Text = "Enable";
            this.cameraEnabled.UseVisualStyleBackColor = true;
            this.cameraEnabled.CheckedChanged += new System.EventHandler(this.cameraEnabled_CheckedChanged);
            // 
            // imageContainer
            // 
            this.imageContainer.Location = new System.Drawing.Point(6, 20);
            this.imageContainer.Name = "imageContainer";
            this.imageContainer.Size = new System.Drawing.Size(320, 240);
            this.imageContainer.TabIndex = 2;
            this.imageContainer.TabStop = false;
            // 
            // LiveCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.boundingBox);
            this.Name = "LiveCamera";
            this.Size = new System.Drawing.Size(338, 397);
            this.boundingBox.ResumeLayout(false);
            this.boundingBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageContainer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boundingBox;
        private System.Windows.Forms.CheckBox cameraEnabled;
        private System.Windows.Forms.PictureBox imageContainer;
        private System.Windows.Forms.CheckBox cameraEnhanced;
        private CameraEnhancerPanel cameraEnhancer;

    }
}
