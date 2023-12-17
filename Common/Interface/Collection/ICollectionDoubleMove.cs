namespace YFramework
{
    /// <summary>
    /// 双向链表移动数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionDoubleMove<T> where T : class ,new()
    {
        /// <summary>
        /// 移动到头部
        /// </summary>
        /// <param name="node"></param>
        void MoveToHeader(T node);
        /// <summary>
        /// 移动到尾部
        /// </summary>
        /// <param name="node"></param>
        void MoveToTail(T node);
    }
}
