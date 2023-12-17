namespace YFramework
{
    public class DictionaryData<TKey, TValue> : BaseDictionaryData<TKey, TValue>
    {
        public override void Recycle()
        {
            ClassPool<DictionaryData<TKey, TValue>>.Push(this);
        }
    }
}
