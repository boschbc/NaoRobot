namespace Naovigate.GUI
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
            this.eventTypeBox = new System.Windows.Forms.GroupBox();
            this.radioSitDown = new System.Windows.Forms.RadioButton();
            this.radioHalt = new System.Windows.Forms.RadioButton();
            this.radioPutDown = new System.Windows.Forms.RadioButton();
            this.launchButton = new System.Windows.Forms.Button();
            this.radioStandUp = new System.Windows.Forms.RadioButton();
            this.radioGrab = new System.Windows.Forms.RadioButton();
            this.radioMove = new System.Windows.Forms.RadioButton();
            this.eventTypeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // eventTypeBox
            // 
            this.eventTypeBox.AutoSize = true;
            this.eventTypeBox.Controls.Add(this.radioSitDown);
            this.eventTypeBox.Controls.Add(this.radioHalt);
            this.eventTypeBox.Controls.Add(this.radioPutDown);
            this.eventTypeBox.Controls.Add(this.launchButton);
            this.eventTypeBox.Controls.Add(this.radioStandUp);
            this.eventTypeBox.Controls.Add(this.radioGrab);
            this.eventTypeBox.Controls.Add(this.radioMove);
            this.eventTypeBox.Location = new System.Drawing.Point(3, 3);
            this.eventTypeBox.Name = "eventTypeBox";
            this.eventTypeBox.Size = new System.Drawing.Size(209, 107);
            this.eventTypeBox.TabIndex = 6;
            this.eventTypeBox.TabStop = false;
            this.eventTypeBox.Text = "Commands";
            // 
            // radioSitDown
            // 
            this.radioSitDown.AutoSize = true;
            this.radioSitDown.Location = new System.Drawing.Point(135, 42);
            this.radioSitDown.Name = "radioSitDown";
            this.radioSitDown.Size = new System.Drawing.Size(68, 17);
            this.radioSitDown.TabIndex = 6;
            this.radioSitDown.TabStop = true;
            this.radioSitDown.Text = "Sit Down";
            this.radioSitDown.UseVisualStyleBackColor = true;
            // 
            // radioHalt
            // 
            this.radioHalt.AutoSize = true;
            this.radioHalt.Location = new System.Drawing.Point(6, 42);
            this.radioHalt.Name = "radioHalt";
            this.radioHalt.Size = new System.Drawing.Size(44, 17);
            this.radioHalt.TabIndex = 5;
            this.radioHalt.TabStop = true;
            this.radioHalt.Text = "Halt";
            this.radioHalt.UseVisualStyleBackColor = true;
            // 
            // radioPutDown
            // 
            this.radioPutDown.AutoSize = true;
            this.radioPutDown.Location = new System.Drawing.Point(64, 19);
            this.radioPutDown.Name = "radioPutDown";
            this.radioPutDown.Size = new System.Drawing.Size(69, 17);
            this.radioPutDown.TabIndex = 3;
            this.radioPutDown.TabStop = true;
            this.radioPutDown.Text = "PutDown";
            this.radioPutDown.UseVisualStyleBackColor = true;
            // 
            // launchButton
            // 
            this.launchButton.Location = new System.Drawing.Point(85, 65);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(94, 23);
            this.launchButton.TabIndex = 4;
            this.launchButton.Text = "Launch Event";
            this.launchButton.UseVisualStyleBackColor = true;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // radioStandUp
            // 
            this.radioStandUp.AutoSize = true;
            this.radioStandUp.Location = new System.Drawing.Point(133, 19);
            this.radioStandUp.Name = "radioStandUp";
            this.radioStandUp.Size = new System.Drawing.Size(70, 17);
            this.radioStandUp.TabIndex = 2;
            this.radioStandUp.Text = "Stand Up";
            this.radioStandUp.UseVisualStyleBackColor = true;
            // 
            // radioGrab
            // 
            this.radioGrab.AutoSize = true;
            this.radioGrab.Location = new System.Drawing.Point(64, 42);
            this.radioGrab.Name = "radioGrab";
            this.radioGrab.Size = new System.Drawing.Size(48, 17);
            this.radioGrab.TabIndex = 1;
            this.radioGrab.Text = "Grab";
            this.radioGrab.UseVisualStyleBackColor = true;
            // 
            // radioMove
            // 
            this.radioMove.AutoSize = true;
            this.radioMove.Checked = true;
            this.radioMove.Location = new System.Drawing.Point(6, 19);
            this.radioMove.Name = "radioMove";
            this.radioMove.Size = new System.Drawing.Size(52, 17);
            this.radioMove.TabIndex = 0;
            this.radioMove.TabStop = true;
            this.radioMove.Text = "Move";
            this.radioMove.UseVisualStyleBackColor = true;
            // 
            // EventLauncherPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.eventTypeBox);
            this.Name = "EventLauncherPanel";
            this.Size = new System.Drawing.Size(215, 113);
            this.eventTypeBox.ResumeLayout(false);
            this.eventTypeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox eventTypeBox;
        private System.Windows.Forms.RadioButton radioPutDown;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.RadioButton radioStandUp;
        private System.Windows.Forms.RadioButton radioGrab;
        private System.Windows.Forms.RadioButton radioMove;
        private System.Windows.Forms.RadioButton radioHalt;
        private System.Windows.Forms.RadioButton radioSitDown;


    }
}
