using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform
        public static IToTween ToMove(this Transform trans, Vector3 targetPos, float time)
        {
            ToPositionTween tween = ClassPool<ToPositionTween>.Pop();
            tween.SetPosition(targetPos, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToMoveXY(this Transform trans, float targetX, float targetY, float time)
        {
            return ToMove(trans, new Vector3(targetX, targetY, trans.position.z), time);
        }
        public static IToTween ToMoveXY(this Transform trans, Vector2 targetPos, float time)
        {
            return ToMove(trans, new Vector3(targetPos.x, targetPos.y, trans.position.z), time);
        }
        public static IToTween ToMoveXZ(this Transform trans, float targetX, float targetZ, float time)
        {
            return ToMove(trans, new Vector3(targetX, trans.position.y, targetZ), time);
        }
        public static IToTween ToMoveXZ(this Transform trans, Vector2 targetPos, float time)
        {
            return ToMove(trans, new Vector3(targetPos.x, trans.position.y, targetPos.y), time);
        }

        public static IToTween ToMoveYZ(this Transform trans, float targetY, float targetZ, float time)
        {
            return ToMove(trans, new Vector3(trans.position.x, targetY, targetZ), time);
        }
        public static IToTween ToMoveYZ(this Transform trans, Vector2 targetPos, float time)
        {
            return ToMove(trans, new Vector3(trans.position.x, targetPos.x, targetPos.y), time);
        }

        public static IToTween ToMoveX(this Transform trans, float targetX, float time)
        {
            return ToMove(trans, new Vector3(targetX, trans.position.y, trans.position.z), time);
        }

        public static IToTween ToMoveY(this Transform trans, float targetY, float time)
        {
            return ToMove(trans, new Vector3(trans.position.x, targetY, trans.position.z), time);
        }
        public static IToTween ToMoveZ(this Transform trans, float targetZ, float time)
        {
            return ToMove(trans, new Vector3(trans.position.x, trans.position.y, targetZ), time);
        }
        #endregion
        #region IToTween
        public static IToTween ToMove(this ITransformToTween trans, Vector3 targetPos, float time)
        {
            ToPositionTween tween = ClassPool<ToPositionTween>.Pop();
            tween.SetPosition(targetPos, trans.transform, time);
            trans.Concat(tween);
            return tween;
        }
        public static IToTween ToMoveXY(this ITransformToTween trans, float targetX, float targetY, float time)
        {
            return ToMove(trans, new Vector3(targetX, targetY, trans.transform.position.z), time);
        }
        public static IToTween ToMoveXY(this ITransformToTween trans, Vector2 targetPos, float time)
        {
            return ToMove(trans, new Vector3(targetPos.x, targetPos.y, trans.transform.position.z), time);
        }
        public static IToTween ToMoveXZ(this ITransformToTween trans, float targetX, float targetZ, float time)
        {
            return ToMove(trans, new Vector3(targetX, trans.transform.position.y, targetZ), time);
        }
        public static IToTween ToMoveXZ(this ITransformToTween trans, Vector2 targetPos, float time)
        {
            return ToMove(trans, new Vector3(targetPos.x, trans.transform.position.y, targetPos.y), time);
        }

        public static IToTween ToMoveYZ(this ITransformToTween trans, float targetY, float targetZ, float time)
        {
            return ToMove(trans, new Vector3(trans.transform.position.x, targetY, targetZ), time);
        }
        public static IToTween ToMoveYZ(this ITransformToTween trans, Vector2 targetPos, float time)
        {
            return ToMove(trans, new Vector3(trans.transform.position.x, targetPos.x, targetPos.y), time);
        }

        public static IToTween ToMoveX(this ITransformToTween trans, float targetX, float time)
        {
            return ToMove(trans, new Vector3(targetX, trans.transform.position.y, trans.transform.position.z), time);
        }

        public static IToTween ToMoveY(this ITransformToTween trans, float targetY, float time)
        {
            return ToMove(trans, new Vector3(trans.transform.position.x, targetY, trans.transform.position.z), time);
        }
        public static IToTween ToMoveZ(this ITransformToTween trans, float targetZ, float time)
        {
            return ToMove(trans, new Vector3(trans.transform.position.x, trans.transform.position.y, targetZ), time);
        }
        #endregion
    }
}
