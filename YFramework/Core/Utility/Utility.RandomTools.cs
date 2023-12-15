using UnityEngine;

namespace YFramework
{
    public partial class Utility {
        /// <summary>
        /// 随机数工具
        /// </summary>
        public static class RandomTools
        {
            private static System.Random mRandom = new System.Random();
            /// <summary>
            /// 随机数，是否为真
            /// </summary>
            /// <returns></returns>
            public static bool IsTrue()
            {
                return mRandom.Next(0,2) == 0;
            }
            /// <summary>
            /// 获取随机生成的二维数组
            /// </summary>
            /// <param name="xMin"></param>
            /// <param name="xMax"></param>
            /// <param name="yMin"></param>
            /// <param name="yMax"></param>
            /// <returns></returns>
            public static Vector2 GetRandomVector2(int xMin, int xMax, int yMin, int yMax)
            {
                if (xMin > xMax || yMin > yMax) return Vector4.zero;
                return new Vector2(mRandom.Next(xMin, xMax), mRandom.Next(yMin, yMax));
            }
            /// <summary>
            /// 获取随机生成的三维数组
            /// </summary>
            /// <param name="xMin"></param>
            /// <param name="xMax"></param>
            /// <param name="yMin"></param>
            /// <param name="yMax"></param>
            /// <returns></returns>
            public static Vector3 GetRandomVector3(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax)
            {
                if (xMin > xMax || yMin > yMax || zMin > zMax) return Vector4.zero;
                return new Vector3(mRandom.Next(xMin, xMax), mRandom.Next(yMin, yMax), mRandom.Next(zMin, zMax));
            }
            /// <summary>
            /// 获取随机生成的四维数组
            /// </summary>
            /// <param name="xMin"></param>
            /// <param name="xMax"></param>
            /// <param name="yMin"></param>
            /// <param name="yMax"></param>
            /// <returns></returns>
            public static Vector4 GetRandomVector4(int xMin, int xMax, int yMin, int yMax, int zMin, int zMax, int wMin, int wMax)
            {
                if (xMin > xMax || yMin > yMax || zMin > zMax || wMin > wMax) return Vector4.zero;
                return new Vector4(mRandom.Next(xMin, xMax), mRandom.Next(yMin, yMax), mRandom.Next(zMin, zMax), mRandom.Next(wMin, wMax));
            }
        }
    }
}