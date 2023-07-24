namespace YFramework
{
    public static class StringExtend
    {

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static float ToFloat(this string str)
        {
            if (str == null) throw new System.Exception("str is null");
            return float.Parse(str);
        }
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
