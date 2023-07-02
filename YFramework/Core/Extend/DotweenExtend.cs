using System;
using System.Collections;
using UnityEngine;

namespace YFramework
{
    public static class DotweenExtend
    {
        public static void DoRotation(this Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
           IEnumeratorModule .StartCoroutine(IEDoRotation(trans, targetPos, time, finish));
        }

        public static void DoScaleZ(this Transform trans, float sacleZ, float time, Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEDoScale(trans, new Vector3(trans.localScale.x, trans.localScale.y, sacleZ), time, finish));
        }

        public static void DoScaleY(this Transform trans, float y, float time, Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEDoScale(trans, new Vector3(trans.localScale.x, y, trans.localScale.z), time, finish));
        }

        public static void DoScaleX(this Transform trans, float x, float time, Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEDoScale(trans, new Vector3(x, trans.localScale.y, trans.localScale.z), time, finish));
        }

        public static void DoScale(this Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEDoScale(trans, targetPos, time, finish));
        }

        public static void DoMoveXY(this Transform trans,Vector2 targetPos,float time,Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEDoMove(trans, new Vector3(targetPos.x, targetPos.y, trans.position.z), time,finish));
        }

        public static ITween DoMoveZ(this Transform trans, float z,float time,Action finish = null)
        {
            return new PositionTween(time, trans, new Vector3(trans.position.x, trans.position.y, z));
        }

        public static ITween DoMoveY(this Transform trans, float y,float time,Action finish = null)
        {
            return new PositionTween(time, trans, new Vector3(trans.position.x, y, trans.position.z));
        }
        public static ITween DoMoveX(this Transform trans,float x,float time,Action finish = null) {
            return new PositionTween(time, trans, new Vector3(x, trans.position.y, trans.position.z));
        }

        public static ITween DoMove(this Transform trans, Vector3 targetPos,float time,Action finish = null) {
            return new PositionTween(time,trans,targetPos);
        }
        public static void DoLocalMove(this Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEDLocaloMove(trans, targetPos, time, finish));
        }
        public static void DoAnchoredMove(this RectTransform trans, Vector3 targetPos,float time,Action finish = null)
        {
            IEnumeratorModule.StartCoroutine(IEAnchoredDoMove(trans, targetPos,time,finish));
        }

        public static IEnumerator IEDoRotation(Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
            if (time <=0) yield break;
            AnimationCurve curveX = AnimationCurve.EaseInOut(0, trans.eulerAngles.x, time, targetPos.x);
            AnimationCurve curveY = AnimationCurve.EaseInOut(0, trans.eulerAngles.y, time, targetPos.y);
            AnimationCurve curveZ = AnimationCurve.EaseInOut(0, trans.eulerAngles.z, time, targetPos.z);
            float timer = Time.realtimeSinceStartup;
            float tempTime = Time.realtimeSinceStartup - timer;
            while (tempTime < time)
            {
                tempTime = Time.realtimeSinceStartup - timer;
                trans.rotation = Quaternion.Euler( new Vector3(curveX.Evaluate(tempTime), curveY.Evaluate(tempTime), curveZ.Evaluate(tempTime)));
                yield return new WaitForSecondsRealtime(0.001f);
            }
            if (finish != null) finish();
        }

        public static IEnumerator IEDoScale(Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
            if (time <= 0) yield break;
            AnimationCurve curveX = AnimationCurve.EaseInOut(0, trans.localScale.x, time, targetPos.x);
            AnimationCurve curveY = AnimationCurve.EaseInOut(0, trans.localScale.y, time, targetPos.y);
            AnimationCurve curveZ = AnimationCurve.EaseInOut(0, trans.localScale.z, time, targetPos.z);
            float timer = Time.realtimeSinceStartup;
            float tempTime = Time.realtimeSinceStartup - timer;
            while (tempTime < time)
            {
                tempTime = Time.realtimeSinceStartup - timer;
                trans.localScale = new Vector3(curveX.Evaluate(tempTime), curveY.Evaluate(tempTime), curveZ.Evaluate(tempTime));
                yield return new WaitForSecondsRealtime(0.001f);
            }
            if (finish != null) finish();
        }

        public static IEnumerator IEDLocaloMove(Transform trans, Vector3 targetPos, float time, Action finish = null)
        {
            if (time <= 0) yield break;
            AnimationCurve curveX = AnimationCurve.EaseInOut(0, trans.localPosition.x, time, targetPos.x);
            AnimationCurve curveY = AnimationCurve.EaseInOut(0, trans.localPosition.y, time, targetPos.y);
            AnimationCurve curveZ = AnimationCurve.EaseInOut(0, trans.localPosition.z, time, targetPos.z);
            float timer = Time.realtimeSinceStartup;
            float tempTime = Time.realtimeSinceStartup - timer;
            while (tempTime < time)
            {
                tempTime = Time.realtimeSinceStartup - timer;
                trans.localPosition = new Vector3(curveX.Evaluate(tempTime), curveY.Evaluate(tempTime), curveZ.Evaluate(tempTime));
                yield return new WaitForSecondsRealtime(0.001f);
            }
            if (finish != null) finish();
        }

        private static IEnumerator IEDoMove(Transform trans, Vector3 targetPos,float time,Action finish = null) {
            if (time <= 0) yield break;
            AnimationCurve curveX = AnimationCurve.EaseInOut(0, trans.position.x, time, targetPos.x);
            AnimationCurve curveY= AnimationCurve.EaseInOut(0, trans.position.y, time, targetPos.y);
            AnimationCurve curveZ = AnimationCurve.EaseInOut(0, trans.position.z, time, targetPos.z);
            float timer = Time.realtimeSinceStartup;
            float tempTime = Time.realtimeSinceStartup - timer;
            while (tempTime < time) {
                tempTime = Time.realtimeSinceStartup - timer;
                trans.position = new Vector3(curveX.Evaluate(tempTime), curveY.Evaluate(tempTime), curveZ.Evaluate(tempTime));
                yield return new WaitForSecondsRealtime (0.001f);
            }
            if (finish != null) finish();
        }

        public static IEnumerator IEAnchoredDoMove(RectTransform trans, Vector3 targetPos,float time,Action finish)
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
                trans.anchoredPosition = new Vector3(curveX.Evaluate(tempTime), curveY.Evaluate(tempTime), curveZ.Evaluate(tempTime));
                yield return new WaitForSecondsRealtime(0.001f);
            }
            if (finish != null) finish(); 
        }
    }
}