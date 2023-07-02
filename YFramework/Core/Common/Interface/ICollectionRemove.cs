namespace YFramework
{
    public interface ICollectionRemove<in T>
    {
        void Delete(T data);
    }
}
