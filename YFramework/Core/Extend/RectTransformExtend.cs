using UnityEngine;

namespace YFramework
{
    public static class RectTransformExtend
    {
        public static void SetSizeDeltaX(this RectTransform rect,float x) {
            if (rect == null) return;
            rect.sizeDelta = new Vector2(x, rect.sizeDelta.y);
        }
        public static void SetSizeDeltaY(this RectTransform rect, float y)
        {
            if (rect == null) return;
            rect.sizeDelta = new Vector2(rect.sizeDelta.x,y);
        }
    }
}
