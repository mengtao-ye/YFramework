namespace YFramework
{
    /// <summary>
    /// 双向链表添加接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionDoubleAdd<T>
    {
        /// <summary>
        /// 向头部添加数据
        /// </summary>
        /// <param name="data"></param>
        void AddToHeader(T data);
        /// <summary>
        /// 向尾部添加数据
        /// </summary>
        /// <param name="data"></param>
        void AddToTail(T data);
    }
}
