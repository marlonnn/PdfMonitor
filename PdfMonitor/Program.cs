using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfMonitor
{
    static class Program
    {
        private static SysConfig _sysConfig;

        /// <summary>
        /// The configure information about software and hardware
        /// </summary>
        public static SysConfig SysConfig
        {
            get { return _sysConfig; }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                _sysConfig = new PdfMonitor.SysConfig();

                //var folderToWatchFor = string.Format("{0}\\input", System.Environment.CurrentDirectory);
                //var outputFolder = string.Format("{0}\\output", System.Environment.CurrentDirectory);
                var filePath = string.Format("{0}\\Config\\MonitorFolder.xml", System.Environment.CurrentDirectory);
                //XMLHelper.Instance.WriteXML<MonitorFolder>(new MonitorFolder(folderToWatchFor, outputFolder), filePath);
                if (File.Exists(filePath))
                {
                    var monitorFolder = XMLHelper.Instance.ReadXML<MonitorFolder>(filePath);
                    if (monitorFolder != null)
                    {
                        if (monitorFolder.FolderToWatchFor != null && monitorFolder.OutputFolder != null)
                        {
                            if (Directory.Exists(monitorFolder.FolderToWatchFor) && Directory.Exists(monitorFolder.OutputFolder))
                            {
                                _sysConfig.MonitorFolder = monitorFolder;
                                Application.Run(new PdfMonitorForm());
                            }
                            else
                            {
                                if (MessageBox.Show("请检查输入/输出文件夹是否存在，程序将退出！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                                {
                                    Application.Exit();
                                }
                            }
                        }
                        else
                        {
                            if (MessageBox.Show("请检查系统配置文件是否存在，程序将退出！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                Application.Exit();
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("请检查系统配置文件是否存在，程序将退出！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    }
                }
                else
                {
                    if (MessageBox.Show("请检查系统配置文件是否存在，程序将退出！", "异常", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                GC.Collect();
                //环境退出
                Environment.Exit(0);
            }
        }
    }
}
