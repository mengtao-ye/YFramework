using System;
using System.Collections.Generic;

namespace YFramework
{
    public partial class Utility
    {
        public static class DictionaryTools
        {
            private static byte[] mTempData = null;
            private static int mIndex;
            private const int MAX_SIZE = 1000000;
            private static void Init()
            {
                mIndex = 0;
                if (mTempData == null)
                {
                    mTempData = new byte[MAX_SIZE];
                }
            }
            /// <summary>
            /// 将字典转换成数组 
            /// 前提条件：
            ///  1.value值数据长度不能超过256个字节
            /// </summary>
            /// <param name="dict"></param>
            /// <returns></returns>
            public static IDictionary<byte, byte[]> BytesToDictionary32(byte[] data)
            {
                if (data == null || data.Length == 0) return null;
                Dictionary<byte, byte[]> tempDict = new Dictionary<byte, byte[]>();
                int len = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    len = (int)BitConverter.ToUInt32(data, i + 1);//+1是事件Code占一个字节
                    tempDict.Add(data[i], ByteTools.SubBytes(data, i + 5, len));//+5是因为事件Code占了一个字节，长度占了四个字节
                    i += len + 4;//这里+4是因为上面i++还加了一个
                }
                return tempDict;
            }
            /// <summary>
            /// 将字典转换成数组 
            /// 前提条件：
            ///  1.value值数据长度不能超过256个字节
            /// </summary>
            /// <param name="dict"></param>
            /// <returns></returns>
            public static byte[] DictionaryToBytes32(IDictionary<byte, byte[]> dict)
            {
                if (dict == null || dict.Count == 0) return null;
                Init();
                byte[] len = null;
                foreach (var item in dict)
                {
                    mTempData[mIndex++] = item.Key;
                    //把value值的长度放入数组里面
                    if (item.Value == null)
                    {
                        mTempData[mIndex++] = 0;
                        mTempData[mIndex++] = 0;
                        mTempData[mIndex++] = 0;
                        mTempData[mIndex++] = 0;//这里加四次因为是用uint来存储长度的，占四个字节
                    }
                    else
                    {
                        len = BitConverter.GetBytes((uint)item.Value.Length);
                        for (int i = 0; i < 4; i++)
                        {
                            mTempData[mIndex++] = len[i];
                        }
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            mTempData[mIndex++] = item.Value[i];
                        }
                    }
                }
                byte[] tempData = new byte[mIndex];
                for (int i = 0; i < mIndex; i++)
                {
                    tempData[i] = mTempData[i];
                }
                return tempData;
            }
            /// <summary>
            /// 将字典转换成数组 
            /// 前提条件：
            ///  1.value值数据长度不能超过256个字节
            /// </summary>
            /// <param name="dict"></param>
            /// <returns></returns>
            public static IDictionary<byte, byte[]> BytesToDictionary(byte[] data)
            {
                if (data == null || data.Length == 0) return null;
                Dictionary<byte, byte[]> tempDict = new Dictionary<byte, byte[]>();
                ushort len = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    len = BitConverter.ToUInt16(data, i + 1);//+1是事件Code占一个字节
                    tempDict.Add(data[i], ByteTools.SubBytes(data, i + 3, len));//+3是因为事件Code占了一个字节，长度占了两个字节
                    i += len + 2;//这里+2是因为上面i++还加了一个
                }
                return tempDict;
            }
            /// <summary>
            /// 将字典转换成数组 
            /// 前提条件：
            ///  1.value值数据长度不能超过256个字节
            /// </summary>
            /// <param name="dict"></param>
            /// <returns></returns>
            public static byte[] DictionaryToBytes(IDictionary<byte, byte[]> dict)
            {
                if (dict == null || dict.Count == 0) return null;
                Init();
                byte[] len = null;
                foreach (var item in dict)
                {
                    mTempData[mIndex++] = item.Key;
                    //把value值的长度放入数组里面
                    if (item.Value == null)
                    {
                        mTempData[mIndex++] = 0;
                        mTempData[mIndex++] = 0;//这里加两次因为是用ushort来存储长度的，占两个字节
                    }
                    else
                    {
                        if (item.Value.Length >= ushort.MaxValue) return null;
                        len = BitConverter.GetBytes((ushort)item.Value.Length);
                        mTempData[mIndex++] = len[0];
                        mTempData[mIndex++] = len[1];//ushort占两个字节
                        for (int i = 0; i < item.Value.Length; i++)
                        {
                            mTempData[mIndex++] = item.Value[i];
                        }
                    }
                }
                byte[] tempData = new byte[mIndex];
                for (int i = 0; i < mIndex; i++)
                {
                    tempData[i] = mTempData[i];
                }
                return tempData;
            }
        }
    }
}
