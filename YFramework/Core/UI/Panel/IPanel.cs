namespace YFramework
{
    public interface IPanel : IUI
    {
        ICanvas mUICanvas { get; }
        void SetCanvas(ICanvas canvas);
    }
}
