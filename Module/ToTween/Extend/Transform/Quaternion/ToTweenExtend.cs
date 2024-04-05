using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform

        public static IToTween ToQuaternion(this Transform trans, Quaternion targetEuler, float time)
        {
            ToQuaternionTween tween = ClassPool<ToQuaternionTween>.Pop();
            tween.SetEuler(targetEuler, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToQuaternionX(this Transform trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(targetEuler, trans.rotation.y, trans.rotation.z, trans.rotation.w), time);
        }
        public static IToTween ToQuaternionY(this Transform trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(trans.rotation.x, targetEuler, trans.rotation.z, trans.rotation.w), time);
        }
        public static IToTween ToQuaternionZ(this Transform trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(trans.rotation.x, trans.rotation.y, targetEuler, trans.rotation.w), time);
        }
        public static IToTween ToQuaternionW(this Transform trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(trans.rotation.x, trans.rotation.y, trans.rotation.y, targetEuler), time);
        }
        #endregion
        #region IToTween

        public static IToTween ToQuaternion(this ITransformToTween trans, Quaternion targetEuler, float time)
        {
            ToQuaternionTween tween = ClassPool<ToQuaternionTween>.Pop();
            tween.SetEuler(targetEuler, trans.transform, time);
            trans.Concat(tween);
            return tween;
        }
        public static IToTween ToQuaternionX(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(targetEuler, trans.transform.rotation.y, trans.transform.rotation.z, trans.transform.rotation.w), time);
        }
        public static IToTween ToQuaternionY(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(trans.transform.rotation.x, targetEuler, trans.transform.rotation.z, trans.transform.rotation.w), time);
        }
        public static IToTween ToQuaternionZ(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(trans.transform.rotation.x, trans.transform.rotation.y, targetEuler, trans.transform.rotation.w), time);
        }
        public static IToTween ToQuaternionW(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToQuaternion(trans, new Quaternion(trans.transform.rotation.x, trans.transform.rotation.y, trans.transform.rotation.y, targetEuler), time);
        }
        #endregion

    }
}
