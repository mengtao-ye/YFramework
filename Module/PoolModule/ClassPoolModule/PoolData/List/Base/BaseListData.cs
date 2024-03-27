using System.Collections.Generic;

namespace YFramework
{
    public abstract class BaseListData<T> : IListData<T>
    {
        protected IList<T> mList;
        public IList<T> list { get { return mList; } set { mList = value; } }
        public T this[int index] { get { return mList[index]; } set { mList[index] = value; } }
        public int Count { get { return mList.Count; } }
        public bool isPop { get; set; }
        public BaseListData()
        {
            mList = new List<T>();
        }
        public void Add(T item)
        {
            mList.Add(item);
        }

        public void Clear()
        {
            mList.Clear();
        }
        public bool Contains(T item)
        {
            return mList.Contains(item);
        }
        public int IndexOf(T item)
        {
            return mList.IndexOf(item);
        }
        public void Insert(int index, T item)
        {
            mList.Insert(index, item);
        }
        public bool Remove(T item)
        {
            return mList.Remove(item);
        }
        public void RemoveAt(int index)
        {
            mList.RemoveAt(index);
        }
        
        public virtual void PopPool()
        {
            isPop = true;
        }
        public virtual void PushPool()
        {
            isPop = false;
            mList.Clear();
        }
        public abstract void Recycle();
    }
}
