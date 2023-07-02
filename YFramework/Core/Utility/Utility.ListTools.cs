using System;
using System.Collections.Generic;

namespace YFramework
{
    public partial class Utility
    {
        public static class ListTools
        {
            public static byte[] GetBytes(List<byte[]> data)
            {
                if (data == null || data.Count == 0) return null;
                int length = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == null)
                    {
                        length += 2;
                    }
                    else
                    {
                        length += (data[i].Length + 2);
                    }

                }
                byte[] concat = new byte[length];
                int index = 0;
                byte[] len = null;
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i] == null)
                    {
                        concat[index++] = 0;
                        concat[index++] = 0;
                    }
                    else
                    {
                        len = BitConverter.GetBytes((ushort)data[i].Length);
                        concat[index++] = len[0];
                        concat[index++] = len[1];
                        for (int j = 0; j < data[i].Length; j++)
                        {
                            concat[index++] = data[i][j];
                        }
                    }
                }
                return concat;
            }
            public static List<byte[]> ToList(byte[] bytes, int startIndex = 0)
            {
                if (bytes == null || bytes.Length == 0 || bytes.Length <= startIndex) return null;
                ushort length = 0;
                List<byte[]> data = new List<byte[]>();
                for (int i = startIndex; i < bytes.Length; i++)
                {
                    length = BitConverter.ToUInt16(bytes, i);
                    byte[] tempByte = new byte[0];
                    if (length != 0)
                    {
                        tempByte = new byte[length];
                        for (int j = 0; j < length; j++)
                        {
                            tempByte[j] = bytes[i + 2 + j];
                        }
                    }
                    data.Add(tempByte);
                    i += length + 1;
                }
                return data;
            }


        }
    }
}
