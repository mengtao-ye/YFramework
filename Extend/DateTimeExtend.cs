using System;

namespace YFramework
{
    /// <summary>
    /// 时间拓展器
    /// </summary>
    public static class DateTimeExtend
    {
        /// <summary>
        /// 将时间转换成年月日
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDate(this DateTime dateTime) 
        {
            return dateTime.ToString("yyyy/dd/MM");
        }
        /// <summary>
        /// 将时间转换成年月日 时分秒
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }
    }
}
