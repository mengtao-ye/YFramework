namespace YFramework
{
    public abstract class BaseLogUI : BaseUI,ILogUI
    {
        protected ILogUIManager mLogUIManager;
        public BaseLogUI()
        {

        }
        /// <summary>
        /// 设置提示面板
        /// </summary>
        /// <param name="panel"></param>
        public void SetLogUIManager(ILogUIManager panel)
        {
            mLogUIManager = panel;
        }
    }
}
