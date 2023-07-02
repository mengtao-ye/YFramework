using System;

namespace YFramework
{
    public static  class Fix32
    {
        private const float ONE = 1000;
        public const int LEN = 4;//当前数据的字节长度
        public static float ToValue(byte[] data)
        {
            return ToValue(data,0);
        }
        public static float ToValue(byte[] data,int startIndex)
        {
            if (data == null ) return 0;
            return BitConverter.ToInt32(data, startIndex) / ONE;
        }
        public static byte[] ToByte(float data)
        {
            return BitConverter.GetBytes ((int)( data * ONE));
        }
    }
}
