using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// 贝塞尔曲线工具
        /// </summary>
        public class UnityTools
        {
            /// <summary>
            /// 创建空对象
            /// </summary>
            /// <param name="name"></param>
            /// <param name="parent"></param>
            /// <returns></returns>
            public static GameObject CreateGameObject(string name,Transform parent)
            {
                GameObject obj = new GameObject(name);
                obj.transform.parent = parent;
                return obj;
            }
        }
    }

}