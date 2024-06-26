﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static YFramework.Utility;

namespace YFramework
{
    public abstract class BaseCanvas : BaseUI, ICanvas
    {
        public EventSystem CurEventSystem = null; //当前的EventSystem
        private ITipsUIManager mShowTipsPanel;
        public ITipsUIManager showTipsPanel
        {
            get
            {
                if (mShowTipsPanel == null)
                {
                    mShowTipsPanel = InitShowPanel();
                }
                return mShowTipsPanel;
            }
            private set { }
        }//提示信息面板
        private ILogUIManager mLogManager;
        public ILogUIManager logUIManager
        {
            get
            {
                if (mLogManager == null)
                {
                    mLogManager = InitLogUIManager();
                }
                return mLogManager;
            }
        }
        protected IScene mBaseScene;//对应的场景
        public IScene scene { get { return mBaseScene; } private set { } }
        private Stack<IPanel> mPopStack;//当前弹出的面板
        protected virtual byte mLayerCount { get { return (byte)CanvasLayerData.LAYER_COUNT; } }
        private RectTransform[] mLayerTrans;
        public IMap<string, UIMapperData> UIMap { get; private set; }
        public IPanel curPanel { get; private set; }//当前显示的面板
        private Stack<IPanel> mTempStackPanel;//临时面板
        private List<IPanel> mClosePanel;//关闭的面板
        public BaseCanvas(IScene scene, IMap<string, UIMapperData> map) : base()
        {
            UIMap = map;
            GameObject canvas = SpawnCanvas();
            SetTrans(canvas.transform);
            Init(scene);
            SetCanvas(this);//设置面板为自己
        }

        /// <summary>
        /// 初始化提示信息面板
        /// </summary>
        /// <returns></returns>
        private ITipsUIManager InitShowPanel()
        {
            ITipsUIManager mShowTipsPanel = new TipsUIManager();
            mShowTipsPanel.SetCanvas(this);
            Button bgImg = SpawnShowTipsPanel();
            mShowTipsPanel.SetBG(bgImg);
            mShowTipsPanel.SetTrans(GetLayer(CanvasLayerData.TIPS_LAYER));
            mShowTipsPanel.Awake();
            mShowTipsPanel.Start();
            return mShowTipsPanel;
        }
        /// <summary>
        /// 初始化提示面板
        /// </summary>
        /// <returns></returns>
        private ILogUIManager InitLogUIManager()
        {
            ILogUIManager logUIManager = new LogUIManager();
            logUIManager.SetCanvas(this);
            logUIManager.SetTrans(GetLayer(CanvasLayerData.LOG_LAYER));
            logUIManager.Awake();
            logUIManager.Start();
            return logUIManager;
        }
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="trans"></param>
        private void Init(IScene scene)
        {
            mPopStack = new Stack<IPanel>();
            mTempStackPanel = new Stack<IPanel>();
            mClosePanel = new List<IPanel>();
            mBaseScene = scene;
            SetEventSysetm();
        }
        /// <summary>
        /// 关于EventSystem的设置
        /// </summary>
        private void SetEventSysetm()
        {
            EventSystem[] eventSystems = GameObject.FindObjectsOfType<EventSystem>();
            if (eventSystems.Length == 0)
            {
                CurEventSystem = new GameObject("EventSystem").AddComponent<EventSystem>();
                CurEventSystem.gameObject.AddComponent<StandaloneInputModule>();
            }
            else
            {
                CurEventSystem = eventSystems[0];
                for (int i = 1; i < eventSystems.Length; i++)
                {
                    GameObject.Destroy(eventSystems[i].gameObject);
                }
            }
        }

        /// <summary>
        /// 获取层级
        /// </summary>
        /// <param name="layer"></param>
        /// <returns></returns>
        public Transform GetLayer(CanvasLayerData layer)
        {
            if ((byte)layer >= (byte)CanvasLayerData.LAYER_COUNT) return null;
            return mLayerTrans[(byte)layer];
        }
        /// <summary>
        /// 生成提示面板的背景
        /// </summary>
        private Button SpawnShowTipsPanel()
        {
            GameObject tipsPanel = new GameObject("TipsPanel");
            tipsPanel.transform.SetParent(GetLayer(CanvasLayerData.TIPS_LAYER), true);
            UITools.SetFullScreen(tipsPanel.AddComponent<RectTransform>());
            GameObject BG = new GameObject("BG");
            BG.transform.SetParent(GetLayer(CanvasLayerData.TIPS_LAYER), true);
            UITools.SetFullScreen(BG.AddComponent<RectTransform>());
            BG.AddComponent<Image>().color = new Color(130 / 255f, 130 / 255f, 130 / 255f, 70 / 255f);
            Button btn = BG.AddComponent<Button>();
            btn.transition = Selectable.Transition.None;
            tipsPanel.transform.Reset();
            BG.transform.Reset();
            return btn;
        }

        /// <summary>
        /// 生成Canvas
        /// </summary>
        /// <returns></returns>
        private GameObject SpawnCanvas()
        {
            GameObject canvas = new GameObject("_Canvas");
            canvas.layer = LayerMask.NameToLayer("UI");
            mLayerTrans = new RectTransform[mLayerCount];
            for (int i = 0; i < mLayerCount; i++)
            {
                mLayerTrans[i] = new GameObject(((CanvasLayerData)i).ToString()).AddComponent<RectTransform>();
                mLayerTrans[i].SetParent(canvas.transform, true);
                UITools.SetFullScreen(mLayerTrans[i]);
            }
            canvas.AddComponent<RectTransform>().pivot = Vector2.zero;
            Canvas tempCanvas = canvas.AddComponent<Canvas>();
            tempCanvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            tempCanvas.GetComponent<Canvas>().pixelPerfect = false;
            tempCanvas.GetComponent<Canvas>().sortingOrder = 0;
            tempCanvas.additionalShaderChannels = AdditionalCanvasShaderChannels.None;
            CanvasScaler tempCanvasSacler = canvas.AddComponent<CanvasScaler>();
            tempCanvasSacler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            if (YFrameworkHelper.IsInitHelper)
            {
                tempCanvasSacler.referenceResolution = new Vector2(YFrameworkHelper.Instance.ScreenSize.x, YFrameworkHelper.Instance.ScreenSize.y);
            }
            else
            {
                LogHelper.LogError("BaseCanvase: YFrameworkHelper not init");
            }
            tempCanvasSacler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            tempCanvasSacler.referencePixelsPerUnit = 100;
            GraphicRaycaster graphicRaycaster = canvas.AddComponent<GraphicRaycaster>();
            graphicRaycaster.ignoreReversedGraphics = true;
            graphicRaycaster.blockingObjects = GraphicRaycaster.BlockingObjects.None;
            return canvas;
        }

        public override void Update()
        {
            if (curPanel != null) curPanel.Update();
            if (mShowTipsPanel != null) mShowTipsPanel.Update();
            if (mLogManager != null) mLogManager.Update();
        }
        public override void OnDestory()
        {
            foreach (var item in mPopStack)
            {
                item.OnDestory();
            }
            mPopStack.Clear();
            mTempStackPanel.Clear();
            mClosePanel.Clear();
            if (mShowTipsPanel != null)
            {
                mShowTipsPanel.OnDestory();
                mShowTipsPanel = null;
            }
            if (mLogManager != null)
            {
                mLogManager.OnDestory();
                mLogManager = null;
            }
        }
        /// <summary>
        /// 显示面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ShowPanel<T>(Action<T> callBack = null) where T : class, IPanel, new()
        {
            T tempPanel = null;
            if (IsExist<T>()) //如果需要显示的界面在列表中的话
            {
                if (curPanel.GetType().Name == typeof(T).Name)
                {
                    callBack?.Invoke(curPanel as T);
                    return;
                }
                tempPanel = SetTopPanel<T>();
                callBack?.Invoke(tempPanel);
                return;
            }
            else
            {
                AsyncSpawnPanel<T>(
                (panel) =>
                {
                    tempPanel = panel;
                    if (tempPanel != null)
                    {
                        AddPanel(tempPanel);
                    }
                    if (curPanel != null)
                    {
                        curPanel.Hide();
                    }
                    curPanel = tempPanel;
                    tempPanel.Show();
                    tempPanel.transform.SetAsLastSibling();
                    callBack?.Invoke(tempPanel);
                }
                );
            }
        }
        /// <summary>
        /// 获取面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T FindPanel<T>() where T : class, IPanel, new()
        {
            //在打开的面板里面查找
            foreach (var item in mPopStack)
            {
                if (item is T) return item as T;
            }
            //在关闭的面板中查找
            for (int i = 0; i < mClosePanel.Count; i++)
            {
                if (mClosePanel[i] is T) return mClosePanel[i] as T;
            }
            return null;
        }


        /// <summary>
        ///  判断面板是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsExist(string name)
        {
            foreach (var item in mPopStack)
            {
                if (item.GetType().Name == name) return true;
            }
            return false;
        }


        /// <summary>
        /// 查找面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool IsExist<T>() where T : IPanel, new()
        {
            foreach (var item in mPopStack)
            {
                if (item is T) return true;
            }
            return false;
        }
        /// <summary>
        /// 查找面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IPanel FindPanel(string name)
        {
            foreach (var item in mPopStack)
            {
                if (item.GetType().Name == name) return item;
            }
            return null;
        }


        /// <summary>
        /// 添加面板
        /// </summary>
        /// <param name="panel"></param>
        public void AddPanel(IPanel panel)
        {
            if (panel == null) return;
            if (mPopStack.Contains(panel)) return;
            mPopStack.Push(panel);
        }
        /// <summary>
        /// 移除面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void RemovePanel<T>() where T : IPanel, new()
        {
            if (!IsExist<T>()) return;
            for (int i = 0; i < mClosePanel.Count; i++)//如果关闭面板中存在的话也现需要移除
            {
                if (mClosePanel[i].GetType().Name == typeof(T).Name)
                {
                    mClosePanel.RemoveAt(i);
                    break;
                }
            }
            IPanel tempPanel = null;
            while (mPopStack.Count != 0)
            {
                tempPanel = mPopStack.Pop();
                if (tempPanel is T)
                {
                    while (mTempStackPanel.Count != 0)
                    {
                        mPopStack.Push(mTempStackPanel.Pop());
                    }
                    tempPanel.OnDestory();
                    return;
                }
                else
                {
                    mTempStackPanel.Push(tempPanel);
                }
            }
            mTempStackPanel.Clear();
        }
        /// <summary>
        /// 实例化面板，但是不显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool InstantiatePanel<T>() where T : class, IPanel, new()
        {
            for (int i = 0; i < mClosePanel.Count; i++)//先检查下需要生成的面板是否在关闭列表中
            {
                if (mClosePanel[i].GetType().Name == typeof(T).Name) return true;//已经实例化了
            }
            T panel = new T();
            panel.SetCanvas(this);
            string assetPath = UIMap.Get(typeof(T).Name).assetPath;
            if (assetPath == null)
            {
                LogHelper.LogError("Panel:" + typeof(T).Name + "未找到对应的映射");
                return false;
            }
            GameObject target = ResourceHelper.LoadAsset<GameObject>(assetPath);
            if (target == null)
            {
                LogHelper.LogError(assetPath + "未找到对应的对象");
                return false;
            }
            target = GameObject.Instantiate(target, GetLayer((byte)CanvasLayerData.PANEL_LAYER));
            if (target == null)
            {
                LogHelper.LogError(assetPath + "未找到对应的层级" + CanvasLayerData.PANEL_LAYER);
                return false;
            }
            panel.SetTrans(target.transform);
            panel.Awake();
            panel.Start();
            panel.Hide();
            if (!mClosePanel.Contains(panel))
            {
                mClosePanel.Add(panel);//添加到关闭列表里面
            }
            return true;
        }
        /// <summary>
        /// 异步生成Panel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assetPath"></param>
        /// <returns></returns>
        private void AsyncSpawnPanel<T>(Action<T> loadCallBack) where T : class, IPanel, new()
        {
            for (int i = 0; i < mClosePanel.Count; i++)//先检查下需要生成的面板是否在关闭列表中
            {
                if (mClosePanel[i].GetType().Name == typeof(T).Name)
                {
                    loadCallBack?.Invoke(mClosePanel[i] as T);
                    return;
                }
            }
            T panel = new T();
            panel.SetCanvas(this);
            string assetPath = UIMap.Get(typeof(T).Name).assetPath;
            if (assetPath == null)
            {
                LogHelper.LogError("Panel:" + typeof(T).Name + "未找到对应的映射");

                return;
            }
            ResourceHelper.AsyncLoadAsset<GameObject>(assetPath,
            (target) =>
            {

                if (target == null)
                {
                    LogHelper.LogError(assetPath + "未找到对应的对象");
                    return;
                }
                target = GameObject.Instantiate(target, GetLayer((byte)CanvasLayerData.PANEL_LAYER));
                if (target == null)
                {
                    LogHelper.LogError(assetPath + "未找到对应的层级" + CanvasLayerData.PANEL_LAYER);
                    return;
                }
                target.name = typeof(T).Name;
                panel.SetTrans(target.transform);
                panel.Awake();
                panel.Start();
                loadCallBack?.Invoke(panel);
                return;
            });
        }
        /// <summary>
        /// 判断面板是否生成并显示
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>是否显示</returns>
        public bool IsShow<T>() where T : class, IPanel, new()
        {
            IPanel value = FindPanel<T>();
            if (value != null && value.isShow) return true;
            return false;
        }
        /// <summary>
        /// 隐藏面板
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>隐藏结果</returns>
        public void HidePanel<T>() where T : class, IPanel, new()
        {
            IPanel value = FindPanel<T>();
            if (value != null && value.isShow)
            {
                value.Hide();
            }
        }
        /// <summary>
        /// 把指定的面板显示的最上层
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private T SetTopPanel<T>() where T : class, IPanel, new()
        {
            IPanel tempPanel = null;
            while (mPopStack.Count != 0)
            {
                tempPanel = mPopStack.Pop();
                if (tempPanel.GetType().Name == typeof(T).Name)
                {
                    while (mTempStackPanel.Count != 0)
                    {
                        mPopStack.Push(mTempStackPanel.Pop());
                    }
                    mPopStack.Push(tempPanel);

                    if (curPanel != null) curPanel.Hide();
                    curPanel = tempPanel;
                    curPanel.Show();
                    if (!curPanel.isShow)
                    {
                        curPanel.Show();
                    }
                    curPanel.transform.SetAsLastSibling();
                    return tempPanel as T;
                }
                else
                {
                    mTempStackPanel.Push(tempPanel);
                }
            }
            mTempStackPanel.Clear();
            return null;
        }
        /// <summary>
        /// 关闭最上层的Panel
        /// </summary>
        public void CloseTopPanel()
        {
            if (mPopStack.Count != 0)
            {
                IPanel topPanel = mPopStack.Pop();
                topPanel.Hide();
                if (!mClosePanel.Contains(topPanel))
                {
                    mClosePanel.Add(topPanel);
                }
                else
                {
                    topPanel.OnDestory();
                }
            }
            if (mPopStack.Count != 0)
            {
                curPanel = mPopStack.Peek();
                curPanel.Show();
            }
        }
    }
}
