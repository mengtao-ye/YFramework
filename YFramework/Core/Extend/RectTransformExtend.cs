using UnityEngine;

namespace YFramework
{

    public static class RectTransformExtend
    {
        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="x"></param>
        public static void SetSizeDeltaX(this RectTransform rect,float x)
        {
            if (rect == null) return;
            rect.sizeDelta = new Vector2(x, rect.sizeDelta.y);
        }
        /// <summary>
        /// 设置大小
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="y"></param>
        public static void SetSizeDeltaY(this RectTransform rect, float y)
        {
            if (rect == null) return;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x,y);
        }
    }
}
