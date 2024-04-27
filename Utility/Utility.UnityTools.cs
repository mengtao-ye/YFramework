using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// ���������߹���
        /// </summary>
        public class UnityTools
        {
            /// <summary>
            /// �����ն���
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