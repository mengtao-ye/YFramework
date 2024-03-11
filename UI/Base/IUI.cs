using UnityEngine;

namespace YFramework
{
    public interface IUI:ILife,IRefresh
    {
        #region Field
        bool isShow { get; }
        Transform transform { get; } 
        string uiName { get; }
        ICanvas mUICanvas { get; }
        #endregion

        #region Method
        void Show();
        void Hide();
        void InverseActive();
        void SetTrans(Transform trans);
        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="canvas"></param>
        void SetCanvas(ICanvas canvas);
        void FirstShow();
        #endregion
    }
}
