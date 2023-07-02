using System.Collections.Generic;

namespace YFramework
{
    public abstract class Map<Tkey, TValue> : IMap<Tkey, TValue>
    {
        private Dictionary<Tkey, TValue> mDataDict;
        public ICollection<Tkey> Keys => mDataDict.Keys;
        public int Count => mDataDict.Count;
        public Map() 
        {
            mDataDict = new Dictionary<Tkey, TValue>();
            Config();
        }
        protected abstract void Config();
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

        public TValue Get(Tkey key)
        {
            if (mDataDict.ContainsKey(key)) return mDataDict[key];
            UnityEngine.Debug.LogError("未找到Key:"+key.ToString());
            return default(TValue);
        }

        public bool Contains(Tkey key)
        {
            return  mDataDict.ContainsKey(key) ;
        }
    }
}
