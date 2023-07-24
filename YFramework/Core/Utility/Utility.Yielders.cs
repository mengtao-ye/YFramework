using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// 协程内置内容
        /// </summary>
        public static class Yielders
        {
            private static Dictionary<float, WaitForSeconds> mTimeDict = new Dictionary<float, WaitForSeconds>(MAX_SIZE);
            private static Dictionary<float, WaitForSecondsRealtime> mRealTimeDict = new Dictionary<float, WaitForSecondsRealtime>(MAX_SIZE);
            private const int MAX_SIZE = 50;
            /// <summary>
            /// 这里创建一个等待最后一帧对象，减少GC
            /// </summary>
            public static WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();
            /// <summary>
            /// 这里创建一个等待FixedUpdate的等待帧对象，减少GC
            /// </summary>
            public static WaitForFixedUpdate WaitForFixedUpdate = new WaitForFixedUpdate();
            /// <summary>
            /// 获取等待时间对象（范围在0-100）
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static WaitForSeconds GetSeconds(float time)
            {
                if (mTimeDict.Count > MAX_SIZE)//做一个小优化，如果超过了这个界限就清除一下数据，防止数据一直叠加
                {
                    mTimeDict.Clear();
                }
                time = Mathf.Clamp(time, 0, 100);//限定时间
                if (!mTimeDict.ContainsKey(time))
                {
                    mTimeDict.Add(time, new WaitForSeconds(time));
                }
                return mTimeDict[time];
            }

            /// <summary>
            /// 获取真实的等待时间对象（范围在0-100）
            /// </summary>
            /// <param name="time"></param>
            /// <returns></returns>
            public static WaitForSecondsRealtime GetRealSeconds(float time)
            {
                if (mRealTimeDict.Count > MAX_SIZE)//做一个小优化，如果超过了这个界限就清除一下数据
                {
                    mRealTimeDict.Clear();
                }
                time = Mathf.Clamp(time, 0, 100);//限定时间
                if (!mRealTimeDict.ContainsKey(time))
                {
                    mRealTimeDict.Add(time, new WaitForSecondsRealtime(time));
                }
                return mRealTimeDict[time];
            }
            /// <summary>
            /// 清除缓存数据
            /// </summary>
            public static void Clear()
            {
                mRealTimeDict.Clear();
                mTimeDict.Clear();
            }
        }
    }
}
