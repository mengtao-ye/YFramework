using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 观察者基类
    /// </summary>
    public abstract class BaseSubject : ISubject
    {
        /// <summary>
        /// 监听者数组
        /// </summary>
        private IList<IObserver> mObservers;
        /// <summary>
        /// 监听者数组
        /// </summary>
        public IList<IObserver> observers { get { return mObservers; } private set { } }
        public BaseSubject()
        {
            mObservers = new List<IObserver>();
        }
        /// <summary>
        /// 添加监听者
        /// </summary>
        /// <param name="observer"></param>
        public void AddObserver(IObserver observer) {
            if (observer == null) return;
            if (!mObservers.Contains(observer))
            {
                mObservers.Add(observer);
            }
        }

        /// <summary>
        /// 移除监听者
        /// </summary>
        /// <param name="observer"></param>
        public void RemoveObserver(IObserver observer)
        {
            if (observer == null) return;
            for (int i = 0; i < mObservers.Count; i++)
            {
                if(mObservers[i] == observer) {
                    mObservers.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 广播事件给监听者
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < mObservers.Count; i++)
            {
                mObservers[i].Update();
            }
        }
    }
}