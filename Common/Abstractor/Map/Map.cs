using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 配置数据
    /// </summary>
    /// <typeparam name="Tkey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public abstract class Map<Tkey, TValue> : IMap<Tkey, TValue>
    {
        /// <summary>
        /// 配置的数据集合
        /// </summary>
        private Dictionary<Tkey, TValue> mDataDict;
        /// <summary>
        /// 配置的数据Key值数组
        /// </summary>
        public ICollection<Tkey> Keys => mDataDict.Keys;
        /// <summary>
        /// 配置数据的个数
        /// </summary>
        public int Count => mDataDict.Count;
        public Map() 
        {
            mDataDict = new Dictionary<Tkey, TValue>();
            Config();
        }
        /// <summary>
        /// 配置数据
        /// </summary>
        protected abstract void Config();
        /// <summary>
        ///添加配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(Tkey key, TValue value)
        {
            if (!mDataDict.ContainsKey(key))
            {
                mDataDict.Add(key, value);
            }
            else {
                UnityEngine.Debug.LogError("已包含Key:" + key.ToString());
            }
        }
        /// <summary>
        /// 获取配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Get(Tkey key)
        {
            if (mDataDict.ContainsKey(key)) return mDataDict[key];
            UnityEngine.Debug.LogError("未找到Key:"+key.ToString());
            return default(TValue);
        }
        /// <summary>
        /// 是否包含配置的数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(Tkey key)
        {
            return  mDataDict.ContainsKey(key) ;
        }
        /// <summary>
        /// 移除配置数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public void Remove(Tkey key)
        {
            if (mDataDict.ContainsKey(key)) {
                mDataDict.Remove(key);
            }
        }
    }
}
