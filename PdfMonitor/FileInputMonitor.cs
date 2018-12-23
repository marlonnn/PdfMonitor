using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PdfMonitor
{
    public class FileInputMonitor
    {
        private FileSystemWatcher fileSystemWatcher;
        private BoilerReportQueue boilerReportQueue;

        private BoilerReportQueue historyReportQueue;

        private bool renameRun = true;

        private Thread renameThread;

        private bool historyCheckRun = true;

        private Thread historyThread;

        public string OutputFolder
        {
            get { return Program.SysConfig.OutputFolder; }
            set
            {
                if (value != Program.SysConfig.OutputFolder)
                {
                    Program.SysConfig.OutputFolder = value;
                }
            }
        }

        public string FolderToWatchFor
        {
            get { return Program.SysConfig.FolderToWatchFor; }
            set
            {
                if (value != Program.SysConfig.FolderToWatchFor)
                {
                    Program.SysConfig.FolderToWatchFor = value;
                    fileSystemWatcher.Created -= FileCreated;
                    fileSystemWatcher = new FileSystemWatcher(Program.SysConfig.FolderToWatchFor);
                    fileSystemWatcher.EnableRaisingEvents = true;
                    fileSystemWatcher.Created += FileCreated;
                }
            }
        }

        public FileInputMonitor()
        {
            boilerReportQueue = Program.SysConfig.BoilerReportQueue;
            //historyReportQueue = Program.SysConfig.HistoryReportQueue;

            renameThread = new Thread(new ThreadStart(Rename));
            historyThread = new Thread(new ThreadStart(HistoryRename));
            fileSystemWatcher = new FileSystemWatcher(Program.SysConfig.FolderToWatchFor);
            fileSystemWatcher.EnableRaisingEvents = true;

            fileSystemWatcher.Created += FileCreated;

        }

        public void StartHistoryThread()
        {
            historyThread.Start();
        }

        public void StopHistoryThread()
        {
            historyCheckRun = false;
            historyThread.Abort();
            historyThread = null;
        }

        public void StartRenameThread()
        {
            renameThread.Start();
        }

        public void StopRenameThread()
        {
            renameRun = false;
            renameThread.Abort();
            renameThread = null;
        }

        private string GetLogInfo(BoilerReport br)
        {
            return string.Format("original file: {0} \r\n final file : {1}", br.OriginalFile, br.ReportName);
        }

        private void Rename()
        {
            while (true)
            {
                if (renameRun)
                {
                    var queue = Program.SysConfig.BoilerReportQueue;
                    if (queue != null && queue.Count > 0)
                    {
                        var boilerReports = queue.PopAll();
                        foreach (var br in boilerReports)
                        {
                            try
                            {
                                var destFileName = string.Format("{0}\\{1}", Program.SysConfig.OutputFolder, br.ReportName);
                                if (!File.Exists(destFileName))
                                {
                                    System.IO.File.Copy(br.OriginalFile, destFileName);
                                    LogHelper.GetLogger<PdfMonitorForm>().Debug(GetLogInfo(br));
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                        }
                    }
                    Thread.Sleep(500);
                }
            }
        }

        private void HistoryRename()
        {
            while (true)
            {
                if (historyCheckRun)
                {
                    var queue = Program.SysConfig.HistoryOriginalQueue;
                    if (queue != null && queue.Count > 0)
                    {
                        var HistoryReportsPaths = queue.PopAll();
                        foreach (var history in HistoryReportsPaths)
                        {
                            var originalFile = history;

                            try
                            {
                                Thread.Sleep(200);
                                string originalText = ExtractTextFromPdf(originalFile);
                                //string re1 = ".*?"; // Non-greedy match on filler
                                string re2 = "((\\d+)(?:[\u4e00-\u9fa5]+))";   // Word 1
                                string re3 = "(\\s+)";  // White Space 1
                                string re4 = "(\\d+)";   // Any Single Digit 1
                                string re5 = "(\\s+)";  // White Space 2
                                string re6 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";   // YYYYMMDD 1
                                string re7 = "( )"; // Any Single Character 1
                                string re8 = "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)";  // HourMinuteSec 1
                                Regex r = new Regex(re2 + re3 + re4 + re5 + re6 + re7 + re8, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                                Match match1 = r.Match(originalText);


                                //string r1 = ".*?"; // Non-greedy match on filler
                                string r2 = "((?:[\u4e00-\u9fa5]+))";   // Word 1
                                string r3 = "(\\s+)";  // White Space 1
                                string r4 = "(\\d+)";   // Any Single Digit 1
                                string r5 = "(\\s+)";  // White Space 2
                                string r6 = "((?:[\u4e00-\u9fa5]+))";   // Word 1
                                string r7 = "(\\s+)";  // White Space 2
                                string r8 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";   // YYYYMMDD 1
                                string r9 = "( )"; // Any Single Character 1
                                string r10 = "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)";  // HourMinuteSec 1

                                Regex rg = new Regex(r2 + r3 + r4 + r5 + r6 + r7 + r8 + r9 + r10, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                                Match match2 = rg.Match(originalText);

                                if (match2.Success && match1.Success)
                                {
                                    String deviceName = match1.Groups[1].ToString();
                                    String id = match1.Groups[4].ToString();
                                    string stime = string.Format("{0} {1}", match1.Groups[6].ToString(), match1.Groups[8].ToString());
                                    var startTime = Convert.ToDateTime(stime);

                                    string sn = match2.Groups[3].ToString();
                                    var eTime = string.Format("{0} {1}", match2.Groups[7].ToString(), match2.Groups[9].ToString());
                                    var endTime = Convert.ToDateTime(eTime);
                                    var br = new BoilerReport(originalFile, sn, deviceName, id, startTime, endTime);
                                    var destFileName = string.Format("{0}\\{1}", Program.SysConfig.OutputFolder, br.ReportName);
                                    if (!File.Exists(destFileName))
                                    {
                                        System.IO.File.Copy(br.OriginalFile, destFileName);
                                        LogHelper.GetLogger<PdfMonitorForm>().Debug(GetLogInfo(br));
                                    }
                                }
                                else
                                {
                                    LogHelper.GetLogger<PdfMonitorForm>().Debug(string.Format("Parse {0} File Fail", history));
                                }

                            }
                            catch (Exception ex)
                            {
                                LogHelper.GetLogger<PdfMonitorForm>().Debug(string.Format("Exception: {0} \n ", ex.Message));
                            }
                        }
                    }
                }
                Thread.Sleep(200);
            }
        }

        private void FileCreated(Object sender, FileSystemEventArgs e)
        {
            ParseUsingPDFBox(e);

        }

        public string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }

        private void ParseUsingPDFBox(FileSystemEventArgs e)
        {
            try
            {
                Thread.Sleep(200);
                string originalText = ExtractTextFromPdf(e.FullPath);

                //string re1 = ".*?"; // Non-greedy match on filler
                string re2 = "((\\d+)(?:[\u4e00-\u9fa5]+))";   // Word 1
                string re3 = "(\\s+)";  // White Space 1
                string re4 = "(\\d+)";   // Any Single Digit 1
                string re5 = "(\\s+)";  // White Space 2
                string re6 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";   // YYYYMMDD 1
                string re7 = "( )"; // Any Single Character 1
                string re8 = "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)";  // HourMinuteSec 1

                Regex r = new Regex(/*re1 +*/ re2 + re3 + re4 + re5 + re6 + re7 + re8, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match match1 = r.Match(originalText);


                //string r1 = ".*?"; // Non-greedy match on filler
                string r2 = "((?:[\u4e00-\u9fa5]+))";   // Word 1
                string r3 = "(\\s+)";  // White Space 1
                string r4 = "(\\d+)";   // Any Single Digit 1
                string r5 = "(\\s+)";  // White Space 2
                string r6 = "((?:[\u4e00-\u9fa5]+))";   // Word 1
                string r7 = "(\\s+)";  // White Space 2
                string r8 = "((?:(?:[1]{1}\\d{1}\\d{1}\\d{1})|(?:[2]{1}\\d{3}))[-:\\/.](?:[0]?[1-9]|[1][012])[-:\\/.](?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";   // YYYYMMDD 1
                string r9 = "( )"; // Any Single Character 1
                string r10 = "((?:(?:[0-1][0-9])|(?:[2][0-3])|(?:[0-9])):(?:[0-5][0-9])(?::[0-5][0-9])?(?:\\s?(?:am|AM|pm|PM))?)";	// HourMinuteSec 1

                Regex rg = new Regex(r2 + r3 + r4 + r5 + r6 + r7 + r8 + r9 + r10, RegexOptions.IgnoreCase | RegexOptions.Singleline);
                Match match2 = rg.Match(originalText);

                if (match2.Success && match1.Success)
                {
                    String deviceName = match1.Groups[1].ToString();
                    String id = match1.Groups[4].ToString();
                    string stime = string.Format("{0} {1}", match1.Groups[6].ToString(), match1.Groups[8].ToString());
                    var startTime = Convert.ToDateTime(stime);

                    string sn = match2.Groups[3].ToString();
                    var eTime = string.Format("{0} {1}", match2.Groups[7].ToString(), match2.Groups[9].ToString());
                    var endTime = Convert.ToDateTime(eTime);
                    boilerReportQueue.Push(new BoilerReport(e.FullPath, sn, deviceName, id, startTime, endTime));
                }
                else
                {
                    LogHelper.GetLogger<PdfMonitorForm>().Debug(string.Format("Parse {0} File Fail", e.FullPath));
                }

            }
            catch (Exception ex)
            {

            }

        }


        private void ProcessFile(String fileName)
        {
            FileStream inputFileStream;
            while (true)
            {
                try
                {
                    inputFileStream = new FileStream(fileName,
                        FileMode.Open, FileAccess.ReadWrite);
                    StreamReader reader = new StreamReader(inputFileStream);
                    Console.WriteLine(reader.ReadToEnd());
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(3000);
                }
            }
        }
    }
}
