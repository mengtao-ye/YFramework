namespace YFramework
{
    /// <summary>
    /// 集合移除
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionRemove<in T>
    {
        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="data"></param>
        void Delete(T data);
    }
}
