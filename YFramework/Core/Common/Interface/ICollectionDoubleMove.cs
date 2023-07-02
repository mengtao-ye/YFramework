namespace YFramework
{
    public interface ICollectionDoubleMove<T> where T : class ,new()
    {
        void MoveToHeader(T node);
        void MoveToTail(T node);
    }
}
