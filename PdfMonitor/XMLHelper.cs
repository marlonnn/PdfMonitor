using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PdfMonitor
{
    public class XMLHelper
    {
        private static XMLHelper helper;
        public static XMLHelper Instance
        {
            get
            {
                if (helper == null)
                {
                    helper = new XMLHelper();
                }
                return helper;
            }
        }
        public void WriteXML<T>(Object obj, string path)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(T));
                System.IO.FileStream file = System.IO.File.Create(path);
                writer.Serialize(file, obj);
                file.Close();
            }
            catch (Exception ex) { }
        }

        public T ReadXML<T>(string path)
        {
            XmlSerializer reader = new XmlSerializer(typeof(T));
            StreamReader file = new StreamReader(path);
            T obj = (T)reader.Deserialize(file);
            file.Close();
            return obj;
        }
    }
}
