using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform
        public static IToTween ToEuler(this Transform trans, Vector3 targetEuler, float time)
        {
            ToEulerTween tween = ClassPool<ToEulerTween>.Pop();
            tween.SetEuler(targetEuler, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToEulerX(this Transform trans, float targetEuler, float time)
        {
            return ToEuler(trans, new Vector3(targetEuler, trans.eulerAngles.y, trans.eulerAngles.z),  time);
        }
        public static IToTween ToEulerY(this Transform trans, float targetEuler, float time)
        {
            return ToEuler(trans, new Vector3(trans.eulerAngles.x, targetEuler, trans.eulerAngles.z), time);
        }
        public static IToTween ToEulerZ(this Transform trans, float targetEuler, float time)
        {
            return ToEuler(trans, new Vector3(trans.eulerAngles.x, trans.eulerAngles.y, targetEuler), time);
        }
        #endregion
        #region ITween
        public static IToTween ToEuler(this ITransformToTween @this, Vector3 targetEuler, float time)
        {
            ToEulerTween tween = ClassPool<ToEulerTween>.Pop();
            tween.SetEuler(targetEuler, @this.transform, time);
            @this.Concat(tween);
            return tween;
        }
        public static IToTween ToEulerX(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToEuler(trans, new Vector3(targetEuler, trans.transform.eulerAngles.y, trans.transform.eulerAngles.z),time);
        }
        public static IToTween ToEulerY(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToEuler(trans, new Vector3(trans.transform.eulerAngles.x, targetEuler, trans.transform.eulerAngles.z), time);
        }
        public static IToTween ToEulerZ(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToEuler(trans, new Vector3(trans.transform.eulerAngles.x, trans.transform.eulerAngles.y, targetEuler), time);
        } 
        #endregion
    } 
}
