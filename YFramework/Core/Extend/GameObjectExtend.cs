using UnityEngine;

namespace YFramework
{
    public static class GameObjectExtend
    {
        /// <summary>
        /// 检查组件是否存在
        /// </summary>
        public static bool HasComponent<T>(this GameObject @this) where T : Component
        {
            return @this.GetComponent<T>() != null;
        }
        /// <summary>
        /// 销毁自己
        /// </summary>
        /// <param name="this"></param>
        public static void DestroySelf(this GameObject @this)
        {
            GameObject.Destroy(@this);
        }
        /// <summary>
        /// 延迟销毁自己
        /// </summary>
        /// <param name="this"></param>
        /// <param name="t"></param>
        public static void DestroySelf(this GameObject @this, float t)
        {
            GameObject.Destroy(@this, t);
        }
        /// <summary>
        /// 销毁该对象的队友子对象
        /// </summary>
        /// <param name="target"></param>
        public static void DestoryAllChild(this GameObject target) {
            if (target == null) return;
            for (int i = target.transform.childCount - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(target.transform.GetChild(i).gameObject);
            }
        }
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="go"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public static GameObject InstantiateGameObject(this GameObject go, Transform parent = null) {
            if (go == null) return null;
            return GameObject.Instantiate(go, parent);
        }
        /// <summary>
        /// 设置物体的显示
        /// </summary>
        /// <param name="go"></param>
        /// <param name="active"></param>
        public static void SetAvtiveExtend(this GameObject go, bool active) {
            if (go == null) return;
            if (go.activeInHierarchy == active) return;
            go.SetActive(active);
        }
        /// <summary>
        /// 反转Active状态
        /// </summary>
        public static void ReversalActive(this GameObject go) {
            if (go == null) return;
            go.SetActive(!go.activeInHierarchy);
        }
    }
}
