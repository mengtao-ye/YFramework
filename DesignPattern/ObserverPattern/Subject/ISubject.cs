namespace YFramework
{
    /// <summary>
    /// 观察者接口
    /// </summary>
    public interface ISubject
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Update();
    }
}
