using System;
using System.Collections;
using UnityEngine;

namespace YFramework
{
    public class PositionTween : BaseTween
    {
        private Vector3 mTargetPos;
        public PositionTween(float time, Transform trans, Vector3 targetPos) : base(time, trans)
        {
            mTargetPos = targetPos;
            IEnumeratorModule.StartCoroutine(IEDoMove(trans, targetPos, time, ()=> {  OnFinish(); }));
        }
        public override void Complete()
        {
            transform.position = mTargetPos;
        }

        private static IEnumerator IEDoMove(Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
            if (time <= 0) yield break;
            AnimationCurve curveX = AnimationCurve.EaseInOut(0, trans.position.x, time, targetPos.x);
            AnimationCurve curveY = AnimationCurve.EaseInOut(0, trans.position.y, time, targetPos.y);
            AnimationCurve curveZ = AnimationCurve.EaseInOut(0, trans.position.z, time, targetPos.z);
            float timer = Time.realtimeSinceStartup;
            float tempTime = Time.realtimeSinceStartup - timer;
            while (tempTime < time)
            {
                tempTime = Time.realtimeSinceStartup - timer;
                trans.position = new Vector3(curveX.Evaluate(tempTime), curveY.Evaluate(tempTime), curveZ.Evaluate(tempTime));
                yield return new WaitForSecondsRealtime(0.001f);
            }
            if (finish != null) finish();
        }
    }
}
