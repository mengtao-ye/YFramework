using System.IO;
using System.Xml.Serialization;

namespace YFramework.Core.Utility
{
    public partial class Utility
    {
        public static class XmlMapper
        {
            public static string ToXml(object obj)
            {
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                StringWriter memoryStream = new StringWriter();
                xml.Serialize(memoryStream, obj);
                string msg = memoryStream.ToString();
                memoryStream.Close();
                return msg;
            }
            public static T ToObject<T>(string data) where T : class
            {
                StringReader memoryStream = new StringReader(data);
                XmlSerializer xml = new XmlSerializer(typeof(T));
                T t = xml.Deserialize(memoryStream) as T;
                memoryStream.Close();
                return t;
            }
        }
    }
}
