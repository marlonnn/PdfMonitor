using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMonitor
{
    [Serializable]
    public class MonitorFolder
    {
        private string folderToWatchFor;
        public string FolderToWatchFor
        {
            get { return this.folderToWatchFor; }
            set { this.folderToWatchFor = value; }
        }

        private string outputFolder;
        public string OutputFolder
        {
            get { return this.outputFolder; }
            set { this.outputFolder = value; }
        }
        public MonitorFolder() { }
        public MonitorFolder (string folderToWatchFor, string outputFolder)
        {
            this.folderToWatchFor = folderToWatchFor;
            this.outputFolder = outputFolder;
        }
    }
}
