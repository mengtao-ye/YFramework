using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 数据模块
    /// </summary>
    public static class DictionaryModule<TKey, TData>
    {
        private static Dictionary<TKey, TData> mAssetDict = new Dictionary<TKey, TData>();
        public static Dictionary<TKey, TData> data { get { return mAssetDict; } }
        /// <summary>
        /// 添加资源
        /// </summary>
        /// <param name="key"></param>
        /// <param name="asset"></param>
        public static void Add(TKey key, TData asset)
        {
            if (mAssetDict.ContainsKey(key)) return;
            if (asset == null) return;
            mAssetDict.Add(key, asset);
        }
        /// <summary>
        /// 移除资源
        /// </summary>
        /// <param name="key"></param>
        public static void Remove(TKey key)
        {
            if (!mAssetDict.ContainsKey(key)) return;
            mAssetDict.Remove(key);
        }
        /// <summary>
        /// 是否包含资源
        /// </summary>
        /// <param name="key"></param>
        public static bool IsContains(TKey key)
        {
            return mAssetDict.ContainsKey(key);
        }
        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TData Get(TKey key)
        {
            if (!IsContains(key)) return default(TData);
            return mAssetDict[key];
        }
        /// <summary>
        /// 清空数据
        /// </summary>
        public static void Clear()
        {
            mAssetDict.Clear();
        }
    }
}
