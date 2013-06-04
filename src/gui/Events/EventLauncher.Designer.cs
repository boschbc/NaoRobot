namespace Naovigate.GUI.Events
{
    partial class EventLauncher
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
            this.eventSelector = new System.Windows.Forms.ComboBox();
            this.postButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventSelector
            // 
            this.eventSelector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventSelector.FormattingEnabled = true;
            this.eventSelector.Location = new System.Drawing.Point(3, 3);
            this.eventSelector.Name = "eventSelector";
            this.eventSelector.Size = new System.Drawing.Size(121, 21);
            this.eventSelector.TabIndex = 0;
            // 
            // postButton
            // 
            this.postButton.AutoSize = true;
            this.postButton.Location = new System.Drawing.Point(130, 4);
            this.postButton.Name = "postButton";
            this.postButton.Size = new System.Drawing.Size(75, 23);
            this.postButton.TabIndex = 1;
            this.postButton.Text = "Post";
            this.postButton.UseVisualStyleBackColor = true;
            this.postButton.Click += new System.EventHandler(this.postButton_Click);
            // 
            // EventLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.postButton);
            this.Controls.Add(this.eventSelector);
            this.Name = "EventLauncher";
            this.Size = new System.Drawing.Size(208, 30);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox eventSelector;
        private System.Windows.Forms.Button postButton;
    }
}
