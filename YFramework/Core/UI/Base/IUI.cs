using UnityEngine;

namespace YFramework
{
    public interface IUI:ILife
    {
        #region Field
        bool isShow { get; }
        Transform transform { get; } 
        string uiName { get; }
        #endregion

        #region Method
        void Show();
        void Hide();
        void InverseActive();
        void SetTrans(Transform trans); 
        #endregion
    }
}
