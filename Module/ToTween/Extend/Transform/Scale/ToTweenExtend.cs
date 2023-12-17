using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform
        public static IToTween ToScale(this Transform trans, Vector3 targetScale, float time)
        {
            ToScaleTween tween = ClassPool<ToScaleTween>.Pop();
            tween.SetScale(targetScale, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToScaleX(this Transform trans, float targetScale, float time)
        {
            return ToScale(trans, new Vector3(targetScale, trans.localScale.y, trans.localScale.z), time);
        }
        public static IToTween ToScaleY(this Transform trans, float targetScale, float time)
        {
            return ToScale(trans, new Vector3(trans.localScale.x, targetScale, trans.localScale.z), time);
        }
        public static IToTween ToScaleZ(this Transform trans, float targetScale, float time)
        {
            return ToScale(trans, new Vector3(trans.localScale.x, trans.localScale.y, targetScale), time);
        }
        #endregion
        #region IToTween
        public static IToTween ToScale(this IToTween trans, Vector3 targetScale, float time)
        {
            ToScaleTween tween = ClassPool<ToScaleTween>.Pop();
            tween.SetScale(targetScale, trans.transform, time);
            trans.Concat(tween);
            return tween;
        }
        public static IToTween ToScaleX(this IToTween trans, float targetScale, float time)
        {
            return ToScale(trans, new Vector3(targetScale, trans.transform.localScale.y, trans.transform.localScale.z), time);
        }
        public static IToTween ToScaleY(this IToTween trans, float targetScale, float time)
        {
            return ToScale(trans, new Vector3(trans.transform.localScale.x, targetScale, trans.transform.localScale.z), time);
        }
        public static IToTween ToScaleZ(this IToTween trans, float targetScale, float time)
        {
            return ToScale(trans, new Vector3(trans.transform.localScale.x, trans.transform.localScale.y, targetScale), time);
        }
        #endregion
    }
}
