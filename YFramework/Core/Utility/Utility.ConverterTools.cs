using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// 数据转换工具
        /// </summary>
        public static class ConverterTools
        {
            #region XmlConverter
            public static T Xml_ToObject<T>(string data) where T : class
            {
                StringReader memoryStream = new StringReader(data);
                XmlSerializer xml = new XmlSerializer(typeof(T));
                T t = xml.Deserialize(memoryStream) as T;
                memoryStream.Close();
                memoryStream.Dispose();
                return t;
            }
            public static string Xml_ToValue(object obj)
            {
                XmlSerializer xml = new XmlSerializer(obj.GetType());
                StringWriter memoryStream = new StringWriter();
                xml.Serialize(memoryStream, obj);
                string msg = memoryStream.ToString();
                memoryStream.Close();
                memoryStream.Dispose();
                return msg;
            } 
            #endregion
            #region TimeConverter
            /// <summary>
            /// 获取当前时间与传入的数据之间的间隔
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static string GetDelayTime(string time)
            {
                return GetDelayTime(DateTime.Parse(time));
            }
            /// <summary>
            /// 获取当前时间与传入的数据之间的间隔
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static string GetDelayTime(DateTime time)
            {
                TimeSpan span = (DateTime.Now - time);
                if (span.Days != 0)
                {
                    if (span.Days > 365) return (span.Days / 365) + "年";
                    if (span.Days > 30) return (span.Days / 30) + "月";
                    return span.Days + "天";
                }
                if (span.Hours != 0) return span.Hours + "小时";
                if (span.Minutes != 0) return span.Minutes + "分钟";
                return span.Minutes + "秒";
            } 
            #endregion
            #region BinaryConverter
            public static T Binary_ToObject<T>(byte[] data) where T : class
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream(data))
                {
                    return binaryFormatter.Deserialize(memoryStream) as T;
                }
            }

            public static byte[] Binary_ToBinary(object obj)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, obj);
                    return memoryStream.ToArray();
                }
            } 
            #endregion
        }
    }
}
