using System.Collections.Generic;

namespace YFramework
{
    public static class ListModule<TValue>
    {
        private static List<TValue> mCacheList = new List< TValue>();
        public static List<TValue> list { get { return mCacheList; } }
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Add(TValue value)
        {
            if (mCacheList.Contains(value)) return;
            mCacheList.Add(value);
        }
        /// <summary>
        /// 清空缓存数据
        /// </summary>
        public static void Clear() 
        {
            mCacheList.Clear();
        }
    }
}
