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

            SysConfig.Save(this);
        }

        private void SetDefault()
        {
            boilerReportQueue = new BoilerReportQueue();
            //historyReportQueue = new BoilerReportQueue();
            historyOriginalQueue = new HistoryReportQueue();
            folderToWatchFor = string.Format("{0}\\input", System.Environment.CurrentDirectory);
            outputFolder = string.Format("{0}\\output", System.Environment.CurrentDirectory);

            //var folderToWatchFor = string.Format("{0}\\input", System.Environment.CurrentDirectory);
            //var outputFolder = string.Format("{0}\\output", System.Environment.CurrentDirectory);
            //MonitorFolder = new MonitorFolder(folderToWatchFor, outputFolder);
        }

        // set default value
        [OnDeserializing]
        private void OnDeserializing(StreamingContext sc)
        {
            SetDefault();
        }

        [OnDeserialized]
        private void OnDeserialized(StreamingContext sc)
        {

        }

        public static SysConfig Load()
        {
            SysConfig config = null;

            try
            {
                config = config.SerializeFromFile(filePath);
            }
            catch (Exception e)
            {
                config = new SysConfig();
            }

            return config;
        }

        public static void Save(SysConfig config, SysConfig configOrigin)
        {
            SysConfig configNewer = SysConfig.Load();

            //merge the sysConfig
            FieldInfo[] fieldsSysConfig = config.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (FieldInfo fieldSysConfig in fieldsSysConfig)
            {
                if (fieldSysConfig == null)
                    continue;
                if ((fieldSysConfig.Attributes & FieldAttributes.NotSerialized) != FieldAttributes.NotSerialized)
                {
                    object origin = fieldSysConfig.GetValue(configOrigin);
                    object modified = fieldSysConfig.GetValue(config);
                    object newer = fieldSysConfig.GetValue(configNewer);
                    CustomAttrs attr = (CustomAttrs)Attribute.GetCustomAttribute(fieldSysConfig, typeof(CustomAttrs));
                    if (attr != null && !attr.IfEntirelyModify)
                    {
                        object mergedProperty = MergeField(origin, modified, newer);
                        fieldSysConfig.SetValue(configNewer, mergedProperty);
                    }
                    else
                    {
                        if (!FieldEqual(origin, modified))
                        {
                            fieldSysConfig.SetValue(configNewer, modified);
                        }
                    }
                }
            }
            Save(configNewer);
        }

        public static void Save(SysConfig config)
        {
            if (config != null)
            {
                try
                {
                    config.SerializeToFile(filePath);
                }
                catch (System.Exception ex)
                {
                }
            }
        }

        public static bool FieldEqual(object origin, object modified)
        {
            return origin == modified || (origin != null && modified != null && modified.SerializeEqual(origin));
        }

        public static Object MergeField(Object objOrigin, Object objModified, Object objNewer)
        {
            if (objModified == objOrigin) return objNewer;      // both null

            if (objOrigin == null || objModified == null || objNewer == null) return objModified;

            FieldInfo[] fields = objModified.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            foreach (FieldInfo field in fields)
            {
                if (field == null) continue;

                // ignore notserialized field
                if ((field.Attributes & FieldAttributes.NotSerialized) == FieldAttributes.NotSerialized) continue;

                object origin = field.GetValue(objOrigin);
                object modified = field.GetValue(objModified);

                if (!FieldEqual(origin, modified))
                {
                    field.SetValue(objNewer, modified);
                }
            }
            return objNewer;
        }
    }

    class CustomAttrs : Attribute
    {
        #region constructor
        public CustomAttrs()
        {

        }

        public CustomAttrs(string key, object value)
        {
            switch (key)
            {
                case "IfEntirelyModify":
                    _ifEntirelyModify = Convert.ToBoolean(value);
                    break;
                default:
                    break;
            }
        }

        #endregion 

        #region private members
        private bool _ifEntirelyModify = false;

        #endregion

        #region public properties
        public bool IfEntirelyModify
        {
            get
            {
                return _ifEntirelyModify;
            }
            //set via constructor
        }
        #endregion
    }
}
