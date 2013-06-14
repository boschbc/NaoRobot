namespace Naovigate.GUI.Util
{
    partial class LoadCalibrationButton
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
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.dropdown = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "naocalib";
            this.openFileDialog.InitialDirectory = "../resources/naocalibs/";
            // 
            // dropdown
            // 
            this.dropdown.AutoCompleteCustomSource.AddRange(new string[] {
            "default"});
            this.dropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dropdown.FormattingEnabled = true;
            this.dropdown.Location = new System.Drawing.Point(3, 0);
            this.dropdown.Name = "dropdown";
            this.dropdown.Size = new System.Drawing.Size(93, 21);
            this.dropdown.TabIndex = 0;
            this.dropdown.SelectedIndexChanged += new System.EventHandler(this.dropdown_SelectedIndexChanged);
            // 
            // LoadCalibrationButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.dropdown);
            this.Name = "LoadCalibrationButton";
            this.Size = new System.Drawing.Size(99, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.ComboBox dropdown;
    }
}
