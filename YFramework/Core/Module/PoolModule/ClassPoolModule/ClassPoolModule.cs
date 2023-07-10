using System;
using System.Collections.Generic;

namespace YFramework
{
    public static class ClassPoolModule<T> where T : class, IReset, new()
    {
        private static Dictionary<Type, Stack<T>> mClassPoolDict = new Dictionary<Type, Stack<T>>();
        public static T Pop() 
        {
            Type type =  typeof(T);
            if (!mClassPoolDict.ContainsKey(type)) 
            {
                mClassPoolDict.Add(type,new Stack<T>());
            }
            T value = null;
            if (mClassPoolDict[type].Count == 0)
            {
                value = new T();
            }
            else 
            {
                value = mClassPoolDict[type].Pop();
            }
            return value;
        }
        public static void Push(T target) 
        {
            if (target == null) return;
            Type type = target.GetType();
            if (!mClassPoolDict.ContainsKey(type))
            {
                mClassPoolDict.Add(type, new Stack<T>());
            }
            target.Reset();
            mClassPoolDict[type].Push(target);
        }
    }
}
