using System;
using System.Collections.Generic;
using System.Text;
using static YFramework.Utility;

namespace YFramework
{
    /// <summary>
    /// 字节转换拓展
    /// </summary>
    public static class BytesConverterExtend
    {
        #region ToBytes
        public static byte[] ToBytes(this IList<int> @this)
        {
            return ListTools.GetBytes(@this);
        }
        public static byte[] ToBytes(this IList<long> @this)
        {
            return ListTools.GetBytes(@this);
        }
        /// <summary>
        /// 将字典转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this IDictionary<byte, byte[]> @this)
        {
            return DictionaryTools.DictionaryToBytes(@this);
        }
        /// <summary>
        /// 将字典转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this IDictionaryData<byte, byte[]> @this)
        {
            return DictionaryTools.DictionaryToBytes(@this);
        }
        /// <summary>
        /// 将字典转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes<T>(this IList<T> @this) where T : IDataConverter, new()
        {
            return ConverterDataTools.ToByte(@this);
        }
        /// <summary>
        /// 将字典转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes<T>(this IListData<T> @this) where T : IDataConverter, new()
        {
            return ConverterDataTools.ToByte(@this);
        }
        /// <summary>
        /// 将List集合转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this IList<byte[]> @this)
        {
            return ListTools.GetBytes(@this);
        }
        /// <summary>
        /// 将List集合转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this IListData<byte[]> @this)
        {
            return ListTools.GetBytes(@this);
        }
        /// <summary>
        /// int 类型转换成字节数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this int @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// long类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this long @this)
        {
            return BitConverter.GetBytes(@this);
        }

        /// <summary>
        /// ulong类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this ulong @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// double类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this double @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// uint类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this uint @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// short类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this short @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// ushort转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this ushort @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// byte类型转换成成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this byte @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// bool类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this bool @this)
        {
            return BitConverter.GetBytes(@this);
        }
        /// <summary>
        /// string 类型转换成byte数组
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this string @this)
        {
            return Encoding.UTF8.GetBytes(@this);
        }
        #endregion
        #region ToValue
        /// <summary>
        /// 将字节数组转换成IDictionaryData<byte, byte[]> 
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IDictionaryData<byte, byte[]> ToBytesDictionary(this byte[] @this)
        {
            return DictionaryTools.BytesToDictionary(@this);
        }
        /// <summary>
        /// 将字节数组转换成IListData<T> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IListData<T> ToListDataBytes<T>(this byte[] @this, int startIndex = 0) where T : IDataConverter, new()
        {
            return ConverterDataTools.ToListObject<T>(@this, startIndex);
        }
        /// <summary>
        /// 将字节数组转换成IListData<T> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IListData<T> ToListPoolDataBytes<T>(this byte[] @this) where T : class, IDataConverter, IPool, new()
        {
            return ConverterDataTools.ToListPoolObject<T>(@this);
        }
        /// <summary>
        /// 将字节数组转换成IListData<byte[]>
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IListData<byte[]> ToListBytes(this byte[] @this,int startIndex = 0)
        {
            return ListTools.ToList(@this, startIndex);
        }
        public static IListData<long> ToListLong(this byte[] @this)
        {
            return ListTools.ToLongList(@this);
        }
        public static IListData<int> ToListInt(this byte[] @this)
        {
            return ListTools.ToIntList(@this);
        }
        /// <summary>
        /// 将字节数组转换成IListData<byte[]>
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static IList<byte[]> ToIListBytes(this byte[] @this)
        {
            return ListTools.ToIList(@this);
        }
        /// <summary>
        ///  将字节数组转换成double
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static double ToDouble(this byte[] @this)
        {
            return BitConverter.ToDouble(@this, 0);
        }
        /// <summary>
        ///  将字节数组转换成long
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToLong(this byte[] @this)
        {
            return ToLong(@this,0) ;
        }
        /// <summary>
        ///  将字节数组转换成long
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static long ToLong(this byte[] @this,int startIndex)
        {
            return BitConverter.ToInt64(@this, startIndex);
        }
        /// <summary>
        ///  将字节数组转换成long
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ulong ToULong(this byte[] @this)
        {
            return BitConverter.ToUInt64(@this, 0);
        }
        /// <summary>
        ///  将字节数组转换成int
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static int ToInt(this byte[] @this,int startIndex = 0)
        {
            return BitConverter.ToInt32(@this, startIndex);
        }
        /// <summary>
        ///  将字节数组转换成uint
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static uint ToUInt(this byte[] @this)
        {
            return BitConverter.ToUInt32(@this, 0);
        }
        /// <summary>
        ///  将字节数组转换成short
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static short ToShort(this byte[] @this)
        {
            return ToShort(@this,0);
        }

        /// <summary>
        ///  将字节数组转换成short
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static short ToShort(this byte[] @this,int startIndex)
        {
            return BitConverter.ToInt16(@this, startIndex);
        }
        /// <summary>
        ///  将字节数组转换成ushort
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static ushort ToUShort(this byte[] @this)
        {
            return BitConverter.ToUInt16(@this, 0);
        }
        /// <summary>
        ///  将字节数组转换成byte
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static byte ToByte(this byte[] @this,int index = 0)
        {
            return @this[index];
        }
        /// <summary>
        ///  将字节数组转换成bool
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool ToBool(this byte[] @this)
        {
            return ToBool(@this,0);
        }
        /// <summary>
        ///  将字节数组转换成bool
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static bool ToBool(this byte[] @this,int index)
        {
            return @this[index] == 1;
        }
        /// <summary>
        ///  将字节数组转换成string
        /// </summary>
        /// <param name="this"></param>
        /// <returns></returns>
        public static string ToStr(this byte[] @this)
        {
            return Encoding.UTF8.GetString(@this);
        }
        #endregion
        #region Converter
        /// <summary>
        /// 将string 转换成long
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static long ToLong(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            long value = 0;
            if (long.TryParse(str, out value))
            {
                return value;
            }
            return value;
        }
        /// <summary>
        /// 将string 转换成Int
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            int value = 0;
            if (int.TryParse(str, out value)) 
            {
                return value;
            }
            return value;
        }
        /// <summary>
        /// 将string 转换成short
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static short ToShort(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            short value = 0;
            if (short.TryParse(str, out value))
            {
                return value;
            }
            return value;
        }
        /// <summary>
        /// 将string 转换成byte
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte ToByte(this string str)
        {
            if (str.IsNullOrEmpty()) return 0;
            byte value = 0;
            if (byte.TryParse(str, out value))
            {
                return value;
            }
            return value;
        }
        /// <summary>
        /// 将string 转换成bool
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            if (str.IsNullOrEmpty()) return false;
            return str.Equals("true") || str.Equals("True") || str.Equals("TRUE");
        }
        #endregion
    }
}
