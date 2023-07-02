using System.Collections.Generic;

namespace YFramework
{
    public abstract class BaseSubject : ISubject
    {
        private List<IObserver> mObservers;
        public List<IObserver> observers { get { return mObservers; } private set { } }
        public BaseSubject()
        {
            mObservers = new List<IObserver>();
        }

        public void AddObserver(IObserver observer) {
            if (observer == null) return;
            mObservers.Add(observer);
        }

        public void Update()
        {
            for (int i = 0; i < mObservers.Count; i++)
            {
                mObservers[i].Update();
            }
        }
    }
}