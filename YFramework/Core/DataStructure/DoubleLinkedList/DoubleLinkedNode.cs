namespace YFramework
{
    public class DoubleLinkedNode<T> : IReset
    {
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
        public DoubleLinkedNode(T data)
        {
            this.data = data;
        }
        public void Reset()
        {
            Pre = null;
            Next = null;
            data = default(T);
        }
    }
}
