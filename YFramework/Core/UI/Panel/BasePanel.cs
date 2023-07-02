namespace YFramework
{
    public abstract class BasePanel : BaseForm ,IPanel
    {
        public ICanvas mUICanvas { get; private set; }
        public BasePanel() : base()
        {

        }
        public void SetCanvas(ICanvas canvas)
        {
            mUICanvas = canvas;
        }

        public override void Show()
        {
            base.Show();
        }
    }
}