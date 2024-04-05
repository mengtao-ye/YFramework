using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform
        public static IToTween ToLocalMove(this Transform trans, Vector3 targetPos, float time)
        {
            ToLocalPositionTween tween = ClassPool<ToLocalPositionTween>.Pop();
            tween.SetPosition(targetPos, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToLocalMoveXY(this Transform trans, float targetX, float targetY, float time)
        {
            return ToLocalMove(trans, new Vector3(targetX, targetY, trans.localPosition.z), time);
        }
        public static IToTween ToLocalMoveXY(this Transform trans, Vector2 targetPos, float time)
        {
            return ToLocalMove(trans, new Vector3(targetPos.x, targetPos.y, trans.localPosition.z), time);
        }

        public static IToTween ToLocalMoveXZ(this Transform trans, float targetX, float targetZ, float time)
        {
            return ToLocalMove(trans, new Vector3(targetX, trans.localPosition.y, targetZ), time);
        }
        public static IToTween ToLocalMoveXZ(this Transform trans, Vector2 targetPos, float time)
        {
            return ToLocalMove(trans, new Vector3(targetPos.x, trans.localPosition.y, targetPos.y), time);
        }

        public static IToTween ToLocalMoveYZ(this Transform trans, float targetY, float targetZ, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.localPosition.x, targetY, targetZ), time);
        }
        public static IToTween ToLocalMoveYZ(this Transform trans, Vector2 targetPos, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.localPosition.x, targetPos.x, targetPos.y), time);
        }
        public static IToTween ToLocalMoveX(this Transform trans, float targetX, float time)
        {
            return ToLocalMove(trans, new Vector3(targetX, trans.localPosition.y, trans.localPosition.z), time);
        }
        public static IToTween ToLocalMoveY(this Transform trans, float targetY, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.localPosition.x, targetY, trans.localPosition.z), time);
        }
        public static IToTween ToLocalMoveZ(this Transform trans, float targetZ, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.localPosition.x, trans.localPosition.y, targetZ), time);
        }
        #endregion
        #region IToTween
        public static IToTween ToLocalMove(this ITransformToTween trans, Vector3 targetPos, float time)
        {
            ToLocalPositionTween tween = ClassPool<ToLocalPositionTween>.Pop();
            tween.SetPosition(targetPos, trans.transform, time);
            tween.Concat(tween);
            return tween;
        }
        public static IToTween ToLocalMoveXY(this ITransformToTween trans, float targetX, float targetY, float time)
        {
            return ToLocalMove(trans, new Vector3(targetX, targetY, trans.transform.localPosition.z), time);
        }
        public static IToTween ToLocalMoveXY(this ITransformToTween trans, Vector2 targetPos, float time)
        {
            return ToLocalMove(trans, new Vector3(targetPos.x, targetPos.y, trans.transform.localPosition.z), time);
        }

        public static IToTween ToLocalMoveXZ(this ITransformToTween trans, float targetX, float targetZ, float time)
        {
            return ToLocalMove(trans, new Vector3(targetX, trans.transform.localPosition.y, targetZ), time);
        }
        public static IToTween ToLocalMoveXZ(this ITransformToTween trans, Vector2 targetPos, float time)
        {
            return ToLocalMove(trans, new Vector3(targetPos.x, trans.transform.localPosition.y, targetPos.y), time);
        }

        public static IToTween ToLocalMoveYZ(this ITransformToTween trans, float targetY, float targetZ, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.transform.localPosition.x, targetY, targetZ), time);
        }
        public static IToTween ToLocalMoveYZ(this ITransformToTween trans, Vector2 targetPos, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.transform.localPosition.x, targetPos.x, targetPos.y), time);
        }
        public static IToTween ToLocalMoveX(this ITransformToTween trans, float targetX, float time)
        {
            return ToLocalMove(trans, new Vector3(targetX, trans.transform.localPosition.y, trans.transform.localPosition.z), time);
        }
        public static IToTween ToLocalMoveY(this ITransformToTween trans, float targetY, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.transform.localPosition.x, targetY, trans.transform.localPosition.z), time);
        }
        public static IToTween ToLocalMoveZ(this ITransformToTween trans, float targetZ, float time)
        {
            return ToLocalMove(trans, new Vector3(trans.transform.localPosition.x, trans.transform.localPosition.y, targetZ), time);
        }
        #endregion
    }
}
