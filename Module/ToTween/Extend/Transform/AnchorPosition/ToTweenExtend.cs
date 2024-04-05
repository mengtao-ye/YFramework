using UnityEngine;
using YFramework;

namespace YFramework
{
    public static partial class ToTweenExtend
    {
        #region Transform
        public static IToTween ToAnchorPositionMove(this RectTransform trans, Vector2 targetPos, float time)
        {
            ToAnchorPositionTween tween = ClassPool<ToAnchorPositionTween>.Pop();
            tween.SetPosition(targetPos, trans, time);
            TotweenModule.Instance.AddToTween(tween);
            return tween;
        }
        public static IToTween ToAnchorPositionMoveX(this RectTransform trans, float targetX, float time)
        {
            return ToAnchorPositionMove(trans,new Vector2(targetX,trans.anchoredPosition.y),time);
        }
        public static IToTween ToAnchorPositionMoveY(this RectTransform trans, float targetY, float time)
        {
            return ToAnchorPositionMove(trans, new Vector2(trans.anchoredPosition.x, targetY), time);
        }
        #endregion
        #region IToTween
        public static IToTween ToAnchorPositionMove(this IRectTransformToTween trans, Vector2 targetPos, float time)
        {
            ToAnchorPositionTween tween = ClassPool<ToAnchorPositionTween>.Pop();
            tween.SetPosition(targetPos, trans.rectTransform, time);
            tween.Concat(tween);
            return tween;
        }
        public static IToTween ToLocalMoveX(this IRectTransformToTween trans, float targetX, float time)
        {
            return ToAnchorPositionMove(trans, new Vector2(targetX, trans.rectTransform.anchoredPosition.y), time);
        }
        public static IToTween ToLocalMoveY(this IRectTransformToTween trans, float targetY, float time)
        {
            return ToAnchorPositionMove(trans, new Vector2(trans.rectTransform.anchoredPosition.x, targetY), time);
        }

        #endregion
    }
}
