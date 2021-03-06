﻿namespace Naovigate.GUI.Events.Parameters
{
    partial class IntegerChooser
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
            this.value = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.value)).BeginInit();
            this.SuspendLayout();
            // 
            // value
            // 
            this.value.DecimalPlaces = 2;
            this.value.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.value.Location = new System.Drawing.Point(0, 0);
            this.value.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.value.Minimum = new decimal(new int[] {
            999,
            0,
            0,
            -2147483648});
            this.value.Name = "value";
            this.value.Size = new System.Drawing.Size(56, 20);
            this.value.TabIndex = 0;
            // 
            // IntegerChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.value);
            this.Name = "IntegerChooser";
            this.Size = new System.Drawing.Size(59, 23);
            ((System.ComponentModel.ISupportInitialize)(this.value)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown value;
    }
}
