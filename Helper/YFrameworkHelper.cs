using UnityEngine;

namespace YFramework
{
    public abstract class YFrameworkHelper
    {
        /// <summary>
        /// 是否初始化完成该助手
        /// </summary>
        public static bool IsInitHelper
        {
            get {
                return Instance != null && Instance.IsInit;
            }
        }
        public static YFrameworkHelper Instance;
        public abstract bool IsInit { get; set; }
        public abstract Vector2 ScreenSize { get; set; }
    }
}
