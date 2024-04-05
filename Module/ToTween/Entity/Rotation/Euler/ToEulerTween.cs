using UnityEngine;
using YFramework;

namespace YFramework
{
    public class ToEulerTween : BaseTransformToTween
    {
        private Vector3 mTargetEuler;
        private Vector3 nNowEuler;
        private float mTempValue;
        public void SetEuler(Vector3 targetEuler, Transform transform, float time)
        {
            mTargetEuler = targetEuler;
            SetBaseToTween( time, transform);
        }
        public override void Awake()
        {
            base.Awake();
            nNowEuler = transform.eulerAngles;
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
            transform.eulerAngles = Vector3.Lerp(nNowEuler, mTargetEuler, mTempValue/ mTime);
        }
        protected override void Finish()
        {
            base.Finish();
            transform.eulerAngles = mTargetEuler;
        }
        public override void Recycle()
        {
            ClassPool<ToEulerTween>.Push(this);
        }
    }
}
