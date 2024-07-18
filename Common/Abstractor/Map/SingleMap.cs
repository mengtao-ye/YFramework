using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 单例配置数据
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TThis"></typeparam>
    public abstract class SingleMap<TKey, TValue, TThis> : Singleton<TThis>, IMap<TKey, TValue> where TThis : class, new()
    {
        /// <summary>
        /// 配置的数据数组
        /// </summary>
        private Dictionary<TKey, TValue> mDataDict;
        /// <summary>
        /// 数据字典
        /// </summary>
        public Dictionary<TKey, TValue> data { get { return mDataDict; } }
        /// <summary>
        /// 配置数据Key值数组
        /// </summary>
        public ICollection<TKey> Keys => mDataDict.Keys;
        /// <summary>
        /// 配置的数据个数
        /// </summary>
        public int Count => mDataDict.Count;
        public SingleMap()
        {
            mDataDict = new Dictionary<TKey, TValue>();
            Config();
        }
        /// <summary>
        /// 配置数据
        /// </summary>
        protected abstract void Config();
        /// <summary>
        /// 添加配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (!mDataDict.ContainsKey(key))
            {
                mDataDict.Add(key, value);
            }
            else
            {
                UnityEngine.Debug.LogError("已包含Key:" + key.ToString());
            }
        }
        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Get(TKey key)
        {
            if (mDataDict.ContainsKey(key)) return mDataDict[key];
            UnityEngine.Debug.LogError("未找到Key:" + key.ToString());
            return default(TValue);
        }
        /// <summary>
        /// 是否包含配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(TKey key)
        {
            return mDataDict.ContainsKey(key);
        }
        /// <summary>
        /// 是否包含配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Remove(TKey key)
        {
            if (mDataDict.ContainsKey(key))
            {
                mDataDict.Remove(key);
            }
        }
    }
}
