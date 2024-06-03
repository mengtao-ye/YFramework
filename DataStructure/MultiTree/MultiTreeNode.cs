using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 多分枝树节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultiTreeNode<T>
    {
        /// <summary>
        /// 父节点
        /// </summary>
        private MultiTreeNode<T> mParentNode;
        /// <summary>
        /// 父节点
        /// </summary>
        public MultiTreeNode<T> parentNode { get { return mParentNode; } }
        /// <summary>
        /// 所有的子节点
        /// </summary>
        private List<MultiTreeNode<T>> mChildNode;
        /// <summary>
        /// 所有的子节点
        /// </summary>
        public List<MultiTreeNode<T>> childNode { get { return mChildNode; } }
        /// <summary>
        ///数据
        /// </summary>
        private T mData;
        /// <summary>
        ///数据
        /// </summary>
        public T data { get { return mData; } }
        public MultiTreeNode(T data, MultiTreeNode<T> parent)
        {
            mData = data;
            mParentNode = parent;
        }
        /// <summary>
        /// 添加子节点
        /// </summary>
        public void AddChildNode(MultiTreeNode<T> parent, T data)
        {
            MultiTreeNode<T> node = new MultiTreeNode<T>(data, parent);
            if (mChildNode == null) mChildNode = new List<MultiTreeNode<T>>();
            mChildNode.Add(node);
        }
        /// <summary>
        /// 重载ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (mData == null) return string.Empty;
            return mData.ToString();
        }
        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="target"></param>
        public void DeleteNode(MultiTreeNode<T> target) {
            if (target == null) return;
            if (mChildNode.IsNullOrEmpty()) return;
            if (mChildNode.Contains(target)) {
                mChildNode.Remove(target);
            }
        }
    }
}
