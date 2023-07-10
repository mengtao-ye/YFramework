using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 多分枝树节点
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MultiTreeNode<T>
    {
        private MultiTreeNode<T> mParentNode;//父节点
        public MultiTreeNode<T> parentNode { get { return mParentNode; } }//父节点
        private List<MultiTreeNode<T>> mChildNode;//所有的子节点
        public List<MultiTreeNode<T>> childNode { get { return mChildNode; } }//所有的子节点
        private T mData;
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
        public override string ToString()
        {
            if (mData == null) return "";
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
