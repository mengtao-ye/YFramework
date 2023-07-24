using System.Collections.Generic;

namespace YFramework
{
    public static class ClassPool<T> where T : class, IPool, new()
    {
        private static Dictionary<string, Stack<T>> mDict = new Dictionary<string, Stack<T>>();
        public static T Pop(params object[] obj)
        {
            string name = typeof(T).Namespace_Name();
            if (mDict.ContainsKey(name) && mDict[name].Count != 0)
            {
                T value = mDict[name].Pop();
                value.isPop = true;
                value.PopPool();
                return value;
            }
            else
            {
                T t = new T();
                t.isPop = true;
                t.PopPool();
                return t;
            }
        }
        public static void Push(T target)
        {
            if (target == null) return;
            if (!target.isPop) return;//已经回收了
            string name = target.GetType().Namespace_Name();
            if (!mDict.ContainsKey(name))
            {
                mDict.Add(name, new Stack<T>());
            }
            target.PushPool();
            target.isPop = false;
            mDict[name].Push(target);
        }
        public static int Count
        {
            get
            {
                string name = typeof(T).Namespace_Name();
                if (mDict.ContainsKey(name))
                    return mDict.Count;
                return 0;
            }
        }

        public static void ClearTarget()
        {
            string name = typeof(T).Namespace_Name();
            if (mDict.ContainsKey(name))
                mDict.Remove(name);
        }
        public static void ClearAll()
        {
            mDict.Clear();
        }
    }
}
