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
        public static byte[] ToBytes(this IDictionary<byte, byte[]> @this)
        {
            return DictionaryTools.DictionaryToBytes(@this);
        }
        public static byte[] ToBytes<T>(this IList<T> @this) where T : IDataConverter, new()
        {
            return ConverterDataTools.ToByte(@this);
        }
        public static byte[] ToBytes(this IList<byte[]> @this)
        {
            return ListTools.GetBytes(@this);
        }
        public static byte[] ToBytes(this int @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this long @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this double @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this uint @this)
        {
            return BitConverter.GetBytes(@this);
        }

        public static byte[] ToBytes(this short @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this ushort @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this byte @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this bool @this)
        {
            return BitConverter.GetBytes(@this);
        }
        public static byte[] ToBytes(this string @this)
        {
            return Encoding.UTF8.GetBytes(@this);
        }
        #endregion
        #region ToValue
        public static IDictionary<byte, byte[]> ToBytesDictionary(this byte[] @this)
        {
            return DictionaryTools.BytesToDictionary(@this);
        }
        public static List<T> ToListBytes<T>(this byte[] @this) where T : IDataConverter, new()
        {
            return ConverterDataTools.ToListObject<T>(@this);
        }
        public static List<byte[]> ToListBytes(this byte[] @this)
        {
            return ListTools.ToList(@this);
        }
        public static double ToDouble(this byte[] @this)
        {
            return BitConverter.ToDouble(@this, 0);
        }
        public static long ToLong(this byte[] @this)
        {
            return BitConverter.ToInt64(@this, 0);
        }
        public static int ToInt(this byte[] @this)
        {
            return BitConverter.ToInt32(@this, 0);
        }
        public static uint ToUInt(this byte[] @this)
        {
            return BitConverter.ToUInt32(@this, 0);
        }

        public static short ToShort(this byte[] @this)
        {
            return BitConverter.ToInt16(@this, 0);
        }
        public static ushort ToUShort(this byte[] @this)
        {
            return BitConverter.ToUInt16(@this, 0);
        }
        public static byte ToByte(this byte[] @this)
        {
            return @this[0];
        }
        public static bool ToBool(this byte[] @this)
        {
            return @this[0] == 1;
        }
        public static string ToStr(this byte[] @this)
        {
            return Encoding.UTF8.GetString(@this);
        }
        #endregion
    }
}
