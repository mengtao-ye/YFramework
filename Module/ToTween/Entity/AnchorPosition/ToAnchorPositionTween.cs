using UnityEngine;
using YFramework;

namespace YFramework
{
    public class ToAnchorPositionTween : BaseRectTransformToTween
    {
        private Vector2 mTargetPos;
        private Vector2 nNowPos;
        private float mTempValue;
        public void SetPosition(Vector2 targetPos, RectTransform transform, float time)
        {
            mTargetPos = targetPos;
            SetBaseToTween(time,transform);
        }
        public override void Awake()
        {
            base.Awake();
            nNowPos = rectTransform.anchoredPosition;
        }
        public override void Update()
        {
            mTimer += Time.deltaTime;
            if (mTimer > mTime)
            {
                Finish();
                return;
            }
            switch (curveType)
            {
                case CurveType.Linear:
                    mTempValue = mTimer / mTime;
                    break;
                case CurveType.FastToLow:
                    mTempValue = Mathf.Lerp(mTimer, mTime, mTimer / mTime);
                    break;
            }
            rectTransform.anchoredPosition = Vector2.Lerp(nNowPos, mTargetPos, mTempValue / mTime);
        }
        protected override void Finish()
        {
            base.Finish();
            rectTransform.anchoredPosition = mTargetPos;
        }
        public override void Recycle()
        {
            ClassPool<ToAnchorPositionTween>.Push(this);
        }
    }
}
