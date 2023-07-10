using System.Text;

namespace YFramework
{
    /// <summary>
    /// 多分支树
    /// </summary>
    public class MultiTree<T>
    {
        public MultiTreeNode<T> mHead { get; private set; }//头节点
        public MultiTree()
        {
            mHead = new MultiTreeNode<T>(default(T),null);
        }
        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="target"></param>
        public void Add(MultiTreeNode<T> parentNode,T data)
        {
            if (parentNode == null) return;
            parentNode.AddChildNode(parentNode, data);
        }
        /// <summary>
        /// 添加节点(找到最近的跟这个数据对应的父节点对象)
        /// </summary>
        /// <param name="parentValue"></param>
        /// <param name="target"></param>
        public void Add(T parentValue, T data)
        {
            if (parentValue == null) return;
            MultiTreeNode<T> node = Find(parentValue);
            if (node == null) return;
            node.AddChildNode(node, data);
        }
        /// <summary>
        /// 根据值来删除节点
        /// </summary>
        /// <param name="target"></param>
        public void Remove(T target)
        {
            MultiTreeNode<T> node = Find(target);
            if (node == null) return;
            Remove(node);
        }
        /// <summary>
        /// 是否包含数据
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool Contains(T target) 
        {
            if (target == null) return false;
            MultiTreeNode<T> node = Find(target);
            return node != null;
        }
        /// <summary>
        /// 根据对象来删除节点
        /// </summary>
        /// <param name="target"></param>
        public void Remove(MultiTreeNode<T> target)
        {
            if (target == null) return;
            if (target.parentNode == null) return;
            target.parentNode.DeleteNode(target);
            target = null;
        }
        /// <summary>
        /// 重写ToString（）
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            Log(sb,mHead);
            return sb.ToString();
        }

        /// <summary>
        /// 打印消息
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="node"></param>
        private void Log(StringBuilder sb,MultiTreeNode<T> node)
        {
            if (node == null) return;
            if (node != mHead) {
                sb.Append(node.ToString());
            }
            if (!node.childNode.IsNullOrEmpty()) 
            {
                for (int i = 0; i < node.childNode.Count; i++)
                {
                    Log(sb, node.childNode[i]);
                }
            }
        }

        /// <summary>
        /// 查找节点(从头节点开始查找)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public MultiTreeNode<T> Find( T data) 
        {
            return FindByNode(mHead, data);
        }

        /// <summary>
        /// 查找节点（从哪个节点开始查找）
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private MultiTreeNode<T> FindByNode(MultiTreeNode<T> parent,T data)
        {
            if (data == null) return null;
            if (parent == null) return null;
            if (parent.data.Equals(data))
            {
                return parent;
            }
            if (!parent.childNode.IsNullOrEmpty())
            {
                for (int i = 0; i < parent.childNode.Count; i++)
                {
                    return FindByNode(parent.childNode[i], data);
                }
            }
            return null;
        }
    }

}
