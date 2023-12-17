namespace YFramework
{
    public interface IForm : IUI
    {
        void AddSubUI(ISubUI subUI);
        ISubUI FindSubUI(string targetName);
        T FindSubUI<T>() where T : class, ISubUI;
    }
}
