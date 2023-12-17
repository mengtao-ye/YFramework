using System.Collections.Generic;

namespace YFramework
{
    public abstract class BaseDictionaryData<TKey,TValue> : IDictionaryData<TKey,TValue>
    {
        protected IDictionary<TKey, TValue> mDict;
        public IDictionary<TKey, TValue> data { get { return mDict; } }
        public TValue this[TKey key]
        {
            get
            {
                return Get(key);
            }
            set
            {
                if (ContainsKey(key))
                {
                    mDict[key] = value;
                }
                else
                {
                    mDict.Add(key, value);
                }
            }
        }
        public int Count { get { return mDict.Count; } }
        public bool isPop { get; set; }
        public BaseDictionaryData()
        {
            mDict = new Dictionary<TKey, TValue>();
        }
        public void Add(TKey key, TValue value)
        {
            if (!mDict.ContainsKey(key))
            {
                mDict.Add(key, value);
            }
        }
        public TValue Get(TKey key)
        {
            if (mDict.ContainsKey(key))
            {
                return mDict[key];
            }
            else
            {
                return default(TValue);
            }
        }
        public bool ContainsKey(TKey key)
        {
            return mDict.ContainsKey(key);
        }
        public bool Remove(TKey key)
        {
            return mDict.Remove(key);
        }
        public virtual void PopPool()
        {
            isPop = true;
        }
        public virtual void PushPool()
        {
            isPop = false;
            mDict.Clear();
        }
        public abstract void Recycle();
        public void Clear()
        {
            mDict?.Clear();
        }
    }
}
