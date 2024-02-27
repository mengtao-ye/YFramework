using System;
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
        /// <summary>
        /// 显示提示UI
        /// </summary>
        /// <param name="name"></param>
        public void ShowLogUI<T>(Action<T> action) where T : class, ILogUI, new()
        {
             GetLogUI<T>((target)=> 
             {
                 if (!isShow) Show();
                 target.Show();
                 action?.Invoke(target);
             });
        }
        /// <summary>
        /// 显示提示UI
        /// </summary>
        /// <param name="name"></param>
        public void GetLogUI<T>(Action<T> action) where T : class, ILogUI, new()
        {
            for (int i = 0; i < mAllLogUIList.Count; i++)
            {
                if (mAllLogUIList[i] is T)
                {
                    action?.Invoke( mAllLogUIList[i] as T);
                    return;
                }
            }
            //如果当前列表中没有的话就生成
            SpawnLogPanel<T>((target)=> {
                AddLogUI(target);
                action?.Invoke(target);
            });
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
        private void SpawnLogPanel<T>(Action<T> action) where T : ILogUI, new()
        {
            T panel = new T();
            panel.SetLogUIManager(this);
            ResourceHelper.AsyncLoadAsset<GameObject>(mUICanvas.UIMap.Get(typeof(T).Name).assetPath,(target)=> {
                target = GameObject.Instantiate(target, transform);
                panel.SetCanvas(mUICanvas);
                panel.SetTrans(target.transform);
                panel.Awake();
                panel.Start();
                action?.Invoke( panel);
            });
        }
    }
}
