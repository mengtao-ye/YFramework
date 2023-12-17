using System;
using System.Collections;
using UnityEngine;

namespace YFramework
{
    public static class IEnumeratorModule
    {
        private static IEnumeratorMono mTarget;
        /// <summary>
        /// 初始化
        /// </summary>
        private static void Init() 
        {
            GameObject temp = new GameObject("IEnumeratorModule");
            temp.hideFlags = HideFlags.HideAndDontSave;
            GameObject.DontDestroyOnLoad(temp);
            mTarget = temp.AddComponent<IEnumeratorMono>();
        }
        /// <summary>
        /// 延迟执行的方法
        /// </summary>
        /// <param name="action"></param>
        public static void DelayExe(Action action,float time)
        {
            if (time < 0) return;
            if (time == 0) action?.Invoke();
            StartCoroutine(IEDelayExe(action,time));
        }

        private static IEnumerator IEDelayExe(Action action,float time)
        {
            yield return new WaitForSeconds(time);
            action?.Invoke();
        }
        public static Coroutine StartCoroutine(IEnumerator ie)
        {
            if (mTarget == null)
            {
                Init();
            }
            if (ie == null) return null;
            return mTarget.StartCoroutine(ie);
        }
        public static void StopCoroutine(IEnumerator ie)
        {
            if (ie == null) return;
            if (mTarget == null)
            {
                Init();
            }
            mTarget.StopCoroutine(ie);
        }
        public static void StopCoroutine(Coroutine cor)
        {
            if (mTarget == null)
            {
                Init();
            }
            mTarget.StopCoroutine(cor);
        }
        public static void StopAllCoroutine()
        {
            if (mTarget == null)
            {
                Init();
            }
            mTarget.StopAllCoroutines();
        }
    }
}
