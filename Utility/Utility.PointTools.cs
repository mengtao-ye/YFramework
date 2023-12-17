using System;
using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// 点工具
        /// </summary>
        public static class PointTools
        {
            /// <summary>
            /// 判断点是否在多边形内
            /// </summary>
            /// <param name="checkPoint">需要判断的点</param>
            /// <param name="polygonPoints">组成多边形点的集合</param>
            /// <returns></returns>
            public static bool IsInPolygon2(Vector2 checkPoint, List<Vector2> polygonPoints)
            {
                int counter = 0;
                int i;
                double xinters;
                Vector2 p1, p2;
                int pointCount = polygonPoints.Count;
                p1 = polygonPoints[0];
                for (i = 1; i <= pointCount; i++)
                {
                    p2 = polygonPoints[i % pointCount];
                    if (checkPoint.y > Math.Min(p1.y, p2.y)//校验点的Y大于线段端点的最小Y
                        && checkPoint.y <= Math.Max(p1.y, p2.y))//校验点的Y小于线段端点的最大Y
                    {
                        if (checkPoint.x <= Math.Max(p1.x, p2.x))//校验点的X小于等线段端点的最大X(使用校验点的左射线判断).
                        {
                            if (p1.y != p2.y)//线段不平行于X轴
                            {
                                xinters = (checkPoint.y - p1.y) * (p2.x - p1.x) / (p2.y - p1.y) + p1.x;
                                if (p1.x == p2.x || checkPoint.x <= xinters)
                                {
                                    counter++;
                                }
                            }
                        }
                    }
                    p1 = p2;
                }
                if (counter % 2 == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }

}