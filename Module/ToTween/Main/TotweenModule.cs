using System.Collections.Generic;
using UnityEngine;
using YFramework;

namespace YFramework
{
    /// <summary>
    /// ToTween������
    /// </summary>
    public class TotweenModule : SingletonMono<TotweenModule>
    {
        /// <summary>
        /// ToTween����
        /// </summary>
        private IList<IToTween> mProcessList = new List<IToTween>();
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Init()
        {
            UnityEngine.Debug.Log("Launcher DoTween");
            GameObject yCenter = new GameObject("Dotween");
            Instance =  yCenter.AddComponent<TotweenModule>();
            DontDestroyOnLoad(yCenter);
            yCenter.hideFlags = HideFlags.HideAndDontSave;
        }
        /// <summary>
        ///���ToTween
        /// </summary>
        /// <param name="process"></param>
        public void AddToTween(IToTween process) {
            if (process == null) return;
            mProcessList.Add(process);
            process.Awake();
            process.Start();
        }
        /// <summary>
        /// �Ƴ�ToTween
        /// </summary>
        /// <param name="process"></param>
        public void RemoveToTween(IToTween process)
        {
            if (process == null) return;
            if (mProcessList.Contains(process))
            {
                mProcessList.Remove(process);
            }
            process.OnDestory();
            process.Recycle();
        }
        private void Update()
        {
            for (int i = 0; i < mProcessList.Count; i++)
            {
                mProcessList[i].Update();
            }
        }
        private void OnDestroy()
        {
            for (int i = 0; i < mProcessList.Count; i++)
            {
                mProcessList[i].OnDestory();
                mProcessList[i].Recycle();
            }
            mProcessList.Clear();
        }
    } 
}
