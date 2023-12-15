using System.Collections.Generic;

namespace YFramework
{
    public class NodeManager
    {
        private AIPath mAIPath;
        private Dictionary<int, PathNode> mNodes;
        public NodeManager(AIPath aiPath)
        {
            mAIPath = aiPath;
        }

        public void Awake()
        {
            mNodes = new Dictionary<int, PathNode>();
            SpawnNode();
        }
        public void Start()
        {

        }

        private void SpawnNode()
        {
            //PathNode node0 = new PathNode(0, 0, 0, 0);
            //PathNode node1 = new PathNode(1, 11.1f, 0, 8.1f);
            //PathNode node2 = new PathNode(2, 30.3f, 0, 8.4f);
            //PathNode node3 = new PathNode(3, 39.8f, 0, 0.3f);
            //PathNode node4 = new PathNode(4, 30.4f, 0, -10f);
            //PathNode node5 = new PathNode(5, 18.3f, 0, -0.9f);
            //PathNode node6 = new PathNode(6, 11.3f, 0, -9.2f);
            //PathNode node7 = new PathNode(7, 31.4f, 0, -24.2f);
            //PathNode node8 = new PathNode(8, 31.4f, 0, 22.4f);

            //mNodes.Add(0, node0);
            //mNodes.Add(1, node1);
            //mNodes.Add(2, node2);
            //mNodes.Add(3, node3);
            //mNodes.Add(4, node4);
            //mNodes.Add(5, node5);
            //mNodes.Add(6, node6);
            //mNodes.Add(7, node7);
            //mNodes.Add(8, node8);

            ////配置连接表
            //node0.AddNearNode(node1);
            //node0.AddNearNode(node5);
            //node0.AddNearNode(node6);

            //node1.AddNearNode(node0);
            //node1.AddNearNode(node5);
            //node1.AddNearNode(node2);

            //node2.AddNearNode(node1);
            //node2.AddNearNode(node3);
            //node2.AddNearNode(node5);
            //node2.AddNearNode(node8);

            //node3.AddNearNode(node2);
            //node3.AddNearNode(node4);

            //node4.AddNearNode(node2);
            //node4.AddNearNode(node3);
            //node4.AddNearNode(node5);
            //node4.AddNearNode(node6);

            //node5.AddNearNode(node4);
            //node5.AddNearNode(node6);
            //node5.AddNearNode(node0);
            //node5.AddNearNode(node1);
            //node5.AddNearNode(node2);

            //node6.AddNearNode(node4);
            //node6.AddNearNode(node5);
            //node6.AddNearNode(node0);
            //node6.AddNearNode(node7);

            //node7.AddNearNode(node6);

            //node8.AddNearNode(node2);
        }
        /// <summary>
        /// 查找对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PathNode Find(int id)
        {
            if (mNodes.ContainsKey(id)) return mNodes[id];
            return null;
        }
    }
}
