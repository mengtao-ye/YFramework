using System;
using System.Collections.Generic;
using System.Text;

namespace YFramework
{
    /// <summary>
    /// 数组拓展器
    /// </summary>
    public static class CollectionExtend
    {
        /// <summary>
        /// 遍历字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action"></param>
        /// <param name="value"></param>
        public static void Foreach<TKey, TValue, T1, T2>(this IDictionary<TKey, TValue> dict, Action<TKey, TValue, T1, T2> action, T1 value, T2 value2)
        {
            if (dict.IsNullOrEmpty()) return;
            IEnumerator<KeyValuePair<TKey, TValue>> enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (action != null) action(enumerator.Current.Key, enumerator.Current.Value, value, value2);
            }
        }
        /// <summary>
        ///遍历字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action"></param>
        public static void Foreach<TKey, TValue, T>(this IDictionary<TKey, TValue> dict, Action<TKey, TValue, T> action, T value)
        {
            if (dict.IsNullOrEmpty()) return;
            IEnumerator<KeyValuePair<TKey, TValue>> enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (action != null) action(enumerator.Current.Key, enumerator.Current.Value, value);
            }
        }
        /// <summary>
        ///遍历字典
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="action"></param>
        public static void Foreach<TKey,TValue>(this IDictionary<TKey,TValue> dict,Action<TKey,TValue> action)
        {
            if (dict.IsNullOrEmpty()) return;
            IEnumerator<KeyValuePair<TKey, TValue>> enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext()) 
            {
                if (action != null) action(enumerator.Current.Key, enumerator.Current.Value);
            }
        }
        /// <summary>
        /// 获取数组里面最后一个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static T GetLastData<T>(this IList<T> collection)
        {
            if (collection.IsNullOrEmpty()) return default(T);
            return collection[collection.Count-1];
        }
        /// <summary>
        /// 是否为空或没数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection) {
            if (collection == null || collection.Count == 0) return true;
            return false;
        }
        /// <summary>
        /// 连接List集合数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> Concat<T>(this List<T> list1, List<T> list2)
        {
            if (list1.IsNullOrEmpty()) return list2;
            if (list2.IsNullOrEmpty()) return list1;
            List<T> list = new List<T>(list1.Count+list2.Count);
            for (int i = 0; i < list1.Count; i++)
            {
                list.Add(list1[i]);
            }
            for (int i = 0; i < list2.Count; i++)
            {
                list.Add(list2[i]);
            }
            return list;
        }
        /// <summary>
        /// 获取当前List内容的大小
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int GetBytesCount<T>(this IList<T> list)
        {
            if (list == null)
            {
                return 0;
            }
            int count = 0;
            for (int i = 0; i < list.Count; i++)
            {
                count += Encoding.UTF8.GetBytes(list[i].ToString()).Length;
            }
            return count;
        }
        /// <summary>
        ///  根据Key获取Value
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TValue TryGet<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key) {
            if (dict == null || dict.Count == 0 ) {
                return default(TValue);   
            }
            TValue value;
            if (dict.TryGetValue(key, out value)) {
                return value;
            }
            return default(TValue);
        }
        /// <summary>
        /// 打乱顺序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        public static void Disrupted<T>(this IList<T> collection)
        {
            int randomData1, randomData2;
            T tempData;
            for (int i = 0; i < 100; i++)
            {
                randomData1 = UnityEngine.Random.Range(0, collection.Count);
                randomData2 = UnityEngine.Random.Range(0, collection.Count);
                if (randomData1 != randomData2) {
                    tempData = collection[randomData1];
                    collection[randomData1] = collection[randomData2];
                    collection[randomData2] = tempData;
                }
            }
        }
        /// <summary>
        /// 检查下标是否正确
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static bool CheckIndex<T>(this IList<T> collection,int index)
        {
            if (collection.IsNullOrEmpty()) return false;
            if (index < collection.Count - 1 || index >= collection.Count)  return false;
            return true;
        }
    }
}