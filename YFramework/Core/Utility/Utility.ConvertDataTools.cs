using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFramework
{
    public partial class Utility
    {
        public static class ConvertDataTools
        {
            public static byte[] ToByte<T>(T value) where T : IDataConverter, new()
            {
                return value.ToBytes();
            }
            public static byte[] ToByte<T>(List<T> value) where T : IDataConverter, new()
            {
                if (value == null || value.Count == 0) return null;
                List<byte[]> byteList = new List<byte[]>();
                for (int i = 0; i < value.Count; i++)
                {
                    byteList.Add(value[i].ToBytes());
                }
                return ListTools.GetBytes(byteList);
            }

            public static T ToObject<T>(byte[] data) where T : IDataConverter, new()
            {
                T value = new T();
                value.ToValue(data);
                return value;
            }

            public static List<T> ToListObject<T>(byte[] data, int startIndex = 0) where T : IDataConverter, new()
            {
                if (data == null || data.Length == 0) return null;
                List<T> byteList = new List<T>();
                List<byte[]> listBytes = ListTools.ToList(data, startIndex);
                for (int i = 0; i < listBytes.Count; i++)
                {
                    byteList.Add(ToObject<T>(listBytes[i]));
                }
                return byteList;
            }
        }
    }
}
