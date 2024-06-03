using System;

namespace YFramework
{
    public interface ILogUIManager : IPanel
    {
        void ShowLogUI<T>(Action<T> action) where T : class, ILogUI, new();
        T GetLogUI<T>() where T : class, ILogUI, new();
        void HideLogUI<T>() where T : class, ILogUI, new();
    }
}
