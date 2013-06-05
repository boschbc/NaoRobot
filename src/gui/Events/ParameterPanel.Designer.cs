namespace Naovigate.GUI.Events
{
    partial class ParameterPanel
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
            this.tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.doneButton = new System.Windows.Forms.Button();
            this.contentTabelPanel = new System.Windows.Forms.TableLayoutPanel();
            this.boundingBox.SuspendLayout();
            this.tableLayoutPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // boundingBox
            // 
            this.boundingBox.AutoSize = true;
            this.boundingBox.Controls.Add(this.tableLayoutPanel);
            this.boundingBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.boundingBox.Location = new System.Drawing.Point(0, 0);
            this.boundingBox.Name = "boundingBox";
            this.boundingBox.Size = new System.Drawing.Size(150, 150);
            this.boundingBox.TabIndex = 0;
            this.boundingBox.TabStop = false;
            this.boundingBox.Text = "Parameters";
            // 
            // tableLayoutPanel
            // 
            this.tableLayoutPanel.AutoSize = true;
            this.tableLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel.ColumnCount = 1;
            this.tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel.Controls.Add(this.contentTabelPanel, 0, 0);
            this.tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel.Name = "tableLayoutPanel";
            this.tableLayoutPanel.RowCount = 2;
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel.Size = new System.Drawing.Size(144, 131);
            this.tableLayoutPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.doneButton);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(92, 99);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(49, 29);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // doneButton
            // 
            this.doneButton.AutoSize = true;
            this.doneButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.doneButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.doneButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.doneButton.Location = new System.Drawing.Point(3, 3);
            this.doneButton.Name = "doneButton";
            this.doneButton.Size = new System.Drawing.Size(43, 23);
            this.doneButton.TabIndex = 2;
            this.doneButton.Text = "Done";
            this.doneButton.UseVisualStyleBackColor = true;
            // 
            // contentTabelPanel
            // 
            this.contentTabelPanel.AutoSize = true;
            this.contentTabelPanel.ColumnCount = 2;
            this.contentTabelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contentTabelPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.contentTabelPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contentTabelPanel.Location = new System.Drawing.Point(3, 3);
            this.contentTabelPanel.Name = "contentTabelPanel";
            this.contentTabelPanel.RowCount = 1;
            this.contentTabelPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.contentTabelPanel.Size = new System.Drawing.Size(138, 90);
            this.contentTabelPanel.TabIndex = 2;
            // 
            // ParameterPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.boundingBox);
            this.Name = "ParameterPanel";
            this.boundingBox.ResumeLayout(false);
            this.boundingBox.PerformLayout();
            this.tableLayoutPanel.ResumeLayout(false);
            this.tableLayoutPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox boundingBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button doneButton;
        private System.Windows.Forms.TableLayoutPanel contentTabelPanel;

    }
}
