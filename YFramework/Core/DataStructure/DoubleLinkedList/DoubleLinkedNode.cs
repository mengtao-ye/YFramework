namespace YFramework
{
    public class DoubleLinkedNode<T> : IPool
    {
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
        public void PopPool()
        {
            
        }
        public void PushPool()
        {
            Pre = null;
            Next = null;
            data = default(T);
        }

        public void Recycle()
        {
            ClassPool<DoubleLinkedNode<T>>.Push(this);
        }
    }
}
