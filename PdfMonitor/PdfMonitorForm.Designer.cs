namespace PdfMonitor
{
    partial class PdfMonitorForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PdfMonitorForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxInputFolder = new System.Windows.Forms.TextBox();
            this.btnInputSelect = new System.Windows.Forms.Button();
            this.btnOutoutFolder = new System.Windows.Forms.Button();
            this.txtBoxOutputFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.显示ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "输入：";
            // 
            // txtBoxInputFolder
            // 
            this.txtBoxInputFolder.Location = new System.Drawing.Point(59, 24);
            this.txtBoxInputFolder.Name = "txtBoxInputFolder";
            this.txtBoxInputFolder.ReadOnly = true;
            this.txtBoxInputFolder.Size = new System.Drawing.Size(410, 21);
            this.txtBoxInputFolder.TabIndex = 1;
            // 
            // btnInputSelect
            // 
            this.btnInputSelect.Location = new System.Drawing.Point(475, 22);
            this.btnInputSelect.Name = "btnInputSelect";
            this.btnInputSelect.Size = new System.Drawing.Size(31, 23);
            this.btnInputSelect.TabIndex = 2;
            this.btnInputSelect.Text = "...";
            this.btnInputSelect.UseVisualStyleBackColor = true;
            this.btnInputSelect.Click += new System.EventHandler(this.btnInputSelect_Click);
            // 
            // btnOutoutFolder
            // 
            this.btnOutoutFolder.Location = new System.Drawing.Point(475, 60);
            this.btnOutoutFolder.Name = "btnOutoutFolder";
            this.btnOutoutFolder.Size = new System.Drawing.Size(31, 23);
            this.btnOutoutFolder.TabIndex = 5;
            this.btnOutoutFolder.Text = "...";
            this.btnOutoutFolder.UseVisualStyleBackColor = true;
            this.btnOutoutFolder.Click += new System.EventHandler(this.btnOutoutFolder_Click);
            // 
            // txtBoxOutputFolder
            // 
            this.txtBoxOutputFolder.Location = new System.Drawing.Point(59, 62);
            this.txtBoxOutputFolder.Name = "txtBoxOutputFolder";
            this.txtBoxOutputFolder.ReadOnly = true;
            this.txtBoxOutputFolder.Size = new System.Drawing.Size(410, 21);
            this.txtBoxOutputFolder.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "输出：";
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "PDF监控";
            this.notifyIcon.BalloonTipTitle = "PDF监控程序";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "PDF监控";
            //this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.显示ToolStripMenuItem,
            this.退出ToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(153, 70);
            // 
            // 显示ToolStripMenuItem
            // 
            this.显示ToolStripMenuItem.Name = "显示ToolStripMenuItem";
            this.显示ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.显示ToolStripMenuItem.Text = "显示";
            this.显示ToolStripMenuItem.Click += new System.EventHandler(this.显示ToolStripMenuItem_Click);
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // PdfMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(531, 102);
            this.Controls.Add(this.btnOutoutFolder);
            this.Controls.Add(this.txtBoxOutputFolder);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnInputSelect);
            this.Controls.Add(this.txtBoxInputFolder);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PdfMonitorForm";
            this.Text = "PDF监控";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PdfMonitorForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PdfMonitorForm_FormClosed);
            this.Load += new System.EventHandler(this.PdfMonitorForm_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxInputFolder;
        private System.Windows.Forms.Button btnInputSelect;
        private System.Windows.Forms.Button btnOutoutFolder;
        private System.Windows.Forms.TextBox txtBoxOutputFolder;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 显示ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
    }
}

