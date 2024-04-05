using UnityEngine;
using YFramework;

namespace YFramework
{
    public class ToLocalQuaternionTween : BaseTransformToTween
    {
        private Quaternion mTargetEuler;
        private Quaternion nNowEuler;
        private float mTempValue;
        public void SetEuler(Quaternion targetEuler, Transform transform, float time)
        {
            mTargetEuler = targetEuler;
            SetBaseToTween(time, transform);
        }
        public override void Awake()
        {
            base.Awake();
            nNowEuler = transform.localRotation;
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
            transform.localRotation = Quaternion.Lerp(nNowEuler, mTargetEuler, mTempValue/ mTime);
        }
        protected override void Finish()
        {
            base.Finish();
            transform.localRotation = mTargetEuler;
        }
        public override void Recycle()
        {
            ClassPool<ToLocalQuaternionTween>.Push(this);
        }
    }
}
