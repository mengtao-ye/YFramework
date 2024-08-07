﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static YFramework.Utility;

namespace YFramework
{
    public class TipsUIManager : BasePanel, ITipsUIManager
    {
        private Transform BG;//显示的背景
        protected Transform mTipsUIParent;//父对象
        protected List<ITipsUI> mAllTipsUI;//所有注册的TipsUI
        public int curShowCount { get { return mTipsUIStack.Count; } }//当前显示的UI的个数
        private Stack<ITipsUI> mTipsUIStack;//当前显示的TipsUI
        private ITipsUI mCurShowTipsUI = null;//当前显示的TipsUI的名字
        public ITipsUI curShowTipsUI { get { return mCurShowTipsUI; } }//当前显示的TipsUI的名字
        private Stack<ITipsUI> mTempTipsUI;//存放临时的TipsUI
        private Action mBGClickCallBack;//背景点击响应
        public TipsUIManager()
        {

        }

        public void SetBG(Button bgImg)
        {
            if (bgImg == null) return;
            bgImg.onClick.AddListener(BGClick);
        }

        private void BGClick()
        {
            mBGClickCallBack?.Invoke();
        }

        public override void Awake()
        {
            base.Awake();
            mTipsUIStack = new Stack<ITipsUI>();
            mAllTipsUI = new List<ITipsUI>();
            mTempTipsUI = new Stack<ITipsUI>();
            if (rectTransform.Find("BG") != null)
            {
                BG = rectTransform.Find("BG");
                BG.SetSiblingIndex(1);
                BG.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("ShowTipsUI下面没有BG面板！");
            }
            if (rectTransform.Find("TipsPanel") != null)
            {
                mTipsUIParent = rectTransform.Find("TipsPanel");
                mTipsUIParent.SetSiblingIndex(0);
            }
            else
            {
                Debug.LogError("ShowTipsUI下面没有TipsUIParent面板！");
            }

        }

        public override void Update()
        {
            for (int i = 0; i < mAllTipsUI.Count; i++)
            {
                if (mAllTipsUI[i].isShow)
                {
                    mAllTipsUI[i].Update();
                }
            }
        }

        public override void OnDestory()
        {
            for (int i = 0; i < mAllTipsUI.Count; i++)
            {
                mAllTipsUI[i].OnDestory();
            }
        }

        /// <summary>
        /// 显示提示UI
        /// </summary>
        /// <param name="name"></param>
        public void ShowTipsUI<T>(Action<T> action) where T : class, ITipsUI, new()
        {
            GetTipsUI<T>((baseTipPanel) =>
            {
                if (!isShow) Show();
                if (IsInShowStack<T>())//在显示栈里面
                {
                    //并且还是显示在最上层
                    if (mCurShowTipsUI != null && mCurShowTipsUI.GetType().Name != baseTipPanel.GetType().Name)
                    {
                        action?.Invoke(baseTipPanel);
                        return;
                    }
                }
                BG.gameObject.SetActiveExtend(true);
                if (!IsInShowStack<T>())
                {
                    mTipsUIStack.Push(baseTipPanel);
                }
                if (mCurShowTipsUI != null)
                {
                    mCurShowTipsUI.rectTransform.SetParent(mTipsUIParent, true);
                    mCurShowTipsUI.rectTransform.SetSiblingIndex(mTipsUIParent.childCount - 1);
                }
                baseTipPanel.rectTransform.SetParent(BG, true);
                baseTipPanel.Show();
                mCurShowTipsUI = baseTipPanel;
                action?.Invoke(mCurShowTipsUI as T);
            });

        }
        /// <summary>
        /// 当前TipsUI是否在显示栈里面
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private bool IsInShowStack<T>() where T : class, ITipsUI, new()
        {
            var temp = mTipsUIStack.GetEnumerator();
            while (temp.MoveNext())
            {
                if (temp.Current.GetType().Name == typeof(T).Name)
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// 将TipsUI显示到最上层
        /// </summary>
        public void PopTopTipsUI(ITipsUI tipsUI)
        {
            if (tipsUI == null) return;
            ITipsUI temp = null;
            bool isFind = false;
            mTipsUIStack.Clear();
            while (mTipsUIStack.Count != 0)
            {
                temp = mTipsUIStack.Pop();
                if (temp.GetType().Name == tipsUI.GetType().Name)
                {
                    isFind = true;
                    while (mTempTipsUI.Count != 0)//把前面拿出来的TipsUI全部放回去
                    {
                        mTipsUIStack.Push(mTempTipsUI.Pop());
                    }
                    mTipsUIStack.Push(temp);//把指定的TipsUI放在最上层
                    break;
                }
                else
                {
                    mTempTipsUI.Push(temp);
                }
            }
            if (isFind)
            {
                mCurShowTipsUI = temp;
            }
            else
            {
                mCurShowTipsUI = null;
            }

        }

        /// <summary>
        /// 显示提示UI
        /// </summary>
        /// <param name="name"></param>
        public void GetTipsUI<T>(Action<T> action) where T : class, ITipsUI, new()
        {
            T tempTipsPanel = null;
            tempTipsPanel = FindTipsPanel<T>();
            if (tempTipsPanel != null)
            {
                action?.Invoke(tempTipsPanel);
                return;
            }
            SpawnTipsPanel<T>((ui) =>
            {
                AddTipsUI(ui);
                action?.Invoke(ui);
            });

        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void HideTipsUI<T>() where T : class, ITipsUI, new()
        {
            if (IsShow<T>())
            {
                FindTipsPanel<T>().Hide();
            }
        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void HideTipsUI<T>(int type) where T : class, ITipsUI, new()
        {
            if (IsShow<T>())
            {
                T value = FindTipsPanel<T>();
                if (value.tipType == type) value.Hide();
            }
        }
        /// <summary>
        /// 关闭按钮按下时
        /// </summary>
        public override void Hide()
        {
            if (mTipsUIStack.Count == 0)
            {
                base.Hide();
                mCurShowTipsUI = null;
                return;
            }
            ITipsUI tempTipsUI = mTipsUIStack.Pop();
            tempTipsUI.Hide(() =>
            {
                tempTipsUI.rectTransform.SetParent(mTipsUIParent, true);
                tempTipsUI.rectTransform.SetSiblingIndex(0);
                if (mTipsUIStack.Count == 0)
                {
                    mCurShowTipsUI = null;
                    base.Hide();
                }
                else
                {
                    mTipsUIStack.Peek().rectTransform.SetParent(BG, true);
                    mCurShowTipsUI = mTipsUIStack.Peek();
                }
            });
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="tipsUI"></param>
        protected void AddTipsUI(ITipsUI tipsUI)
        {
            if (IsContainsInAll(tipsUI)) return;
            mAllTipsUI.Add(tipsUI);
        }
        public bool IsContainsInAll(ITipsUI tipsUI)
        {
            if (tipsUI == null) return false;
            for (int i = 0; i < mAllTipsUI.Count; i++)
            {
                if (mAllTipsUI[i].GetType().Name == tipsUI.GetType().Name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 生成TipsPanel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private void SpawnTipsPanel<T>(Action<T> action) where T : class, ITipsUI, new()
        {
            T panel = new T();
            panel.SetShowTipsPanel(this);
            ResourceHelper.AsyncLoadAsset<GameObject>(mUICanvas.UIMap.Get(typeof(T).Name).assetPath, (target) =>
            {
                target = GameObject.Instantiate(target, mTipsUIParent);
                panel.SetCanvas(mUICanvas);
                panel.SetTrans(target.transform);
                panel.rectTransform.Reset();
                panel.Awake();
                panel.Start();
                action?.Invoke(panel);
            });
        }
        /// <summary>
        /// 查找面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindTipsPanel<T>() where T : class, ITipsUI, new()
        {
            for (int i = 0; i < mAllTipsUI.Count; i++)
            {
                if (mAllTipsUI[i].GetType().Name == typeof(T).Name)
                {
                    return mAllTipsUI[i] as T;
                }
            }
            return null;
        }
        /// <summary>
        /// 判断面板是否显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsShow<T>() where T : class, ITipsUI, new()
        {
            T panel = FindTipsPanel<T>();
            if (panel != null && panel.isShow) return true;
            return false;
        }
        /// <summary>
        /// 设置背景状态
        /// </summary>
        /// <param name="active"></param>
        public void SetBGActive(bool active)
        {
            if (BG != null)
            {
                BG.gameObject.SetActiveExtend(active);
            }
        }

        public void SetBGClickCallBack(Action callBack)
        {
            mBGClickCallBack = callBack;
        }

        public void ClearBGClickCallBack()
        {
            mBGClickCallBack = null;
        }
    }
}
