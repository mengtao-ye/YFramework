namespace YFramework
{
    public abstract class BaseObserver  : IObserver
    {
        protected ISubject mSubject;
        public BaseObserver(ISubject baseSubject)
        {
            if (baseSubject == null)
            {
                return;
            }
            mSubject = baseSubject;
        }
        public abstract void Update();
    }
}