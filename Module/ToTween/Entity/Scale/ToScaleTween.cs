using UnityEngine;
using YFramework;

namespace YFramework
{
    public class ToScaleTween : BaseTransformToTween
    {
        private Vector3 mTargetScale;
        private Vector3 nNowScale;
        private float mTempValue;
        public void SetScale(Vector3 targetScale, Transform transform, float time)
        {
            mTargetScale = targetScale;
            SetBaseToTween(time, transform);
        }
        public override void Awake()
        {
            base.Awake();
            nNowScale = transform.localScale;
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
            transform.localScale = Vector3.Lerp(nNowScale, mTargetScale, mTempValue/ mTime);
        }
        protected override void Finish()
        {
            base.Finish();
            transform.localScale = mTargetScale;
        }
        public override void Recycle()
        {
            ClassPool<ToScaleTween>.Push(this);
        }
    }
}
