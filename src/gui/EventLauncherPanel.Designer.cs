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
            this.tableContainer = new System.Windows.Forms.TableLayoutPanel();
            this.eventTypeBox = new System.Windows.Forms.GroupBox();
            this.radioLook = new System.Windows.Forms.RadioButton();
            this.radioGrab = new System.Windows.Forms.RadioButton();
            this.radioMove = new System.Windows.Forms.RadioButton();
            this.launchButton = new System.Windows.Forms.Button();
            this.tableContainer.SuspendLayout();
            this.eventTypeBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableContainer
            // 
            this.tableContainer.AutoSize = true;
            this.tableContainer.ColumnCount = 1;
            this.tableContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableContainer.Controls.Add(this.eventTypeBox, 0, 0);
            this.tableContainer.Controls.Add(this.launchButton, 0, 1);
            this.tableContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableContainer.Location = new System.Drawing.Point(0, 0);
            this.tableContainer.Name = "tableContainer";
            this.tableContainer.RowCount = 2;
            this.tableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableContainer.Size = new System.Drawing.Size(206, 243);
            this.tableContainer.TabIndex = 0;
            // 
            // eventTypeBox
            // 
            this.eventTypeBox.AutoSize = true;
            this.eventTypeBox.Controls.Add(this.radioLook);
            this.eventTypeBox.Controls.Add(this.radioGrab);
            this.eventTypeBox.Controls.Add(this.radioMove);
            this.eventTypeBox.Location = new System.Drawing.Point(3, 3);
            this.eventTypeBox.Name = "eventTypeBox";
            this.eventTypeBox.Size = new System.Drawing.Size(64, 101);
            this.eventTypeBox.TabIndex = 5;
            this.eventTypeBox.TabStop = false;
            this.eventTypeBox.Text = "Event";
            // 
            // radioLook
            // 
            this.radioLook.AutoSize = true;
            this.radioLook.Location = new System.Drawing.Point(6, 65);
            this.radioLook.Name = "radioLook";
            this.radioLook.Size = new System.Drawing.Size(49, 17);
            this.radioLook.TabIndex = 2;
            this.radioLook.TabStop = true;
            this.radioLook.Text = "Look";
            this.radioLook.UseVisualStyleBackColor = true;
            // 
            // radioGrab
            // 
            this.radioGrab.AutoSize = true;
            this.radioGrab.Location = new System.Drawing.Point(6, 42);
            this.radioGrab.Name = "radioGrab";
            this.radioGrab.Size = new System.Drawing.Size(48, 17);
            this.radioGrab.TabIndex = 1;
            this.radioGrab.TabStop = true;
            this.radioGrab.Text = "Grab";
            this.radioGrab.UseVisualStyleBackColor = true;
            // 
            // radioMove
            // 
            this.radioMove.AutoSize = true;
            this.radioMove.Location = new System.Drawing.Point(6, 19);
            this.radioMove.Name = "radioMove";
            this.radioMove.Size = new System.Drawing.Size(52, 17);
            this.radioMove.TabIndex = 0;
            this.radioMove.TabStop = true;
            this.radioMove.Text = "Move";
            this.radioMove.UseVisualStyleBackColor = true;
            // 
            // launchButton
            // 
            this.launchButton.Location = new System.Drawing.Point(3, 110);
            this.launchButton.Name = "launchButton";
            this.launchButton.Size = new System.Drawing.Size(94, 23);
            this.launchButton.TabIndex = 4;
            this.launchButton.Text = "Launch Event";
            this.launchButton.UseVisualStyleBackColor = true;
            this.launchButton.Click += new System.EventHandler(this.launchButton_Click);
            // 
            // EventLauncherPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.tableContainer);
            this.Name = "EventLauncherPanel";
            this.Size = new System.Drawing.Size(206, 243);
            this.tableContainer.ResumeLayout(false);
            this.tableContainer.PerformLayout();
            this.eventTypeBox.ResumeLayout(false);
            this.eventTypeBox.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableContainer;
        private System.Windows.Forms.Button launchButton;
        private System.Windows.Forms.GroupBox eventTypeBox;
        private System.Windows.Forms.RadioButton radioLook;
        private System.Windows.Forms.RadioButton radioGrab;
        private System.Windows.Forms.RadioButton radioMove;

    }
}
