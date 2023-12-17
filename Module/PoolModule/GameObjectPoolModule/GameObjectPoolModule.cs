using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// GameObject对象池
    /// </summary>
    public static class GameObjectPoolModule
    {
        private static Dictionary<int, Stack<IGameObjectPoolTarget>> mPoolDict;//存放对象池对象
        private static Dictionary<int, List<IGameObjectPoolTarget>> mPopTarget;//存放已经弹出来的对象
        private static bool mIsInit = false;
        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init()
        {
            mIsInit = true;
            mPopTarget = new Dictionary<int, List<IGameObjectPoolTarget>>();
            mPoolDict = new Dictionary<int, Stack<IGameObjectPoolTarget>>();
        }
        /// <summary>
        /// 弹出对象池里面的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T Pop<T>(int type, Transform parent) where T : class, IGameObjectPoolTarget, new()
        {
            if (!mIsInit)
            {
                Init();
            }
            if (!mPoolDict.ContainsKey(type) || mPoolDict[type].Count == 0)
            {
                return SpawnTarget<T>(parent);
            }
            else
            {
                IGameObjectPoolTarget tempGo = mPoolDict[type].Pop();
                if (tempGo == null)
                {
                    return SpawnTarget<T>(parent);
                }
                if (tempGo.Target == null)
                {
                    return SpawnTarget<T>(parent);
                }
                tempGo.Target.transform.parent = parent;
                tempGo.Pop();
                if (!mPopTarget.ContainsKey(tempGo.Type))
                {
                    mPopTarget.Add(tempGo.Type, new List<IGameObjectPoolTarget>());
                }
                mPopTarget[tempGo.Type].Add(tempGo);
                return tempGo as T;
            }
        }
        /// <summary>
        /// 生成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static T SpawnTarget<T>(Transform parent) where T : class, IGameObjectPoolTarget, new()
        {
            T temp = new T();
            if (temp == null)
            {
                return default(T);
            }
            GameObject target = GameObject.Instantiate(temp.Original);
            target.transform.SetParent(parent);
            temp.Init(target);
            temp.Pop();
            if (!mPopTarget.ContainsKey(temp.Type))
            {
                mPopTarget.Add(temp.Type, new List<IGameObjectPoolTarget>());
            }
            mPopTarget[temp.Type].Add(temp);
            return temp;
        }
        /// <summary>
        /// 获取当前池子里面的数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Stack<IGameObjectPoolTarget> Get(int type)
        {
            if (!mIsInit)
            {
                Init();
            }
            if (!mPoolDict.ContainsKey(type))
            {
                return null;
            }
            return mPoolDict[type];
        }

        /// <summary>
        /// 将指定类型全部放入栈中
        /// </summary>
        /// <param name="type"></param>
        public static void PushTarget(int type,System.Predicate<IGameObjectPoolTarget> predicate)
        {
            if (!mIsInit)
            {
                Init();
            }
            if (mPopTarget.ContainsKey(type))
            {
                if (!mPoolDict.ContainsKey(type))
                {
                    mPoolDict.Add(type, new Stack<IGameObjectPoolTarget>());
                }
                List<IGameObjectPoolTarget> targets = mPopTarget[type];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].IsPop && predicate(targets[i]) )
                    {
                        targets[i].Push();
                        mPoolDict[type].Push(targets[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 将指定类型全部放入栈中
        /// </summary>
        /// <param name="type"></param>
        public static void PushTarget(int type)
        {
            if (!mIsInit)
            {
                Init();
            }
            if (mPopTarget.ContainsKey(type))
            {
                if (!mPoolDict.ContainsKey(type))
                {
                    mPoolDict.Add(type, new Stack<IGameObjectPoolTarget>());
                }
                List<IGameObjectPoolTarget> targets = mPopTarget[type];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].IsPop)
                    {
                        targets[i].Push();
                        mPoolDict[type].Push(targets[i]);
                    }
                }
                mPopTarget[type].Clear();
            }
        }
        /// <summary>
        /// 放入对象
        /// </summary>
        /// <param name="target"></param>
        public static void Push(IGameObjectPoolTarget target)
        {
            if (!mIsInit)
            {
                Init();
            }
            if (target == null) return;
            if (!mPoolDict.ContainsKey(target.Type))
            {
                mPoolDict.Add(target.Type, new Stack<IGameObjectPoolTarget>());
            }
            target.Push();
            mPoolDict[target.Type].Push(target);
            if (mPopTarget.ContainsKey(target.Type))
            {
                List<IGameObjectPoolTarget> targets = mPopTarget[target.Type];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i] == target)
                    {
                        mPopTarget[target.Type].RemoveAt(i);
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
            if (!mIsInit)
            {
                Init();
            }
            mPoolDict.Clear();
            mPopTarget.Clear();
        }
        /// <summary>
        /// 清除目标对象
        /// </summary>
        /// <param name="type"></param>
        public static void ClearTarget(int type)
        {
            if (!mIsInit)
            {
                Init();
            }
            if (mPoolDict.ContainsKey(type))
            {
                mPoolDict.Remove(type);
            }
            if (mPopTarget.ContainsKey(type))
            {
                mPopTarget.Remove(type);
            }
        }
    }
}