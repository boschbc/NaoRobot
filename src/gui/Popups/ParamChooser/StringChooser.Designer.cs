namespace Naovigate.GUI.Popups.ParamChooser
{
    partial class StringChooser
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
            this.value = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // value
            // 
            this.value.Location = new System.Drawing.Point(3, 3);
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(147, 20);
            this.value.TabIndex = 0;
            // 
            // StringParameterChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.value);
            this.Name = "StringParameterChooser";
            this.Size = new System.Drawing.Size(153, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox value;
    }
}
