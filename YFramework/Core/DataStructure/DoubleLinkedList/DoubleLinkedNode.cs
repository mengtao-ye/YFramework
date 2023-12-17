namespace YFramework
{
    /// <summary>
    /// 双向链表节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoubleLinkedNode<T> : IPool
    {
        /// <summary>
        /// 是否使用
        /// </summary>
        public bool isPop { get; set; }
        /// <summary>
        /// 前一个节点
        /// </summary>
        public DoubleLinkedNode<T> Pre = null;
        /// <summary>
        /// 下一个节点
        /// </summary>
        public DoubleLinkedNode<T> Next = null;
        /// <summary>
        /// 数据
        /// </summary>
        public T data = default(T);
        public DoubleLinkedNode()
        {

        }
        /// <summary>
        /// 使用时执行
        /// </summary>
        public void PopPool()
        {
            
        }
        /// <summary>
        /// 放入时执行
        /// </summary>
        public void PushPool()
        {
            Pre = null;
            Next = null;
            data = default(T);
        }
        /// <summary>
        /// 回收
        /// </summary>
        public void Recycle()
        {
            ClassPool<DoubleLinkedNode<T>>.Push(this);
        }
    }
}
