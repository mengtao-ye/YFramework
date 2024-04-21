using System;
using UnityEngine;

namespace YFramework
{
    public abstract class BaseTipsUI : BaseForm , ITipsUI
    {
        protected ITipsUIManager mShowTipsPanel;
        protected virtual ShowAnimEnum ShowAnim { get { return ShowAnimEnum.SmallToNormalSize; } }
        protected virtual HideAnimEnum HideAnim { get { return HideAnimEnum.None; } }
        protected virtual float ShowAnimTime { get { return 0.35f; } }
        protected virtual float HideAnimTime { get { return 0.35f; } }
        public int tipType { get; private set;}//该界面的类型
        public BaseTipsUI() :base()
        {

        }
        /// <summary>
        /// 设置类型
        /// </summary>
        /// <param name="type"></param>
        public void SetType(int type)
        {
            tipType = type;
        }
        /// <summary>
        /// 根据类型来隐藏界面
        /// </summary>
        /// <param name="type"></param>
        public void HideByType(int type)
        {
            if (tipType != 0 && tipType != type) return;
            Hide();
        }
        /// <summary>
        /// 设置提示面板
        /// </summary>
        /// <param name="panel"></param>
        public void SetShowTipsPanel(ITipsUIManager panel)
        {
            mShowTipsPanel = panel;
        }
        /// <summary>
        /// 设置背景点击响应
        /// </summary>
        /// <param name="callBack"></param>
        protected void SetBGClickCallBack(Action callBack)
        {
            mShowTipsPanel.SetBGClickCallBack(callBack);
        }

        public override void Show()
        {
            if (isShow) return;
            mShowTipsPanel.SetBGActive(true);
            base.Show();
            switch (ShowAnim)
            {
                case ShowAnimEnum.SmallToNormalSize:
                    transform.localScale = Vector3.zero;
                    transform.ToScale(Vector3.one, ShowAnimTime);
                    break;
                case ShowAnimEnum.BigToNormalSize:
                    transform.localScale = Vector3.one * 2;
                    transform.ToScale(Vector3.one, ShowAnimTime);
                    break;
                case ShowAnimEnum.RightToLeftPos:
                    rectTransform.anchoredPosition = new Vector2(Screen.width, 0);
                    rectTransform.ToAnchorPositionMoveX(0, ShowAnimTime);
                    break;
                case ShowAnimEnum.LeftToRightPos:
                    rectTransform.anchoredPosition = new Vector2(-Screen.width, 0);
                    rectTransform.ToAnchorPositionMoveX(0, ShowAnimTime);
                    break;
                case ShowAnimEnum.TopToBottomPos:
                    rectTransform.anchoredPosition = new Vector3(0, Screen.height);
                    rectTransform.ToAnchorPositionMoveY(0, ShowAnimTime);
                    break;
                case ShowAnimEnum.BottomToTopPos:
                    rectTransform.anchoredPosition = new Vector2(0, -Screen.height);
                    rectTransform.ToAnchorPositionMoveY(0, ShowAnimTime);
                    break;
            }

        }
        public void Hide(Action finish)
        {
            if (!isShow) return;
        
            switch (HideAnim)
            {
                case HideAnimEnum.NormalToZeroSize:
                    transform.localScale = Vector3.one;
                    transform.ToScale(Vector3.zero, HideAnimTime )
                        .AddCompleteCallBack(() =>
                        {
                            base.Hide();
                            if (mShowTipsPanel.curShowCount == 0)
                                mShowTipsPanel.SetBGActive(false);
                            finish?.Invoke();
                        });
                    break;
                case HideAnimEnum.RightToLeftPos:
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.ToAnchorPositionMoveX( - Screen.width, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            base.Hide();
                            if (mShowTipsPanel.curShowCount == 0)
                                mShowTipsPanel.SetBGActive(false);
                            finish?.Invoke();
                        })
                        ;
                    break;
                case HideAnimEnum.LeftToRightPos:
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.ToAnchorPositionMoveX(Screen.width, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            base.Hide();
                            if (mShowTipsPanel.curShowCount == 0)
                                mShowTipsPanel.SetBGActive(false);
                            finish?.Invoke();
                        })
                        ;
                    break;
                case HideAnimEnum.TopToBottomPos:
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.ToAnchorPositionMoveY(- Screen.height, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            base.Hide();
                            if (mShowTipsPanel.curShowCount == 0)
                                mShowTipsPanel.SetBGActive(false);
                            finish?.Invoke();
                        })
                        ;
                    break;
                case HideAnimEnum.BottmToTopPos:
                    rectTransform.anchoredPosition = Vector2.zero;
                    rectTransform.ToAnchorPositionMoveY(Screen.height, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            base.Hide();
                            if (mShowTipsPanel.curShowCount == 0)
                                mShowTipsPanel.SetBGActive(false);
                            finish?.Invoke();
                        })
                        ;
                    break;
                case HideAnimEnum.None:
                    base.Hide();
                    finish();
                    if (mShowTipsPanel.curShowCount == 0)
                        mShowTipsPanel.SetBGActive(false);
                    break;
            }
        }
        public override void Hide()
        {
            mShowTipsPanel.Hide();
            mShowTipsPanel.ClearBGClickCallBack();
        }
    }
}
