using System;

namespace YFramework
{
    public interface ITipsUI : IUI
    {
        int tipType { get; }//该界面的类型
        void Hide(Action finish);
        void SetShowTipsPanel(ITipsUIManager panel);
    }
}
