using System;
using System.Collections.Generic;
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

        private static SysConfig _sysConfigOrigin;

        /// <summary>
        /// The original configuration about software and hardware
        /// </summary>
        public static SysConfig SysConfigOrigin
        {
            get { return _sysConfigOrigin; }
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
                //_sysConfig = new PdfMonitor.SysConfig();
                _sysConfig = SysConfig.Load();
                _sysConfigOrigin = SysConfig.Load();

                Application.Run(new PdfMonitorForm());
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
