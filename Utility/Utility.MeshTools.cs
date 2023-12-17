using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        ///网格数据工具
        /// </summary>
        public static class MeshTools
        {
            /// <summary>
            /// 根据点生成平面模型
            /// </summary>
            /// <param name="vectors"></param>
            /// <param name="mat"></param>
            /// <returns></returns>
            public static GameObject SpawnSurface(List<Vector3> vectors, Material mat)
            {
                if (vectors == null || vectors.Count <= 2) return null;
                GameObject surface = new GameObject("Surface");
                Mesh mesh = new Mesh();
                List<int> triangles = new List<int>();
                for (int i = 0; i < vectors.Count - 1; i++)
                {
                    triangles.Add(i);
                    triangles.Add(i + 1);
                    triangles.Add(vectors.Count - i - 1);
                    //双面
                    triangles.Add(vectors.Count - i - 1);
                    triangles.Add(i + 1);
                    triangles.Add(i);
                }
                mesh.vertices = vectors.ToArray();
                mesh.triangles = triangles.ToArray();
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
                surface.AddComponent<MeshFilter>().mesh = mesh;
                surface.AddComponent<MeshRenderer>().material = mat;
                return surface;
            }

            /// <summary>
            /// 根据点生成路线
            /// </summary>
            /// <param name="pos">线条的点</param>
            /// <param name="mat">线条的材质</param>
            /// <param name="width">线条宽度</param>
            public static GameObject SpawnLines(List<Vector3> pos, Material mat, float width)
            {
                if (pos == null || pos.Count <= 1) return null;
                GameObject lines = new GameObject("Lines");
                List<Vector3> vertices = new List<Vector3>();
                List<int> triangles = new List<int>();
                //开始的点
                Vector3 pos1ChuiZhiVector = Vector3.Cross(pos[1] - pos[0], Vector3.up).normalized * width * 0.5f;
                Vector3 pos1RightPoint = pos[0] + pos1ChuiZhiVector;
                Vector3 pos1LeftPoint = pos[0] - pos1ChuiZhiVector;
                vertices.Add(pos1RightPoint);
                vertices.Add(pos1LeftPoint);
                Vector3 preDir = Vector3.zero;
                for (int i = 0; i < pos.Count - 2; i++)
                {
                    Vector3 dir1 = (pos[i] - pos[i + 1]).normalized;
                    Vector3 dir2 = (pos[i + 2] - pos[i + 1]).normalized;
                    bool isLeft = IsLeft(pos[i], pos[i + 2], pos[i + 1]);
                    float angle = Vector3.Angle(dir1, dir2);
                    bool isPingXing = angle == 180 || angle == 0;
                    Vector3 dir = Vector3.zero;
                    dir = (dir1 + dir2).normalized;
                    if (isLeft)
                    {
                        dir = -dir;
                    }
                    if (isPingXing)
                    {
                        dir = Vector3.Cross(dir1, Vector3.up);
                        float preAngle = Vector3.Angle(dir, preDir);
                        if (preAngle > 90 || preAngle == 0 || preAngle == 180)
                        {
                            dir = -dir;
                        }
                    }
                    if (isPingXing)
                    {
                        dir = dir.normalized + dir.normalized * (Mathf.Sin(angle) + 1) / 2 * 0.5f;
                    }
                    else
                    {
                        dir = dir.normalized + dir.normalized * 0.3f;
                    }
                    vertices.Add(pos[i + 1] + dir * width * 0.5f);
                    vertices.Add(pos[i + 1] - dir * width * 0.5f);
                    preDir = dir;
                }
                //最后的点
                Vector3 posEndChuiZhiVector = Vector3.Cross(pos[pos.Count - 1] - pos[pos.Count - 2], Vector3.up).normalized * width * 0.5f;
                Vector3 posEndRightPoint = pos[pos.Count - 1] + posEndChuiZhiVector;
                Vector3 posEndLeftPoint = pos[pos.Count - 1] - posEndChuiZhiVector;
                vertices.Add(posEndRightPoint);
                vertices.Add(posEndLeftPoint);
                for (int i = 0; i < vertices.Count - 2; i += 2)
                {
                    triangles.Add(i);
                    triangles.Add(i + 2);
                    triangles.Add(i + 3);

                    triangles.Add(i + 2);
                    triangles.Add(i + 3);
                    triangles.Add(i);

                    triangles.Add(i + 3);
                    triangles.Add(i + 1);
                    triangles.Add(i);

                    triangles.Add(i);
                    triangles.Add(i + 1);
                    triangles.Add(i + 2);
                }
                Mesh mesh = new Mesh();
                mesh.vertices = vertices.ToArray();
                mesh.triangles = triangles.ToArray();
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();
                lines.AddComponent<MeshFilter>().mesh = mesh;
                lines.AddComponent<MeshRenderer>().material = mat;
                return lines;
            }
            /// <summary>
            /// 判断点是否在向量的左边
            /// </summary>
            /// <param name="startPoint"></param>
            /// <param name="endPoint"></param>
            /// <param name="point"></param>
            /// <returns></returns>
            private static bool IsLeft(Vector3 startPoint, Vector3 endPoint, Vector3 point)
            {
                return ((endPoint.x - startPoint.x) * (point.z - startPoint.z) - (endPoint.z - startPoint.z) * (point.x - startPoint.x)) > 0;
            }
        }
    }

}