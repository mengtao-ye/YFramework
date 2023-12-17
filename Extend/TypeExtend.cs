using System;

namespace YFramework
{
    public static class TypeExtend
    {
        /// <summary>
        /// 获取命名空间加类名组合的名称
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Namespace_Name(this Type t)
        {
            if (t == null) return null;
            return t.Namespace + "." + t.Name;
        }
    }
}
