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
        public BaseTipsUI() :base()
        {

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
                    transform.position += new Vector3(Screen.width, 0, 0);
                    transform.ToMoveX(transform.position.x - Screen.width, ShowAnimTime);
                    break;
                case ShowAnimEnum.LeftToRightPos:
                    transform.position -= new Vector3(Screen.width, 0, 0);
                    transform.ToMoveX(transform.position.x + Screen.width, ShowAnimTime);
                    break;
                case ShowAnimEnum.TopToBottomPos:
                    transform.position += new Vector3(0, Screen.height, 0);
                    transform.ToMoveY(transform.position.y - Screen.height, ShowAnimTime);
                    break;
                case ShowAnimEnum.BottmToTopPos:
                    transform.position -= new Vector3(0, Screen.height, 0);
                    transform.ToMoveY(transform.position.y + Screen.height, ShowAnimTime);
                    break;
            }

        }
        public void Hide(Action finish)
        {
            if (!isShow) return;
            if(mShowTipsPanel.curShowCount== 0) 
               mShowTipsPanel.SetBGActive(false);
            switch (HideAnim)
            {
                case HideAnimEnum.NormalToZeroSize:
                    transform.ToScale(Vector3.zero, HideAnimTime )
                        .AddCompleteCallBack(() =>
                        {
                            base.Hide();
                            finish();
                        });
                    break;
                case HideAnimEnum.RightToLeftPos:
                    transform.ToMoveX(transform.position.x - Screen.width, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            transform.position += Vector3.right * Screen.width;
                            base.Hide();
                            finish();
                        })
                        ;
                    break;
                case HideAnimEnum.LeftToRightPos:
                    transform.ToMoveX(transform.position.x + Screen.width, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            transform.position -= Vector3.right * Screen.width;
                            base.Hide();
                            finish();
                        })
                        ;
                    break;
                case HideAnimEnum.TopToBottomPos:
                    transform.ToMoveY(transform.position.y - Screen.height, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            transform.position += Vector3.up * Screen.height;
                            base.Hide();
                            finish();
                        })
                        ;
                    break;
                case HideAnimEnum.BottmToTopPos:
                    transform.ToMoveY(transform.position.y + Screen.height, HideAnimTime)
                        .AddCompleteCallBack(() =>
                        {
                            transform.position -= Vector3.up * Screen.height;
                            base.Hide();
                            finish();
                        })
                        ;
                    break;
                case HideAnimEnum.None:
                    base.Hide();
                    finish();
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
