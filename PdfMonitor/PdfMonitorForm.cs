using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfMonitor
{
    public partial class PdfMonitorForm : Form
    {
        private FileInputMonitor fileInputMonitor;
        private HistoryReportQueue historyOrignalQueue;
        public PdfMonitorForm()
        {
            InitializeComponent();

            fileInputMonitor = new FileInputMonitor();
            InitializeFolder();

            historyOrignalQueue = Program.SysConfig.HistoryOriginalQueue;
            LogHelper.GetLogger<PdfMonitorForm>().Debug("Start Monitor!");
            notifyIcon.ShowBalloonTip(1000);

        }

        private void InitializeFolder()
        {
            this.txtBoxInputFolder.Text = fileInputMonitor.FolderToWatchFor;
            this.txtBoxOutputFolder.Text = fileInputMonitor.OutputFolder;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
            notifyIcon.Visible = true;
            this.Hide();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;    //取消"关闭窗口"事件
                this.WindowState = FormWindowState.Minimized;    //使关闭时窗口向右下角缩小的效果
                notifyIcon.Visible = true;
                this.Hide();
                return;
            }
        }
        private void PdfMonitorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            LogHelper.GetLogger<PdfMonitorForm>().Debug("Stop Monitor!");
        }

        private void btnInputSelect_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK &&
                    !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    fileInputMonitor.FolderToWatchFor = fbd.SelectedPath;
                    this.txtBoxInputFolder.Text = fileInputMonitor.FolderToWatchFor;
                    CheckHistoryFiles(fbd.SelectedPath);
                }
            }
        }

        private void btnOutoutFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                if (result == DialogResult.OK &&
                    !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    fileInputMonitor.OutputFolder = fbd.SelectedPath;
                    this.txtBoxOutputFolder.Text = fileInputMonitor.OutputFolder;
                }
            }
        }

        private void CheckHistoryFiles(string filePath)
        {
            DirectoryInfo folder = new DirectoryInfo(filePath);
            FileInfo[] fileNames = folder.GetFiles("*.pdf");
            foreach(var files in fileNames)
            {
                historyOrignalQueue.Push(files.FullName);
            }
        }
        private void PdfMonitorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            fileInputMonitor.StopRenameThread();
            fileInputMonitor.StopHistoryThread();
        }

        private void PdfMonitorForm_Load(object sender, EventArgs e)
        {
            fileInputMonitor.StartRenameThread();
            fileInputMonitor.StartHistoryThread();

            CheckHistoryFiles(fileInputMonitor.FolderToWatchFor);
        }

        protected override void OnResize(EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void 显示ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                // 关闭所有的线程
                this.Dispose();
                this.Close();
            }
        }
    }
}
