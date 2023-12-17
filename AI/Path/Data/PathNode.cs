using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public class PathNode
    {
        private Vector3 mPos;
        public Vector3 pos => mPos;
        private List<PathNode> mNearNodes;
        public List<PathNode> nearNodes => mNearNodes;
        private int ID;
        public int id => ID;
        public int preID;//上一个节点的ID
        public float MinDis;//当前最小路径
        public PathNode(int id, float x, float y, float z)
        {

            Init(id, new Vector3(x, y, z));
        }
        public PathNode(int id, Vector3 pos)
        {
            Init(id, pos);
        }
        private void Init(int id, Vector3 pos)
        {
            preID = -1;
            ID = id;
            mPos = pos;
        }
        public void AddNearNode(PathNode node)
        {
            if (mNearNodes == null)
            {
                mNearNodes = new List<PathNode>();
            }
            if (node == null)
            {
                Debug.LogError("Node is null");
                return;
            }
            if (mNearNodes.Contains(node))
            {
                Debug.Log("已包含点：" + ID);
                return;
            }
            mNearNodes.Add(node);
        }
    }
}
