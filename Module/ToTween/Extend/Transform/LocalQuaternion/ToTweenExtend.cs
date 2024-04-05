using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform
        public static IToTween ToLocalQuaternion(this Transform trans, Quaternion targetEuler, float time)
        {
            ToLocalQuaternionTween tween = ClassPool<ToLocalQuaternionTween>.Pop();
            tween.SetEuler(targetEuler, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToLocalQuaternionX(this Transform trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(targetEuler, trans.localRotation.y, trans.localRotation.z, trans.localRotation.w), time);
        }

        public static IToTween ToLocalQuaternionY(this Transform trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(trans.localRotation.x, targetEuler, trans.localRotation.z, trans.localRotation.w), time);
        }

        public static IToTween ToLocalQuaternionZ(this Transform trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(trans.localRotation.x, trans.localRotation.y, targetEuler, trans.localRotation.w), time);
        }
        public static IToTween ToLocalQuaternionW(this Transform trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(trans.localRotation.x, trans.localRotation.y, trans.localRotation.z, targetEuler), time);
        } 
        #endregion
        #region IToTween
        public static IToTween ToLocalQuaternion(this ITransformToTween trans, Quaternion targetEuler, float time)
        {
            ToLocalQuaternionTween tween = ClassPool<ToLocalQuaternionTween>.Pop();
            tween.SetEuler(targetEuler, trans.transform, time);
            trans.Concat(tween);
            return tween;
        }
        public static IToTween ToLocalQuaternionX(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(targetEuler, trans.transform.localRotation.y, trans.transform.localRotation.z, trans.transform.localRotation.w), time);
        }

        public static IToTween ToLocalQuaternionY(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(trans.transform.localRotation.x, targetEuler, trans.transform.localRotation.z, trans.transform.localRotation.w), time);
        }

        public static IToTween ToLocalQuaternionZ(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(trans.transform.localRotation.x, trans.transform.localRotation.y, targetEuler, trans.transform.localRotation.w), time);
        }
        public static IToTween ToLocalQuaternionW(this ITransformToTween trans, float targetEuler, float time)
        {
            return ToLocalQuaternion(trans, new Quaternion(trans.transform.localRotation.x, trans.transform.localRotation.y, trans.transform.localRotation.z, targetEuler), time);
        }
        #endregion
    }
}
