using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// ���������߹���
        /// </summary>
        public class BesierLineTools
        {
            /// <summary>
            /// ���α���������
            /// </summary>
            /// <param name="startPoint"></param>
            /// <param name="controlPoint"></param>
            /// <param name="endPoint"></param>
            /// <param name="segmentNum"></param>
            /// <returns></returns>
            public static Vector3[] GetBeizerList_2(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
            {
                Vector3[] path = new Vector3[segmentNum + 1];
                for (int i = 0; i <= segmentNum; i++)
                {
                    float t = i / (float)segmentNum;
                    Vector3 pixel = Bezier_2(startPoint,
                        controlPoint, endPoint, t);
                    path[i] = pixel;
                }
                return path;
            }
            /// <summary>
            /// ���α���������
            /// </summary>
            /// <param name="p0"></param>
            /// <param name="p1"></param>
            /// <param name="p2"></param>
            /// <param name="t">[0-1]</param>
            /// <returns></returns>
            private static Vector3 Bezier_2(Vector3 p0, Vector3 p1, Vector3 p2, float t)
            {
                return (1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2);
            }

            /// <summary>
            /// ���α���������
            /// </summary>
            /// <param name="startPoint"></param>
            /// <param name="controlPoint_1"></param>
            /// <param name="controlPoint_2"></param>
            /// <param name="endPoint"></param>
            /// <param name="segmentNum"></param>
            /// <returns></returns>
            public static Vector3[] GetBeizerList_3(Vector3 startPoint, Vector3 controlPoint_1, Vector3 controlPoint_2, Vector3 endPoint, int segmentNum)
            {
                Vector3[] path = new Vector3[segmentNum + 1];
                for (int i = 0; i <= segmentNum; i++)
                {
                    float t = i / (float)segmentNum;
                    Vector3 pixel = Bezier_3(startPoint,
                        controlPoint_1, controlPoint_2, endPoint, t);
                    path[i] = pixel;
                }
                return path;


            }
            /// <summary>
            /// ���α���������
            /// </summary>
            /// <param name="p0"></param>
            /// <param name="p1"></param>
            /// <param name="p2"></param>
            /// <param name="p3"></param>
            /// <param name="t"></param>
            /// <returns></returns>
            private static Vector3 Bezier_3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
            {
                return (1 - t) * ((1 - t) * ((1 - t) * p0 + t * p1) + t * ((1 - t) * p1 + t * p2)) + t * ((1 - t) * ((1 - t) * p1 + t * p2) + t * ((1 - t) * p2 + t * p3));
            }


            /// <summary>
            /// n�α���������
            /// </summary>
            /// <param name="vertex"></param>
            /// <param name="vertexCount"></param>
            /// <returns></returns>
            public static Vector3[] GetBeizerList_n(Vector3[] vertex, int vertexCount)
            {
                List<Vector3> pointList = new List<Vector3>();
                pointList.Clear();
                for (float ratio = 0; ratio <= 1; ratio += 1.0f / vertexCount)
                {
                    pointList.Add(Bezier_n(vertex, ratio));
                }
                //  pointList.Add(vertex[vertex.Length - 1]);
                return pointList.ToArray();
            }

            private static Vector3 Bezier_n(Vector3[] vecs, float t)
            {
                Vector3[] temp = new Vector3[vecs.Length];
                for (int i = 0; i < temp.Length; i++)
                {
                    temp[i] = vecs[i];
                }
                //���㼯���ж೤�����ߵ�ÿһ�������Ҫ������ٴΡ�
                int n = temp.Length - 1;
                for (int i = 0; i < n; i++)
                {
                    //���μ�����������ڵĶ���Ĳ�ֵ�������棬ÿ�μ��㶼����н��ס�ʣ����ٽ׼�����ٴΡ�ֱ���õ����һ���������ߡ�
                    for (int j = 0; j < n - i; j++)
                    {
                        temp[j] = Vector3.Lerp(temp[j], temp[j + 1], t);
                    }
                }
                //���ص�ǰ���������ߵĵ�
                return temp[0];
            }

        }
    }

}