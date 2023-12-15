using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace YFramework
{
    public class CalcPathManager
    {
        private AIPath mAIPath;
        public CalcPathManager(AIPath aiPath)
        {
            mAIPath = aiPath;
        }
        public void Awake()
        {
        }
        public void Start()
        {

        }
        /// <summary>
        /// 查找路径
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public int[] FindPath(int startIndex, int endIndex)
        {
            PathNode startNode = mAIPath.nodeManager.Find(startIndex);
            if (startNode == null)
            {
                Debug.LogError("StartIndex is not found:" + startIndex);
                return null;
            }
            List<PathNode> closeList = new List<PathNode>();//存放关闭列表
            List<List<int>> allRoads = new List<List<int>>();//所有可选择的路径 
            FindRoad(startNode, null, closeList, ref allRoads, endIndex);//查找路径
            return FindBastLines(allRoads);
        }
        /// <summary>
        /// 寻找最优路径
        /// </summary>
        /// <returns></returns>
        private int[] FindBastLines(List<List<int>> allRoads)
        {
            if (allRoads == null || allRoads.Count == 0) return null;
            int maxIndex = 0;
            float minDis = float.MaxValue;
            for (int i = 0; i < allRoads.Count; i++)
            {
                float dis = 0;
                if (allRoads[i].Count >= 2)
                {
                    for (int j = 0; j < allRoads[i].Count - 1; j++)
                    {
                        PathNode node1 = mAIPath.nodeManager.Find(allRoads[i][j]);
                        PathNode node2 = mAIPath.nodeManager.Find(allRoads[i][j + 1]);
                        dis += Vector3.Distance(node1.pos, node2.pos);
                    }
                }
                if (dis < minDis)
                {
                    minDis = dis;
                    maxIndex = i;
                }
            }
            return allRoads[maxIndex].ToArray();
        }
        /// <summary>
        /// 获取成功的路线
        /// </summary>
        /// <param name="lastNode"></param>
        /// <returns></returns>
        private List<int> GetLines(PathNode lastNode)
        {
            if (lastNode == null) return null;
            List<int> roadList = new List<int>();
            roadList.Add(lastNode.id);
            while (lastNode != null && lastNode.preID != -1)
            {
                roadList.Add(lastNode.preID);
                lastNode = mAIPath.nodeManager.Find(lastNode.preID);
            }
            return roadList;
        }
        /// <summary>
        /// 递归查找对象
        /// </summary>
        /// <param name="node"></param>
        private void FindRoad(PathNode node, PathNode preNode, List<PathNode> closeList, ref List<List<int>> allRoads, int endIndex)
        {
            if (node == null) return;
            if (node.id == endIndex)//找到终点
            {
                node.preID = preNode == null ? -1 : preNode.id;//设置终点的上一个节点的ID
                List<int> lines = GetLines(node);//获取路线
                if (lines != null && lines.Count != 0)
                {
                    //把这次成功的路线记录下来
                    allRoads.Add(lines);
                }
                return;
            }
            float bastValue = -1;//最优解数据
            if (closeList.Contains(node))//在关闭列表中
            {
                float curDis = preNode.MinDis + Vector3.Distance(node.pos, preNode.pos);//当前点到目标点的距离
                if (curDis >= node.MinDis)
                {
                    //到达目标点无更优解
                    return;
                }

                bastValue = curDis;//记录最优解
            }
            if (preNode != null)//非第一个点
            {
                node.preID = preNode.id;//设置上一个节点的ID
                if (bastValue != -1)//有最优解
                {
                    if (node.MinDis != 0)//不是初始值
                    {
                        if (node.MinDis > bastValue)//如果有更小的情况的话
                            node.MinDis = bastValue;
                    }
                    else
                    {
                        node.MinDis = bastValue;//初始值的情况就直接赋值
                    }

                }
                else
                {
                    float curDis = preNode.MinDis + Vector3.Distance(node.pos, preNode.pos);//当前点到目标点的距离
                    if (node.MinDis != 0)//不是初始值
                    {
                        if (node.MinDis > curDis)//如果有更小的情况的话
                            node.MinDis = curDis;
                    }
                    else
                    {
                        node.MinDis = curDis;//初始值的情况就直接赋值
                    }
                }
            }
            else
            {
                node.preID = -1;//设置第一个点的上一个节点的ID
            }
            closeList.Add(node);//加入关闭列表
            if (node.nearNodes != null && node.nearNodes.Count != 0)
            {
                for (int i = 0; i < node.nearNodes.Count; i++)
                {
                    FindRoad(node.nearNodes[i], node, closeList, ref allRoads, endIndex);//递归查询
                }
            }
        }
    }
}
