using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PdfMonitor
{
    public class BoilerReport
    {
        //锅次号
        private string sn;
        public string SN
        {
            get { return this.sn; }
            set { this.sn = value; }
        }

        //设备名
        private string deviceName;
        public string DeviceName
        {
            get { return this.deviceName; }
            set { this.deviceName = value; }
        }

        //设备id
        private string id;
        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        }

        //原始文件名（包含完整路径）
        private string originalFile;
        public string OriginalFile
        {
            get { return this.originalFile; }
            set { this.originalFile = value; }
        }

        //开始时间
        private DateTime startTime;
        public DateTime StartTime
        {
            get { return this.startTime; }
            set { this.startTime = value; }
        }

        //结束时间
        private DateTime endTime;
        public DateTime EndTime
        {
            get { return this.endTime; }
            set { this.endTime = value; }
        }

        public BoilerReport(string originalFile, string sn, string deviceName, string id, DateTime startTime, DateTime endTime)
        {
            this.originalFile = originalFile;
            this.sn = sn;
            this.deviceName = deviceName;
            this.id = id;
            this.startTime = startTime;
            this.endTime = endTime;
        }

        public string ReportName
        {
            get
            {
                return string.Format("{0}_{1}_{2}_{3}.pdf", sn, deviceName, id, startTime.ToString("yyyyMMddHHmmss"));
            }
        }
    }
}
