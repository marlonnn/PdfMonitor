using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PdfMonitor
{
    [Serializable]
    public class SysConfig
    {
        [NonSerialized]
        private BoilerReportQueue boilerReportQueue;
        public BoilerReportQueue BoilerReportQueue
        {
            get { return this.boilerReportQueue; }
        }

        [NonSerialized]
        private HistoryReportQueue historyOriginalQueue;
        public HistoryReportQueue HistoryOriginalQueue
        {
            get { return this.historyOriginalQueue; }
        }

        //private string folderToWatchFor;
        //public string FolderToWatchFor
        //{
        //    get { return this.folderToWatchFor; }
        //    set { this.folderToWatchFor = value; }
        //}

        //private string outputFolder;
        //public string OutputFolder
        //{
        //    get { return this.outputFolder; }
        //    set { this.outputFolder = value; }
        //}

        private MonitorFolder monitorFolder;
        public MonitorFolder MonitorFolder
        {
            get { return this.monitorFolder; }
            set { this.monitorFolder = value; }
        }

        [NonSerialized]
        private static string filePath = Application.StartupPath + "\\MConfig";

        public SysConfig()
        {
            SetDefault();
        }

        private void SetDefault()
        {
            boilerReportQueue = new BoilerReportQueue();
            //historyReportQueue = new BoilerReportQueue();
            historyOriginalQueue = new HistoryReportQueue();

            //var folderToWatchFor = string.Format("{0}\\input", System.Environment.CurrentDirectory);
            //var outputFolder = string.Format("{0}\\output", System.Environment.CurrentDirectory);
            //MonitorFolder = new MonitorFolder(folderToWatchFor, outputFolder);
        }


    }
}
