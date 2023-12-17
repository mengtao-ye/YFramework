namespace YFramework
{
    public static class ObjectExtend
    {
        /// <summary>
        /// 将object作为泛型类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As_Struct<T>(this object obj) where T : struct
        {
            return (T)obj ;
        }
        /// <summary>
        /// 将object作为泛型类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T As<T>(this object obj) where T : class
        {
            return  obj as T;
        }
        /// <summary>
        /// 将object转换成泛型类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CastTo<T>(this object obj) where T : class
        {
            return (T)obj;
        }
        /// <summary>
        /// 将struct转换成泛型类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T CastTo_Struct<T>(this object obj) where T : struct
        {
            return (T)obj;
        }
    }
}
