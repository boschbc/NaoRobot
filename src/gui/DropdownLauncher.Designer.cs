namespace Naovigate.GUI
{
    partial class DropdownLauncher
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
            this.eventDropdown = new System.Windows.Forms.ComboBox();
            this.postEventButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // eventDropdown
            // 
            this.eventDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.eventDropdown.FormattingEnabled = true;
            this.eventDropdown.Items.AddRange(new object[] {
            "Error",
            "Failure",
            "Success",
            "Agent",
            "Location",
            "State",
            "See",
            "DistanceTo",
            "Holding"});
            this.eventDropdown.Location = new System.Drawing.Point(0, 3);
            this.eventDropdown.Name = "eventDropdown";
            this.eventDropdown.Size = new System.Drawing.Size(121, 21);
            this.eventDropdown.TabIndex = 0;
            // 
            // postEventButton
            // 
            this.postEventButton.Location = new System.Drawing.Point(127, 3);
            this.postEventButton.Name = "postEventButton";
            this.postEventButton.Size = new System.Drawing.Size(75, 23);
            this.postEventButton.TabIndex = 1;
            this.postEventButton.Text = "Post to Goal";
            this.postEventButton.UseVisualStyleBackColor = true;
            this.postEventButton.Click += new System.EventHandler(this.postEventButton_Click);
            // 
            // DropdownLauncher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.postEventButton);
            this.Controls.Add(this.eventDropdown);
            this.Name = "DropdownLauncher";
            this.Size = new System.Drawing.Size(205, 29);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox eventDropdown;
        private System.Windows.Forms.Button postEventButton;
    }
}
