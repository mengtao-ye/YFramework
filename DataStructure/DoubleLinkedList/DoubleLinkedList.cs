using System.Text;

namespace YFramework
{
    /// <summary>
    /// 双向链表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoubleLinkedList<T> : ICollectionDoubleAdd<DoubleLinkedNode<T>>, ICollectionDoubleAdd<T>, ICollectionDoubleMove<DoubleLinkedNode<T>>, ICollectionCount ,ICollectionContain<DoubleLinkedNode<T>>, ICollectionContain<T> ,ICollectionRemove<DoubleLinkedNode<T>>, ICollectionRemove<T>
    {
        /// <summary>
        /// 表头
        /// </summary>
        public DoubleLinkedNode<T> Head = null;
        /// <summary>
        /// 表尾
        /// </summary>
        public DoubleLinkedNode<T> Tail = null;
        /// <summary>
        /// 表内数量
        /// </summary>
        public int Count { get; private set; }
        /// <summary>
        /// 添加数据至头部
        /// </summary>
        /// <param name="data"></param>
        public void AddToHeader(T data)
        {
            DoubleLinkedNode<T> temp =  ClassPool<DoubleLinkedNode<T>>.Pop();
            temp.Next = null;
            temp.Pre = null;
            temp.data = data;
            AddToHeader(temp);
        }
        /// <summary>
        /// 添加节点至头部
        /// </summary>
        /// <param name="node"></param>
        public void AddToHeader(DoubleLinkedNode<T> node) {
            if (node == null) return;
            if (Contains(node))
            {
                UnityEngine.Debug.LogError(string.Format("节点\"{0}\"已经存在在List中，无法再次添加！",node.data.ToString()));
                return;
            }

            node.Pre = null;
            if (Head == null)
            {
                Head = Tail = node;
            }
            else {
                node.Next = Head;
                Head.Pre = node;
                Head = node;
            }
            Count++;
        }
        /// <summary>
        /// 添加数据至尾部
        /// </summary>
        /// <param name="data"></param>
        public void AddToTail(T data)
        {
            DoubleLinkedNode<T> temp = ClassPool<DoubleLinkedNode<T>>.Pop();
            temp.Next = null;
            temp.Pre = null;
            temp.data = data;
            AddToTail(temp);
        }
        /// <summary>
        /// 添加节点至尾部
        /// </summary>
        /// <param name="node"></param>
        public void AddToTail(DoubleLinkedNode<T> node)
        {
            if (node == null) return;
            if (Contains(node))
            {
                UnityEngine.Debug.LogError(string.Format("节点\"{0}\"已经存在在List中，无法再次添加！", node.data.ToString()));
                return;
            }
            node.Next = null;
            if (Tail == null)
            {
                Head = Tail = node;
            }
            else
            {
                node.Pre = Tail;
                Tail.Next = node;
                Tail = node;
            }
            Count++;
        }
        /// <summary>
        /// 节点是否包含在List中
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Contains(DoubleLinkedNode<T> data)
        {
            DoubleLinkedNode<T> temp = Head;
            while (temp != null)
            {
                if (data == temp) return true;
                temp = temp.Next;
            }
            return false;
        }
        /// <summary>
        /// 数据是否包含在List中 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Contains(T data)
        {
            DoubleLinkedNode<T> temp = Head;
            while (temp != null)
            {
                if (data.Equals( temp.data)) return true;
                temp = temp.Next;
            }
            return false;
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="data"></param>
        public void Delete(T data)
        {
            DoubleLinkedNode<T> temp = Head;
            while (temp != null)
            {
                if (data.Equals(temp.data))
                {
                    DeleteNode(temp);
                    return;
                }
                temp = temp.Next;
            }
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="node"></param>
        private void DeleteNode(DoubleLinkedNode<T> node) {
            if (node == null ||Count == 0 ) return;
            if (node == Head) {
                Head = node.Next;
            }
            if (node == Tail) {
                Tail = node.Pre;
            }
            if (node.Pre != null) {
                node.Pre.Next = node.Next;
            }
            if (node.Next != null) {
                node.Next.Pre = node.Pre;
            }
            Count--;
            ClassPool<DoubleLinkedNode<T>>.Push(node);
        }
        /// <summary>
        ///删除节点
        /// </summary>
        /// <param name="data"></param>
        public void Delete(DoubleLinkedNode<T> data)
        {
            DeleteNode(data);
        }
        /// <summary>
        /// 移动节点至头部
        /// </summary>
        /// <param name="node"></param>
        public void MoveToHeader(DoubleLinkedNode<T> node)
        {
            if (node == null || node == Head || Count == 0) return;
            if (node.Pre != null) {
                node.Pre.Next = node.Next;
                if (node == Tail)
                {
                    Tail = node.Pre;
                }
            }
            if (node.Next != null) {
                node.Next.Pre = node.Pre;
            }
            node.Pre = null;
            Head.Pre = node;
            node.Next = Head;
            Head = node;
        }
        /// <summary>
        /// 移动节点至尾部
        /// </summary>
        /// <param name="node"></param>
        public void MoveToTail(DoubleLinkedNode<T> node)
        {
            if (node == null || node == Tail || Count == 0) return;
            if (node.Pre != null)
            {
                node.Pre.Next = node.Next;
            }
            if (node.Next != null)
            {
                node.Next.Pre = node.Pre;
                if (node == Head)
                {
                    Head = node.Next;
                }
            }
            node.Next = null;
            Tail.Next = node;
            node.Pre = Tail;
            Tail = node;
        }
        /// <summary>
        /// 遍历数据
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (Count == 0) return string.Empty;
            StringBuilder sb = new StringBuilder();
            DoubleLinkedNode<T> temp = Head;
            while (temp != null) {
                sb.Append(temp.data.ToString()+",");
                temp = temp.Next;
            }
            return sb.ToString();
        }
    }
}
