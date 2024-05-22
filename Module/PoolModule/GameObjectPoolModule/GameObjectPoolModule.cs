using System;
using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// GameObject对象池
    /// </summary>
    public static class GameObjectPoolModule
    {
        private static Dictionary<string, Stack<IGameObjectPoolTarget>> mPoolDict;//存放对象池对象
        private static Dictionary<string, List<IGameObjectPoolTarget>> mPopTarget;//存放已经弹出来的对象
        private static bool mIsInit = false;
        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init()
        {
            mIsInit = true;
            mPopTarget = new Dictionary<string, List<IGameObjectPoolTarget>>();
            mPoolDict = new Dictionary<string, Stack<IGameObjectPoolTarget>>();
        }

        /// <summary>
        /// 异步弹出对象池里面的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>

        public static void AsyncPop<T,T2>( Transform parent, Action<T,T2> initSuccessCallBack,T2 value) where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;

            if (!mIsInit)
            {
                Init();
            }

            if (!mPoolDict.ContainsKey(typeName) || mPoolDict[typeName].Count == 0)

            {
                AsyncSpawnTarget<T,T2>(parent, initSuccessCallBack,value);
                return;
            }
            else
            {

                IGameObjectPoolTarget tempGo = mPoolDict[typeName].Pop();

                if (tempGo == null)
                {
                    AsyncSpawnTarget<T,T2>(parent, initSuccessCallBack,value);
                    return;
                }
                if (tempGo.Target == null)
                {
                    AsyncSpawnTarget<T,T2>(parent, initSuccessCallBack,value);
                    return;
                }

                tempGo.Target.transform.SetParent(parent, tempGo.isUI);
                tempGo.Pop();
                if (!mPopTarget.ContainsKey(typeName))
                {
                    mPopTarget.Add(typeName, new List<IGameObjectPoolTarget>());
                }
                mPopTarget[typeName].Add(tempGo);

                initSuccessCallBack?.Invoke(tempGo as T,value);
            }
        }

        /// <summary>
        /// 异步弹出对象池里面的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>

        public static void AsyncPop<T>( Transform parent ,Action<T> initSuccessCallBack) where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;

            if (!mIsInit)
            {
                Init();
            }

            if (!mPoolDict.ContainsKey(typeName) || mPoolDict[typeName].Count == 0)

            {
                AsyncSpawnTarget<T>(parent, initSuccessCallBack);
                return;
            }
            else
            {

                IGameObjectPoolTarget tempGo = mPoolDict[typeName].Pop();

                if (tempGo == null)
                {
                    AsyncSpawnTarget<T>(parent, initSuccessCallBack);
                    return;
                }
                if (tempGo.Target == null)
                {
                    AsyncSpawnTarget<T>(parent, initSuccessCallBack);
                    return;
                }

                tempGo.Target.transform.SetParent(parent, tempGo.isUI);
                tempGo.Pop();
                if (!mPopTarget.ContainsKey(typeName))
                {
                    mPopTarget.Add(typeName, new List<IGameObjectPoolTarget>());
                }
                mPopTarget[typeName].Add(tempGo);
                initSuccessCallBack?.Invoke( tempGo as T);
            }
        }
        /// <summary>
        /// 弹出对象池里面的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static T Pop<T>( Transform parent) where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;
            if (!mIsInit)
            {
                Init();
            }
            if (!mPoolDict.ContainsKey(typeName) || mPoolDict[typeName].Count == 0)
            {
                return SpawnTarget<T>(parent);
            }
            else
            {
                IGameObjectPoolTarget tempGo = mPoolDict[typeName].Pop();
                if (tempGo == null)
                {
                    return SpawnTarget<T>(parent);
                }
                if (tempGo.Target == null)
                {
                    return SpawnTarget<T>(parent);
                }
                tempGo.Target.transform.SetParent(parent, tempGo.isUI);
                tempGo.Pop();
                if (!mPopTarget.ContainsKey(typeName))
                {
                    mPopTarget.Add(typeName, new List<IGameObjectPoolTarget>());
                }
                mPopTarget[typeName].Add(tempGo);
                return tempGo as T;
            }
        }
        /// <summary>
        /// 生成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static void AsyncSpawnTarget<T,T2>(Transform parent, Action<T,T2> successCallBack,T2 value) where T : class, IGameObjectPoolTarget, new()
        {

            string typeName = typeof(T).Name;

            T temp = new T();
            if (temp == null)
            {
                return;
            }
            ResourceHelper.AsyncLoadAsset<GameObject>(temp.assetPath, (item) => {
                GameObject target = GameObject.Instantiate(item);

                target.transform.SetParent(parent, temp.isUI);
                temp.Init(target);
                temp.Pop();
                if (!mPopTarget.ContainsKey(typeName))
                {
                    mPopTarget.Add(typeName, new List<IGameObjectPoolTarget>());
                }
                mPopTarget[typeName].Add(temp);

                successCallBack?.Invoke(temp,value);
            });
        }
        /// <summary>
        /// 生成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static void AsyncSpawnTarget<T>(Transform parent,Action<T> successCallBack) where T : class, IGameObjectPoolTarget, new()
        {

            string typeName = typeof(T).Name;

            T temp = new T();
            if (temp == null)
            {
                return ;
            }
            ResourceHelper.AsyncLoadAsset<GameObject>(temp.assetPath, (item) => {
                GameObject target = GameObject.Instantiate(item);
                target.transform.SetParent(parent, temp.isUI);
                temp.Init(target);
                temp.Pop();
                if (!mPopTarget.ContainsKey(typeName))
                {
                    mPopTarget.Add(typeName, new List<IGameObjectPoolTarget>());
                }
                mPopTarget[typeName].Add(temp);

                successCallBack?.Invoke(temp);
            });
        }
       
        /// <summary>
        /// 生成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parent"></param>
        /// <returns></returns>
        private static T SpawnTarget<T>(Transform parent) where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;
            T temp = new T();
            if (temp == null)
            {
                return default(T);
            }
            GameObject target = ResourceHelper.LoadAsset<GameObject>(temp.assetPath);
            target =  GameObject.Instantiate(target,parent);

            temp.Init(target);
            temp.Pop();
            if (!mPopTarget.ContainsKey(typeName))
            {
                mPopTarget.Add(typeName, new List<IGameObjectPoolTarget>());
            }
            mPopTarget[typeName].Add(temp);
            return temp;
        }
        /// <summary>
        /// 获取当前池子里面的数据
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Stack<IGameObjectPoolTarget> Get<T>() where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;
            if (!mIsInit)
            {
                Init();
            }
            if (!mPoolDict.ContainsKey(typeName))
            {
                return null;
            }
            return mPoolDict[typeName];
        }

        /// <summary>
        /// 将指定类型全部放入栈中
        /// </summary>
        /// <param name="type"></param>
        public static void PushTarget<T>(System.Predicate<IGameObjectPoolTarget> predicate) where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;
            if (!mIsInit)
            {
                Init();
            }
            if (mPopTarget.ContainsKey(typeName))
            {
                if (!mPoolDict.ContainsKey(typeName))
                {
                    mPoolDict.Add(typeName, new Stack<IGameObjectPoolTarget>());
                }
                List<IGameObjectPoolTarget> targets = mPopTarget[typeName];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].GameObjectIsPop && predicate(targets[i]) )
                    {
                        targets[i].Push();
                        mPoolDict[typeName].Push(targets[i]);
                    }
                }
            }
        }

        /// <summary>
        /// 将指定类型全部放入栈中
        /// </summary>
        /// <param name="type"></param>
        public static void PushTarget<T>() where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;
            if (!mIsInit)
            {
                Init();
            }
            if (mPopTarget.ContainsKey(typeName))
            {
                if (!mPoolDict.ContainsKey(typeName))
                {
                    mPoolDict.Add(typeName, new Stack<IGameObjectPoolTarget>());
                }
                List<IGameObjectPoolTarget> targets = mPopTarget[typeName];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i].GameObjectIsPop)
                    {
                        targets[i].Push();
                        mPoolDict[typeName].Push(targets[i]);
                    }
                }
                mPopTarget[typeName].Clear();
            }
        }
        /// <summary>
        /// 放入对象
        /// </summary>
        /// <param name="target"></param>
        public static void Push(IGameObjectPoolTarget target)
        {
            if (target == null) return;
            string typeName = target.GetType().Name;
            if (!mIsInit)
            {
                Init();
            }
            if (!target.GameObjectIsPop) return;
            if (!mPoolDict.ContainsKey(typeName))
            {
                mPoolDict.Add(typeName, new Stack<IGameObjectPoolTarget>());
            }
            target.Push();
            mPoolDict[typeName].Push(target);
            if (mPopTarget.ContainsKey(typeName))
            {
                List<IGameObjectPoolTarget> targets = mPopTarget[typeName];
                for (int i = 0; i < targets.Count; i++)
                {
                    if (targets[i] == target)
                    {
                        mPopTarget[typeName].RemoveAt(i);
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
        }
        /// <summary>
        /// 清除目标对象
        /// </summary>
        /// <param name="type"></param>
        public static void ClearTarget<T>() where T : class, IGameObjectPoolTarget, new()
        {
            string typeName = typeof(T).Name;
            if (!mIsInit)
            {
                Init();
            }
            if (mPoolDict.ContainsKey(typeName))
            {
                mPoolDict.Remove(typeName);
            }
            if (mPopTarget.ContainsKey(typeName))
            {
                mPopTarget.Remove(typeName);
            }
        }
    }
}