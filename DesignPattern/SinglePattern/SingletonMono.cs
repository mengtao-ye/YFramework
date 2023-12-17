using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// Mono单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        /// <summary>
        /// 单例对象
        /// </summary>
        private static T mInstance;
        /// <summary>
        /// 单例对象
        /// </summary>
        public static T Instance
        {
            get
            {
                if (mInstance == null)
                {
                    T[] ts = UnityEngine.Resources.FindObjectsOfTypeAll<T>();
                    if (ts.Length == 1)
                    {
                        mInstance = ts[0];
                        mInstance.name = typeof(T).Name;
                    }
                    else if (ts.Length == 0)
                    {
                        UnityEngine.GameObject gameObject = new UnityEngine.GameObject(typeof(T).Name);
                        mInstance = gameObject.AddComponent<T>();
                        UnityEngine.Debug.Log("未找到对应的单例："+typeof(T).Name+",在场景中新建了一个单例");
                    }
                    else if (ts.Length > 1)
                    {
                        for (int i = 1; i < ts.Length; i++)
                        {
                            Destroy(ts[i].gameObject);
                        }
                        mInstance = ts[0];
                        mInstance.name = typeof(T).Name;
                        UnityEngine.Debug.LogError("对应的单例：" + typeof(T).Name + "过多，删除了多余的单例对象");
                    }
                }
                return mInstance;
            }
            set {
                mInstance = value;
            }
        }
    }
}