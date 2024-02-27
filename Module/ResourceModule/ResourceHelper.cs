using System;
using Object = UnityEngine.Object;

namespace YFramework
{
    public abstract class ResourceHelper
    {
        public static ResourceHelper Instance;
        protected abstract T Load<T>(string assetPath) where T :Object;
        protected abstract void AsyncLoad<T>(string assetPath,Action<T> callBack) where T : Object;
        /// <summary>
        /// 同步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        public static T LoadAsset<T>(string assetPath) where T : Object
        {
            return Instance.Load<T>(assetPath);
        }
        /// <summary>
        /// 异步加载资源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <param name="callBack"></param>
        public static void AsyncLoadAsset<T>(string assetPath, Action<T> callBack) where T : Object
        {
            Instance. AsyncLoad(assetPath, callBack);
        }
    }
}
