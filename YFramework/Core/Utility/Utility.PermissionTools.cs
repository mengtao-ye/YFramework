using System.Collections.Generic;
using UnityEngine.Android;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// Unity Android权限工具
        /// </summary>
        public static class PermissionTools
        {
            /// <summary>
            /// 申请当前所有权限
            /// </summary>
            /// <param name="permission"></param>
            public static void ApplyForAllPermission(List<string> permission)
            {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (permission == null || permission.Count == 0) return;
            bool needApplyPermission = false;
            for (int i = 0; i < permission.Count; i++)
            {
                if (!Permission.HasUserAuthorizedPermission(permission[i])) {
                    needApplyPermission = true;
                    break;
                }
            }
            if (needApplyPermission) 
            {
                Permission.RequestUserPermissions(permission.ToArray());
            }
#endif
            }

            /// <summary>
            /// 检查权限，如果有的话就不做其他操作，没有的话就申请
            /// </summary>
            /// <param name="permissionName"></param>
            public static void CheckForPermission(string permissionName)
            {
                if (string.IsNullOrEmpty(permissionName)) return;
                bool hasPermission = Permission.HasUserAuthorizedPermission(permissionName);
                if (hasPermission) return;
                Permission.RequestUserPermission(permissionName);
            }
        }
    }
}
