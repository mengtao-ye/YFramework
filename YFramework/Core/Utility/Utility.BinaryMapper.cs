using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace YFramework.Core.Utility
{
    public partial class Utility
    {
        public static class BinaryMapper
        {
            public static T ToObject<T>(byte[] data) where T : class
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream(data))
                {
                    return binaryFormatter.Deserialize(memoryStream) as T;
                }
            }
            public static byte[] ToBinary(object obj)
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    binaryFormatter.Serialize(memoryStream, obj);
                    return memoryStream.ToArray();
                }
            }
            public static byte[] GetBytes(object data)
            {
                if (data.GetType().Name == typeof(string).Name)
                {
                    return Encoding.UTF8.GetBytes(data.ToString());
                }
                if (data.GetType().Name == typeof(int).Name)
                {
                    return BitConverter.GetBytes(int.Parse(data.ToString()));
                }
                if (data.GetType().Name == typeof(long).Name)
                {
                    return BitConverter.GetBytes(long.Parse(data.ToString()));
                }
                if (data.GetType().Name == typeof(bool).Name)
                {
                    return new byte[] { (byte)(data.ToString() == true.ToString() ? 1 : 0) };
                }
                if (data.GetType().Name == typeof(short).Name)
                {
                    return BitConverter.GetBytes(short.Parse(data.ToString()));
                }
                if (data.GetType().Name == typeof(List<byte>).Name)
                {
                    return (data as List<byte>).ToArray();
                }
                if (data.GetType().Name == typeof(byte).Name)
                {
                    return new byte[] { (byte)data };
                }
                if (data.GetType().Name == typeof(float).Name)
                {
                    return new byte[] { (byte)data };
                }
                return null;
            }
        }
    }
}
