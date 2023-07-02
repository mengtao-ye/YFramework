using System.Collections.Generic;

namespace YFramework
{
    public abstract class SingleMap<TKey, TValue, TThis> : Singleton<TThis>, IMap<TKey, TValue> where TThis : class, new()
    {
        private Dictionary<TKey, TValue> mDataDict;
        public ICollection<TKey> Keys => mDataDict.Keys;
        public int Count => mDataDict.Count;
        public SingleMap()
        {
            mDataDict = new Dictionary<TKey, TValue>();
            Config();
        }

        protected abstract void Config();

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

        public TValue Get(TKey key)
        {
            if (mDataDict.ContainsKey(key)) return mDataDict[key];
            UnityEngine.Debug.LogError("未找到Key:" + key.ToString());
            return default(TValue);
        }

        public bool Contains(TKey key)
        {
            return mDataDict.ContainsKey(key);
        }
    }
}
