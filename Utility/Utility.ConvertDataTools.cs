using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// 转换工具
        /// </summary>
        public static class ConverterDataTools
        {
            #region ToBytes
            /// <summary>
            /// 将泛型对象转换成字节
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value"></param>
            /// <returns></returns>
            public static byte[] ToByte<T>(T value) where T : IDataConverter, new()
            {
                return value.ToBytes();
            }
            /// <summary>
            /// 将泛型数组转换成字节
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value"></param>
            /// <returns></returns>
            public static byte[] ToByte<T>(IListData<T> value) where T : IDataConverter, new()
            {
                if (value == null) return null;
                return ToByte(value.list);
            }
            /// <summary>
            /// 将泛型数组转换成字节
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="value"></param>
            /// <returns></returns>
            public static byte[] ToByte<T>(IList<T> value) where T : IDataConverter, new()
            {
                if (value == null || value.Count == 0) return null;
                IListData<byte[]> byteList = ClassPool<ListData<byte[]>>.Pop();
                for (int i = 0; i < value.Count; i++)
                {
                    byteList.Add(value[i].ToBytes());
                }
                byte[] datas = ListTools.GetBytes(byteList);
                byteList.Recycle();
                return datas;
            }

            #endregion
            #region ToValue

            /// <summary>
            /// 将字节转换成泛型对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="startIndex"></param>
            /// <returns></returns>
            public static T ToPoolObject<T>(byte[] data, int startIndex = 0) where T : class, IDataConverter, IPool, new()
            {
                T value = ClassPool<T>.Pop();
                value.ToValue(ByteTools.SubBytes(data, startIndex));
                return value;
            }

            /// <summary>
            /// 将字节转换成泛型对象
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="startIndex"></param>
            /// <returns></returns>
            public static T ToObject<T>(byte[] data, int startIndex = 0) where T : IDataConverter, new()
            {
                T value = new T();
                value.ToValue(ByteTools.SubBytes(data, startIndex));
                return value;
            }
            /// <summary>
            /// 将字节转换成泛型数组
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="startIndex"></param>
            /// <returns></returns>
            public static IListData<T> ToListObject<T>(byte[] data, int startIndex = 0) where T : IDataConverter, new()
            {
                if (data == null || data.Length == 0) return null;
                IListData<T> byteList = null;
                IListData<byte[]> listBytes = ListTools.ToList(data, startIndex);
                for (int i = 0; i < listBytes.Count; i++)
                {
                    if(byteList == null) byteList = ClassPool<ListData<T>>.Pop();
                    byteList.Add(ToObject<T>(listBytes[i]));
                }
                listBytes.Recycle();
                return byteList;
            }

            /// <summary>
            /// 将字节转换成泛型数组
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="data"></param>
            /// <param name="startIndex"></param>
            /// <returns></returns>
            public static IListData<T> ToListPoolObject<T>(byte[] data, int startIndex = 0) where T :class, IDataConverter,IPool, new()
            {
                if (data == null || data.Length == 0) return null;
                IListData<T> byteList = null;
                IListData<byte[]> listBytes = ListTools.ToList(data, startIndex);
                for (int i = 0; i < listBytes.Count; i++)
                {
                    if(byteList == null) byteList = ClassPool<ListPoolData<T>>.Pop();
                    T value = ToPoolObject<T>(listBytes[i]);
                    byteList.Add(value);
                }
                listBytes.Recycle();
                return byteList;
            }
            #endregion
        }
    }
}
