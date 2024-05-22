using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// GameObject对象池
    /// </summary>
    public  class TargetPoolModule<T> where T : Object
    {
        private static Dictionary<string, Stack<T>> mPoolDict;//存放对象池对象
        private static Dictionary<string, List<T>> mPopTarget;//存放已经弹出来的对象
        private static Dictionary<string, T> mOriginalPair;//原始对象
        private static bool mIsInit = false;

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init()
        {
            mIsInit = true;
            mPopTarget = new Dictionary<string, List<T>>();
            mPoolDict = new Dictionary<string, Stack<T>>();
            mOriginalPair = new Dictionary<string, T>();
        }

        public static void AddPair(string key, T target)
        {
            if (key.IsNullOrEmpty())
            {
                LogHelper.LogError("TargetPoolModule.AddPair(string key,GameObject target) key is null");
                return;
            }
            if (target == null)
            {
                LogHelper.LogError("TargetPoolModule.AddPair(string key,GameObject target) target is null");
                return;
            }
            if (!mIsInit)
            {
                Init();
            }
            if (!mOriginalPair.ContainsKey(key))
            {
                mOriginalPair.Add(key, target);
            }
            else 
            {
                mOriginalPair[key] = target;
            }
        }

        /// <summary>
        /// 弹出对象池里面的对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T Pop(string key)
        {
            if (key.IsNullOrEmpty())
            {
                LogHelper.LogError("TargetPoolModule.Pop(string key,Transform parent) key is null");
                return null;
            }
           
            if (!mIsInit)
            {
                Init();
            }
            if (!mPoolDict.ContainsKey(key) || mPoolDict[key].Count == 0)
            {
                return SpawnTarget(key);
            }
            else
            {
                T tempGo = mPoolDict[key].Pop();
                if (tempGo == null)
                {
                    tempGo =  SpawnTarget(key);
                }
                if (!mPopTarget.ContainsKey(key))
                {
                    mPopTarget.Add(key, new List<T>());
                }
                mPopTarget[key].Add(tempGo);
                return tempGo ;
            }
        }
        /// <summary>
        /// 生成对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static T SpawnTarget(string key) 
        {
            if (key.IsNullOrEmpty())
            {
                LogHelper.LogError("TargetPoolModule.SpawnTarget(string key,Transform parent) key is null");
                return null;
            }
            if (mOriginalPair.ContainsKey(key))
            {
                T gameObject = mOriginalPair[key].InstantiateGameObject();
                return gameObject;
            }
            else
            {
                LogHelper.LogError("TargetPoolModule.SpawnTarget(string key,Transform parent) key:" + key + "is not add");
                return null;
            }
        }
  
        /// <summary>
        /// 将指定类型全部放入栈中
        /// </summary>
        /// <param name="type"></param>
        public static void PushTarget(string key) 
        {
            if (key.IsNullOrEmpty())
            {
                LogHelper.LogError("TargetPoolModule.PushTarget( string key) key is null");
                return;
            }
            if (!mIsInit)
            {
                Init();
            }
            if (mPopTarget.ContainsKey(key))
            {
                if (!mPoolDict.ContainsKey(key))
                {
                    mPoolDict.Add(key, new Stack<T>());
                }
                List<T> targets = mPopTarget[key];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i] !=null)
                    {
                        mPoolDict[key].Push(targets[i]);
                    }
                }
                mPopTarget[key].Clear();
            }
        }
        /// <summary>
        /// 放入对象
        /// </summary>
        /// <param name="target"></param>
        public static void Push(string key,T target)
        {
            if (target == null)
            {
                LogHelper.LogError("TargetPoolModule. Push(GameObject target) target is null");
                return ;
            }
            if (!mIsInit)
            {
                Init();
            }
            if (!mPoolDict.ContainsKey(key))
            {
                mPoolDict.Add(key, new Stack<T>());
            }
            mPoolDict[key].Push(target);
            if (mPopTarget.ContainsKey(key))
            {
                List<T> targets = mPopTarget[key];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i] == target)
                    {
                        mPopTarget[key].RemoveAt(i);
                        break;
                    }
                }
            }
        }
        /// <summary>
        /// 清除所有数据
        /// </summary>
        public static void Clear()
        {
            mPoolDict?.Clear();
            mPopTarget?.Clear();
            mOriginalPair?.Clear();
        }
        /// <summary>
        /// 清除目标对象
        /// </summary>
        /// <param name="type"></param>
        public static void ClearTarget(string key)
        {
            if (key.IsNullOrEmpty())
            {
                LogHelper.LogError("TargetPoolModule.ClearTarget(string key) key is null");
                return ;
            }
            if (!mIsInit)
            {
                Init();
            }
            if (mPoolDict.ContainsKey(key))
            {
                mPoolDict.Remove(key);
            }
            if (mPopTarget.ContainsKey(key))
            {
                mPopTarget.Remove(key);
            }
        }
    }
}