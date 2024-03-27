using System.Collections.Generic;

namespace YFramework
{
    public interface IListData<T> : IPool
    {
        IList<T> list { get; set; }
        T this[int index] { get; set; }
        int Count { get; }
        void Add(T item);
        void Clear();
        bool Contains(T item);
        int IndexOf(T item);
        void Insert(int index, T item);
        bool Remove(T item);
        void RemoveAt(int index);
    }
}
