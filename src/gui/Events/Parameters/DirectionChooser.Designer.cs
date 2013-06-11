namespace Naovigate.GUI.Events.Parameters
{
    partial class DirectionChooser
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
            this.value = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // value
            // 
            this.value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.value.FormattingEnabled = true;
            this.value.Items.AddRange(new object[] {
            "UP",
            "DOWN",
            "LEFT",
            "RIGHT"});
            this.value.Location = new System.Drawing.Point(0, 0);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(121, 21);
            this.value.TabIndex = 0;
            // 
            // DirectionChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.value);
            this.Name = "DirectionChooser";
            this.Size = new System.Drawing.Size(124, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox value;
    }
}
