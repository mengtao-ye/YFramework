using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public class LogUIManager : BasePanel, ILogUIManager
    {
        private List<ILogUI> mAllLogUIList;
        public LogUIManager()
        {
            mAllLogUIList = new List<ILogUI>();
        }
        public override void Update()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                mAllLogUIList[i].Update();
            }
        }
        public override void FixedUpdate()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                mAllLogUIList[i].FixedUpdate();
            }
        }

        public override void LaterUpdate()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                mAllLogUIList[i].LaterUpdate();
            }
        }

        public override void OnDestory()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                mAllLogUIList[i].OnDestory();
            }
        }
        public override void Clear()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                mAllLogUIList[i].Clear();
            }
        }

        /// <summary>
        /// 显示提示UI
        /// </summary>
        /// <param name="name"></param>
        public T ShowLogUI<T>() where T : class, ILogUI, new()
        {
            T baseLogPanel = GetLogUI<T>();
            if(!isShow) Show();
            baseLogPanel.Show();
            return baseLogPanel;
        }
        /// <summary>
        /// 显示提示UI
        /// </summary>
        /// <param name="name"></param>
        public T GetLogUI<T>() where T : class, ILogUI, new()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                if (mAllLogUIList[i] is T)
                {
                    return mAllLogUIList[i] as T;
                }
            }
            //如果当前列表中没有的话就生成
            T tempTipsPanel = SpawnLogPanel<T>();
            AddLogUI(tempTipsPanel);
            return tempTipsPanel;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tipsUI"></param>
        protected void AddLogUI(ILogUI tipsUI)
        {
            mAllLogUIList.Add(tipsUI);
        }

        /// <summary>
        /// 生成TipsPanel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private T SpawnLogPanel<T>() where T : ILogUI, new()
        {
            T panel = new T();
            panel.SetLogUIManager(this);
            GameObject target = Resource.LoadAsset<GameObject>(mUICanvas.UIMap.Get(typeof(T).Name).assetPath);
            target = GameObject.Instantiate(target, transform);
            panel.SetTrans(target.transform);
            panel.Awake();
            panel.Start();
            return panel;
        }
    }
}
