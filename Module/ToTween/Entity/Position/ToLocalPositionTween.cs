using UnityEngine;
using YFramework;

namespace YFramework
{
    /// <summary>
    /// LocalPosition 的Tween
    /// </summary>
    public class ToLocalPositionTween : BaseTransformToTween
    {
        private Vector3 mTargetPos;
        private Vector3 nNowPos;
        private float mTempValue;
        public void SetPosition(Vector3 targetPos, Transform transform, float time)
        {
            mTargetPos = targetPos;
            SetBaseToTween(time, transform);
        }
        public override void Awake()
        {
            base.Awake();
            nNowPos = transform.localPosition;
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
            transform.localPosition= Vector3.Lerp(nNowPos, mTargetPos, mTempValue/ mTime);
        }
        protected override void Finish()
        {
            base.Finish();
            transform.localPosition = mTargetPos;
        }
        public override void Recycle()
        {
            ClassPool<ToLocalPositionTween>.Push(this);
        }
    }
}
