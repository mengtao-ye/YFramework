using System;

namespace YFramework
{
    public static class DateTimeExtend
    {
        public static string ToDate(this DateTime dateTime) {
            return dateTime.ToString("yyyy/dd/MM");
        }
        public static string ToDateTime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy/dd/MM HH:mm:ss");
        }
    }
}
