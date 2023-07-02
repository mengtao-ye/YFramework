using UnityEngine;

namespace YFramework
{
    public abstract class Resource
    {
        public static Resource Instance;
        protected abstract T Load<T>(string assetPath) where T : Object;
        public static T LoadAsset<T>(string assetPath) where T : Object
        {
            return Instance.Load<T>(assetPath);
        }
    }
}
