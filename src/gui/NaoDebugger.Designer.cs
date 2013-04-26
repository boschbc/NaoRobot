namespace Naovigate.gui
{
    partial class NaoDebugger
    {
        /*
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

        private void InitializeMonitors(string ip, int port)
        {
            this.locationMonitor = new LocationMonitor(ip, port);
            this.debugDataTable.Controls.Add(this.locationMonitor, 0, 0);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainVBox = new System.Windows.Forms.TableLayoutPanel();
            this.debugDataTable = new System.Windows.Forms.TableLayoutPanel();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.mainVBox.SuspendLayout();
            this.debugDataTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainVBox
            // 
            this.mainVBox.ColumnCount = 1;
            this.mainVBox.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.mainVBox.Controls.Add(this.debugDataTable, 0, 1);
            this.mainVBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainVBox.Location = new System.Drawing.Point(0, 0);
            this.mainVBox.Name = "mainVBox";
            this.mainVBox.RowCount = 2;
            this.mainVBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.47899F));
            this.mainVBox.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.52101F));
            this.mainVBox.Size = new System.Drawing.Size(540, 476);
            this.mainVBox.TabIndex = 0;
            // 
            // debugDataTable
            // 
            this.debugDataTable.ColumnCount = 1;
            this.debugDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.debugDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.debugDataTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.debugDataTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.debugDataTable.Location = new System.Drawing.Point(3, 347);
            this.debugDataTable.Name = "debugDataTable";
            this.debugDataTable.RowCount = 5;
            this.debugDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.debugDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.debugDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.debugDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.debugDataTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.debugDataTable.Size = new System.Drawing.Size(534, 126);
            this.debugDataTable.TabIndex = 0;
            // 
            // updateTimer
            // 
            this.updateTimer.Enabled = true;
            this.updateTimer.Interval = 500;
            this.updateTimer.Tick += new System.EventHandler(this.updateTimer_Tick);
            // 
            // NaoDebugger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 476);
            this.Controls.Add(this.mainVBox);
            this.Name = "NaoDebugger";
            this.Text = "NaoDebugger";
            this.Load += new System.EventHandler(this.NaoDebugger_Load);
            this.mainVBox.ResumeLayout(false);
            this.debugDataTable.ResumeLayout(false);
            this.debugDataTable.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel mainVBox;
        private System.Windows.Forms.TableLayoutPanel debugDataTable;
        private System.Windows.Forms.Timer updateTimer;
        private LocationMonitor locationMonitor;
         * */
    }
}