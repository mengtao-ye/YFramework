namespace YFramework 
{
    public interface ICollectionContain<in T>
    {
        bool Contains(T data);
    }
}
