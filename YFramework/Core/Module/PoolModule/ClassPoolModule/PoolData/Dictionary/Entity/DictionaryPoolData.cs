namespace YFramework
{
    public class DictionaryPoolData<TKey, TValue> : BaseDictionaryData<TKey,TValue> where TValue : IPool
    {
        public override void Recycle()
        {
            mDict.Foreach(Foreach);
            ClassPool<DictionaryPoolData<TKey, TValue>>.Push(this);
        }
        private void Foreach(TKey key,TValue value)
        {
            if (value != null) value.Recycle();
        }
    }
}
