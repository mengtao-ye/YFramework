namespace YFramework
{
    public class ListData<T> : BaseListData<T>
    {
        public override void Recycle()
        {
            ClassPool<ListData<T>>.Push(this);
        }
    }
}
