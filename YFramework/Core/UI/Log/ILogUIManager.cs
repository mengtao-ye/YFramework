namespace YFramework
{
    public interface ILogUIManager : IPanel
    {
        T ShowLogUI<T>() where T : class, ILogUI, new();
        T GetLogUI<T>() where T : class, ILogUI, new();
    }
}
