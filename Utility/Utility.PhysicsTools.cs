using UnityEngine;

namespace YFramework
{
    public static class PhysicsTools
    {
        public static int MAX_DISTANCE = 100;//检测的距离
        public static int LAYER = LayerMask.GetMask("Default");//检测的层级
        private static Ray mRay;
        private static RaycastHit mHit;
        private static Camera mCamera;
        public static void InitCamera(Camera camera)
        {
            mCamera = camera; 
        }
        /// <summary>
        /// 获取射线的对象
        /// </summary>
        /// <returns></returns>
        public static GameObject Raycast()
        {
            if (mCamera is null) return null;
            Vector3 screenPos = Input.mousePosition;
            mRay = mCamera.ScreenPointToRay(screenPos);
            if (Physics.Raycast(mRay, out mHit, MAX_DISTANCE, LAYER))
            {
                return mHit.collider.gameObject;
            }
            return null;
        }
        /// <summary>
        /// 获取射线检测的对象身上的组件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Raycast<T>() where T : Component
        {
            GameObject go = Raycast();
            if (go == null) return default(T);
            T component = go.GetComponent<T>();
            if (component == null) return default(T);
            return component;
        }
    }
}
