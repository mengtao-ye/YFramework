using System;
using UnityEngine.UI;

namespace YFramework
{
    public interface ITipsUIManager :IPanel
    {
        int curShowCount { get; }
        void ShowTipsUI<T>(Action<T> action) where T :class, ITipsUI, new();
        void GetTipsUI<T>(Action<T> action) where T : class, ITipsUI, new();
        void HideTipsUI<T>() where T : class, ITipsUI, new();
        void HideTipsUI<T>(int type) where T : class, ITipsUI, new();
        T FindTipsPanel<T>() where T : class, ITipsUI, new();
        bool IsShow<T>() where T : class, ITipsUI, new();
        void SetBGActive(bool active);
        void SetBGClickCallBack(Action callBack);
        void ClearBGClickCallBack();
        void SetBG(Button bgImg);
    }
}
