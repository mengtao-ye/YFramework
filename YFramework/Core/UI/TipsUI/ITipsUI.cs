using System;

namespace YFramework
{
    public interface ITipsUI : IUI
    {
        void Hide(Action finish);
        void SetShowTipsPanel(ITipsUIManager panel);
    }
}
