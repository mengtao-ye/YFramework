using System;
using UnityEngine.UI;

namespace YFramework
{
    public interface ITipsUIManager :IPanel
    {
        int curShowCount { get; }
        T ShowTipsUI<T>() where T :class, ITipsUI, new();
        T GetTipsUI<T>() where T : class, ITipsUI, new();
        void HideTipsUI<T>() where T : class, ITipsUI, new();
        T FindTipsPanel<T>() where T : class, ITipsUI, new();
        bool IsShow<T>() where T : class, ITipsUI, new();
        void SetBGActive(bool active);
        void SetBGClickCallBack(Action callBack);
        void ClearBGClickCallBack();
        void SetBG(Button bgImg);
    }
}
