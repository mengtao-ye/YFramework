using System.Collections.Generic;

namespace YFramework
{
    public interface IDictionaryData <TKey,TValue> : IPool
    {
        IDictionary<TKey, TValue> data { get; }
        TValue this[TKey key] { get; set; }
        int Count { get; }
        void Add(TKey key, TValue value);
        TValue Get(TKey key);
        bool ContainsKey(TKey key);
        bool Remove(TKey key);
        void Clear();
    }
}
