namespace YFramework
{
    public interface ICanvas : IUI
    {
        ILogUIManager logUIManager { get; }
        ITipsUIManager showTipsPanel { get; }
        IScene scene { get; }
        IMap<string, UIMapperData> UIMap { get;  }
        void CloseTopPanel();
        T ShowPanel<T>() where T : class, IPanel, new();
        T FindPanel<T>() where T : class, IPanel, new();
        bool IsExist<T>() where T : IPanel, new();
        bool IsExist(string name);
        IPanel FindPanel(string name);
        void AddPanel(IPanel panel);
        void RemovePanel<T>() where T : IPanel, new();
        bool IsShow<T>() where T : class, IPanel, new();
        void HidePanel<T>() where T : class, IPanel, new();
    }
}
