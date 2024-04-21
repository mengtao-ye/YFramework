using UnityEngine;

namespace YFramework
{
    public static class TransformExtend
    {
        public static T AddComponent<T>(this Transform trans) where T : Component
        {
            if (trans == null ) return null;
            return trans.gameObject.AddComponent<T>();
        }

        /// <summary>
        /// 查找对象
        /// </summary>
        /// <typeparam name="T">查找的类型</typeparam>
        /// <param name="trans"></param>
        /// <param name="name">查找的对象名</param>
        /// <param name="includeInactive">是否包含不显示在场景中的对象</param>
        /// <returns></returns>
        public static T FindObject<T>(this Transform trans, string name, bool includeInactive = true) where T : Component
        {
            if (trans == null || name == null) return default(T);
            T[] ts = trans.GetComponentsInChildren<T>(includeInactive);
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i].name == name) return ts[i];
            }
            return default(T);
        }
        /// <summary>
        /// 查找对象
        /// </summary>
        /// <typeparam name="T">查找的类型</typeparam>
        /// <param name="trans"></param>
        /// <param name="name">查找的对象名</param>
        /// <param name="includeInactive">是否包含不显示在场景中的对象</param>
        /// <returns></returns>
        public static GameObject FindObject(this Transform trans, string name, bool includeInactive = true)
        {
            if (trans == null || name == null) return null;
            Transform[] ts = trans.GetComponentsInChildren<Transform>(includeInactive);
            for (int i = 0; i < ts.Length; i++)
            {
                if (ts[i].name == name) return ts[i].gameObject;
            }
            return null;
        }
        /// <summary>
        /// 递归查找对象
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Transform FindReverse(this Transform trans, string name)
        {
            if (trans == null || name == null) return null;
            Transform result = trans.Find(name);
            if (result == null)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    result = FindReverse(trans.GetChild(i), name);
                }
            }
            return result;
        }

        public static T FindT<T>(this Transform trans, string path)
        {
            if (trans == null || path == null) return default(T);
            return trans.Find(path).GetComponent<T>();
        }
        /// <summary>
        /// 递归查找对象
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static T FindReverse<T>(this Transform trans, string name) where T : Component
        {
            Transform target = trans.FindReverse(name);
            if (target != null)
            {
                if (target.GetComponent<T>() != null)
                    return target.GetComponent<T>();
            }
            return null;
        }
        /// <summary>
        /// 清除孩子对象
        /// </summary>
        /// <param name="trans"></param>
        public static void ClearChild(this Transform trans,int index = 0) {
            if (trans != null) {
                for (int i = trans.childCount-1; i>= index; i--)
                {
                    GameObject.DestroyImmediate(trans.GetChild(i).gameObject);
                }
            }
        }
        /// <summary>
        /// 重置参数
        /// </summary>
        /// <param name="trans"></param>
        public static void Reset(this Transform trans)
        {
            if (trans == null) return;
            trans.localPosition = Vector3.zero;
            trans.localRotation = Quaternion.identity;
            trans.localScale = Vector3.one;
        }
        /// <summary>
        /// 设置欧拉角X轴旋转
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="x"></param>
        public static void SetEulerAngle_X(this Transform trans ,float x)
        {
            trans.eulerAngles = new Vector3(x, trans.eulerAngles.y, trans.eulerAngles.z);
        }
        /// <summary>
        /// 设置欧拉角Y轴旋转
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="y"></param>
        public static void SetEulerAngle_Y(this Transform trans, float y)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, y, trans.eulerAngles.z);
        }
        /// <summary>
        /// 设置欧拉角Z轴旋转
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="z"></param>
        public static void SetEulerAngle_Z(this Transform trans, float z)
        {
            trans.eulerAngles = new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, z);
        }

      
        public static void SetLocalEulerAngle_Z(this Transform trans, float z)
        {
            trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, z);
        }
        
        public static void SetLocalEulerAngle_Y(this Transform trans, float y)
        {
            trans.localEulerAngles = new Vector3(trans.localEulerAngles.x, y, trans.localEulerAngles.z);
        }

        public static void SetLocalEulerAngle_X(this Transform trans, float x)
        {
            trans.localEulerAngles = new Vector3(x, trans.localEulerAngles.y, trans.localEulerAngles.z);
        }

        public static void SetRotation_X(this Transform trans, float x)
        {
            trans.rotation = new Quaternion(x, trans.rotation.y, trans.rotation.z, trans.rotation.w);
        }

        public static void SetRotation_Y(this Transform trans, float y)
        {
            trans.rotation = new Quaternion(trans.rotation.x, y, trans.rotation.z, trans.rotation.w);
        }

        public static void SetRotation_Z(this Transform trans, float z)
        {
            trans.rotation = new Quaternion(trans.rotation.x, trans.rotation.y, z, trans.rotation.w);
        }

        public static void SetRotation_W(this Transform trans, float w)
        {
            trans.rotation = new Quaternion(trans.rotation.x, trans.rotation.y, trans.rotation.z, w);
        }

        public static void SetLocalRotation_W(this Transform trans, float w)
        {
            trans.localRotation = new Quaternion(trans.localRotation.x, trans.localRotation.y, trans.localRotation.z, w);
        }

        public static void SetLocalRotation_Z(this Transform trans, float z)
        {
            trans.localRotation = new Quaternion(trans.localRotation.x, trans.localRotation.y, z, trans.localRotation.w);
        }



        public static void SetLocalRotation_Y(this Transform trans, float y)
        {
            trans.localRotation = new Quaternion(trans.localRotation.x, y, trans.localRotation.z, trans.localRotation.w);
        }


        public static void SetLocalRotation_X(this Transform trans, float x)
        {
            trans.localRotation = new Quaternion(x, trans.localRotation.y, trans.localRotation.z, trans.localRotation.w);
        }
        public static void SetPos_X(this Transform trans, float x)
        {
            trans.position = new Vector3(x, trans.position.y, trans.position.z);
        }

        public static void SetPos_Y(this Transform trans, float y)
        {
            trans.position = new Vector3(trans.position.x, y, trans.position.z);
        }

        public static void SetPos_Z(this Transform trans, float z)
        {
            trans.position = new Vector3(trans.position.x, trans.position.y, z);
        }

        public static void SetLocalPos_Z(this Transform trans, float z)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, trans.localPosition.y, z);
        }

        public static void SetLocalPos_Y(this Transform trans, float y)
        {
            trans.localPosition = new Vector3(trans.localPosition.x, y, trans.localPosition.z);
        }

        public static void SetLocalPos_X(this Transform trans, float x)
        {
            trans.localPosition = new Vector3(x, trans.localPosition.y, trans.localPosition.z);
        }
    } 
}
