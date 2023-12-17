namespace YFramework
{
    public class ListPoolData<T> : BaseListData<T> where T : IPool
    {
        public override void Recycle()
        {
            for (int i = 0; i < mList.Count; i++)
            {
                mList[i].Recycle();
            }
            ClassPool<ListPoolData<T>>.Push(this);
        }
    }
}
