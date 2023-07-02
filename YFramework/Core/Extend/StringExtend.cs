namespace YFramework
{
    public static class StringExtend
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str) 
        {
            return string.IsNullOrEmpty(str);
        }
    }
}
