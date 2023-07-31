using System.Collections.Generic;

namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// 字节工具
        /// </summary>
        public static class ByteTools
        {

            public static byte[] SubBytes(byte[] data, int startIndex)
            {
                if (data == null) return null;
                if (startIndex < 0) return null;
                if (data.Length <= startIndex) return null;
                byte[] temp = new byte[data.Length - startIndex];
                for (int i = startIndex; i < data.Length; i++)
                {
                    temp[i - startIndex] = data[i];
                }
                return temp;
            }

            public static byte[] SubBytes(byte[] data, int startIndex, int length)
            {
                if (data == null) return null;
                if (startIndex < 0 || length < 0) return null;
                if (data.Length <= startIndex) return null;
                if (data.Length < startIndex + length) return null;
                byte[] temp = new byte[length];
                for (int i = startIndex; i < startIndex + length; i++)
                {
                    temp[i - startIndex] = data[i];
                }
                return temp;
            }
            /// <summary>
            /// Byte 数组连接
            /// </summary>
            /// <param name="data1"></param>
            /// <param name="data2"></param>
            /// <returns></returns>
            public static byte[] Concat(byte[] data1, byte[] data2)
            {
                if (data1 == null) return data2;
                if (data2 == null) return data1;
                byte[] data = new byte[data1.Length + data2.Length];
                int index = 0;
                for (int i = 0; i < data1.Length; i++)
                {
                    data[index++] = data1[i];
                }
                for (int i = 0; i < data2.Length; i++)
                {
                    data[index++] = data2[i];
                }
                return data;
            }
            /// <summary>
            /// Byte数组连接
            /// </summary>
            /// <param name="data1"></param>
            /// <param name="data2"></param>
            /// <returns></returns>
            public static byte[] Concat(byte data1, byte[] data2)
            {
                if (data2 == null) return new byte[] { data1 };
                byte[] data = new byte[data2.Length + 1];
                data[0] = data1;
                for (int i = 0; i < data2.Length; i++)
                {
                    data[i + 1] = data2[i];
                }
                return data;
            }
            /// <summary>
            /// Byte数组连接
            /// </summary>
            /// <param name="data1"></param>
            /// <param name="data2"></param>
            /// <returns></returns>
            public static byte[] Concat(byte[] data1, byte data2)
            {
                if (data1 == null) return new byte[] { data2 };
                byte[] data = new byte[data1.Length + 1];
                for (int i = 0; i < data1.Length; i++)
                {
                    data[i + 1] = data1[i];
                }
                data[data1.Length] = data2;
                return data;
            }
            /// <summary>
            /// Byte数组连接
            /// </summary>
            /// <param name="data1"></param>
            /// <param name="data2"></param>
            /// <returns></returns>
            public static byte[] ConcatParam(params byte[][] list)
            {
                if (list == null || list.Length == 0) return null;
                int len = 0;
                for (int i = 0; i < list.Length; i++)
                {
                    len += list[i].Length;
                }
                byte[] data = new byte[len];
                int index = 0;
                for (int i = 0; i < list.Length; i++)
                {
                    if (list[i] != null)
                    {
                        for (int j = 0; j < list[i].Length; j++)
                        {
                            data[index++] = list[i][j];
                        }
                    }
                }
                return data;
            }
            /// <summary>
            /// Byte数组连接
            /// </summary>
            /// <param name="data1"></param>
            /// <param name="data2"></param>
            /// <returns></returns>
            public static byte[] Concat(IList<byte[]> bytes)
            {
                int len = 0;
                for (int i = 0; i < bytes.Count; i++)
                {
                    len += bytes[i].Length;
                }
                byte[] data = new byte[len];
                int index = 0;
                for (int i = 0; i < bytes.Count; i++)
                {
                    if (bytes[i] != null)
                    {
                        for (int j = 0; j < bytes[i].Length; j++)
                        {
                            data[index++] = bytes[i][j];
                        }
                    }
                }
                return data;
            }
            /// <summary>
            /// 判断两个数组是否相同
            /// </summary>
            /// <param name="a"></param>
            /// <param name="b"></param>
            /// <returns></returns>
            public static bool IsCompare(byte[] a, byte[] b)
            {
                if (a == null && b == null) return true;
                if (a == null && b != null) return false;
                if (a != null && b == null) return false;
                if (a.Length != b.Length) return false;
                for (int i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i]) return false;
                }
                return true;

            }
        }
    }

}