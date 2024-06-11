using System.Collections.Generic;
using UnityEngine;
using YFramework;

namespace YFramework
{
    /// <summary>
    /// ToTween管理器
    /// </summary>
    public class TotweenModule : SingletonMono<TotweenModule>
    {
        private static bool mIsInstantiate;
        /// <summary>
        /// ToTween数组
        /// </summary>
        private IList<IToTween> mProcessList = new List<IToTween>();
        public static void Init()
        {
            if (mIsInstantiate) return;
            mIsInstantiate = true;
            UnityEngine.Debug.Log("Launcher ToTween");
            GameObject yCenter = new GameObject("Totween");
            Instance =  yCenter.AddComponent<TotweenModule>();
            DontDestroyOnLoad(yCenter);
            yCenter.hideFlags = HideFlags.HideAndDontSave;
        }
        /// <summary>
        ///添加ToTween
        /// </summary>
        /// <param name="process"></param>
        public void AddToTween(IToTween process) {
            if (process == null) return;
            mProcessList.Add(process);
            process.Awake();
            process.Start();
        }
        /// <summary>
        /// 移除ToTween
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
