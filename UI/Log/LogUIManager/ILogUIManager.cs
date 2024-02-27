using System;

namespace YFramework
{
    public interface ILogUIManager : IPanel
    {
        void ShowLogUI<T>(Action<T> action) where T : class, ILogUI, new();
        void GetLogUI<T>(Action<T> action) where T : class, ILogUI, new();
    }
}
