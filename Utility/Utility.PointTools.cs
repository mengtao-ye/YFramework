using System;
using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// �㹤��
        /// </summary>
        public static class PointTools
        {
            /// <summary>
            /// �жϵ��Ƿ��ڶ������
            /// </summary>
            /// <param name="checkPoint">��Ҫ�жϵĵ�</param>
            /// <param name="polygonPoints">��ɶ���ε�ļ���</param>
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
                    if (checkPoint.y > Math.Min(p1.y, p2.y)//У����Y�����߶ζ˵����СY
                        && checkPoint.y <= Math.Max(p1.y, p2.y))//У����YС���߶ζ˵�����Y
                    {
                        if (checkPoint.x <= Math.Max(p1.x, p2.x))//У����XС�ڵ��߶ζ˵�����X(ʹ��У�����������ж�).
                        {
                            if (p1.y != p2.y)//�߶β�ƽ����X��
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