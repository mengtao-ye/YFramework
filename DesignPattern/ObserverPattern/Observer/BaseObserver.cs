namespace YFramework
{
    /// <summary>
    /// 监听者基类
    /// </summary>
    public abstract class BaseObserver  : IObserver
    {
        /// <summary>
        /// 观察者
        /// </summary>
        protected ISubject mSubject;
        public BaseObserver(ISubject baseSubject)
        {
            if (baseSubject == null)
            {
                return;
            }
            mSubject = baseSubject;
        }
        /// <summary>
        /// 数据更新时
        /// </summary>
        public abstract void Update();
    }
}