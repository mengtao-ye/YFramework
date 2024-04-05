using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region LocalEuler
        public static IToTween ToLocalEuler(this Transform trans, Vector3 targetEuler, float time)
        {
            ToLocalEulerTween tween = ClassPool<ToLocalEulerTween>.Pop();
            tween.SetEuler(targetEuler, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToLocalEulerX(this Transform trans, float targetEuler, float time)
        {
            return ToLocalEuler(trans, new Vector3(targetEuler, trans.localEulerAngles.y, trans.localEulerAngles.z),time);
        }
        public static IToTween ToLocalEulerY(this Transform trans, float targetEuler, float time)
        {
            return ToLocalEuler(trans, new Vector3(trans.localEulerAngles.x, targetEuler, trans.localEulerAngles.z), time);
        }
        public static IToTween ToLocalEulerZ(this Transform trans, float targetEuler, float time)
        {
            return ToLocalEuler(trans, new Vector3(trans.localEulerAngles.x, trans.localEulerAngles.y, targetEuler), time);
        }
        #endregion
        #region IToTween
        public static IToTween ToLocalEuler(this ITransformToTween trans, Vector3 targetEuler, float time)
        {
            ToLocalEulerTween tween = ClassPool<ToLocalEulerTween>.Pop();
            tween.SetEuler(targetEuler, trans.transform, time);
            trans.Concat(tween);
            return tween;
        }
        public static IToTween ToLocalEulerX(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalEuler(trans, new Vector3(targetEuler, trans.transform.localEulerAngles.y, trans.transform.localEulerAngles.z), time);
        }
        public static IToTween ToLocalEulerY(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalEuler(trans, new Vector3(trans.transform.localEulerAngles.x, targetEuler, trans.transform.localEulerAngles.z), time);
        }
        public static IToTween ToLocalEulerZ(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalEuler(trans, new Vector3(trans.transform.localEulerAngles.x, trans.transform.localEulerAngles.y, targetEuler), time);
        } 
        #endregion
    } 
}
