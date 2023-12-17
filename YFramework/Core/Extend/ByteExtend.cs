using System;

namespace YFramework
{
    /// <summary>
    /// byte拓展器
    /// </summary>
    public static class ByteExtend
    {
        /// <summary>
        /// 获取指定下标位置及长度数组
        /// </summary>
        /// <param name="b"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetBytes(this byte[] b,int startIndex,int length) 
        {
            if (b.Length == 0 || startIndex < 0 || length < 0 || b.Length < startIndex + length) return null;
            byte[] temp = new byte[length];
            for (int i = startIndex; i < startIndex+ length; i++)
            {
                temp[i - startIndex] = b[startIndex];
            }
            return temp;
        }
        /// <summary>
        ///  获取指定下标位置及长度数组
        /// </summary>
        /// <param name="bs"></param>
        /// <param name="startIndex"></param>
        /// <param name="length"></param>
        /// <param name="verse">反向获取当前数组里面的值</param>
        /// <returns></returns>
        public static byte[] GetBytes(this byte[] bs, int startIndex, int length, bool verse = false)
        {
            if (bs.Length == 0)
            {
                throw new Exception("byte长度不能为0！");
            }
            if (bs.Length < length)
            {
                throw new Exception("byte数据长度不足！");
            }
            if (startIndex < 0 || startIndex > bs.Length - 1 || length - startIndex > bs.Length)
            {
                throw new Exception("开始下标错误！");
            }
            byte[] tempData = new byte[length];
            int index = 0;
            if (!verse)
            {
                for (int i = startIndex; i < startIndex + length; i++)
                {
                    tempData[index++] = bs[i];
                }
            }
            else
            {
                for (int i = startIndex + length - 1; i >= startIndex; i--)
                {
                    tempData[index++] = bs[i];
                }
            }
            return tempData;
        }
       /// <summary>
       /// 将byte类型转换成bool类型 当传入的对象==1时为true,否则为false
       /// </summary>
       /// <param name="b"></param>
       /// <returns></returns>
        public static bool ToBool(this byte b)
        {
            return b == 1;    
        }
    }
}
