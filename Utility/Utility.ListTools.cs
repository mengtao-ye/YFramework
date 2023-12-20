using System;
using System.Collections.Generic;

namespace YFramework
{
    public partial class Utility
    {
        public static class ListTools
        {
            public static byte[] GetBytes(IListData<string> data)
            {
                if (data.IsNullOrEmpty()) return null;
                IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
                for (int i = 0; i < data.Count; i++)
                {
                    list.Add(data[i].ToBytes());
                }
                byte[] bytes = GetBytes(list);
                list.Recycle();
                return bytes;
            }
            public static byte[] GetBytes(IList<string> data)
            {
                if (data.IsNullOrEmpty()) return null;
                IListData<byte[]> list = ClassPool<ListData<byte[]>>.Pop();
                for (int i = 0; i < data.Count; i++)
                {
                    list.Add(data[i].ToBytes());
                }
                byte[] bytes = GetBytes(list);
                list.Recycle();
                return bytes;
            }
            public static byte[] GetBytes(IList<byte[]> data)
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
            public static byte[] GetBytes(IListData<byte[]> data)
            {
                if (data == null) return null;
                return GetBytes(data.list);
            }
            public static IListData<byte[]> ToList(byte[] bytes, int startIndex = 0)
            {
                if (bytes == null || bytes.Length == 0 || bytes.Length <= startIndex) return null;
                ushort length = 0;
                IListData<byte[]> data = ClassPool<ListData<byte[]>>.Pop();
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
            public static IList<byte[]> ToIList(byte[] bytes, int startIndex = 0)
            {
                if (bytes == null || bytes.Length == 0 || bytes.Length <= startIndex) return null;
                ushort length = 0;
                IList<byte[]> data = new List<byte[]>();
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
            public static IList<string> ToListString(byte[] bytes, int startIndex = 0)
            {
                if (bytes == null || bytes.Length == 0 || bytes.Length <= startIndex) return null;
                ushort length = 0;
                IList<string> data = new List<string>();
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
                    data.Add(tempByte.ToStr());
                    i += length + 1;
                }
                return data;
            }
        }
    }
}
