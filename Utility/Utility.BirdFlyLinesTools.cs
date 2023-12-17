using UnityEngine;

namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// 小鸟飞行路线
        /// </summary>
        public class BirdFlyLinesTools
        {
            private GameObject mBird;
            private Vector3 mMinPos;//飞行的最小点
            private Vector3 mMaxPos;//飞行的最大点
            private Vector3[] mFlyPoints;//飞行点
            private Vector3[] mFlyLines;//飞行路线
            public Vector3 GetRandomPos { get { return RandomTools.GetRandomVector3((int)mMinPos.x, (int)mMaxPos.x, (int)mMinPos.y, (int)mMaxPos.y, (int)mMinPos.z, (int)mMaxPos.z); } }
            private int mCurIndex;
            private int mSplitCount;
            private float mMaxMoveDelta = 0.01f;
            public BirdFlyLinesTools(GameObject bird, Vector3 minPos, Vector3 maxPos)
            {
                if (bird == null)
                {
                    Debug.LogError("Bird target is null");
                    return;
                }
                mBird = bird;
                mMinPos = minPos;
                mMaxPos = maxPos;
                mFlyPoints = new Vector3[3];
                mCurIndex = 0;
                mSplitCount = 30;
                SpawnLines(bird.transform.position, GetRandomPos, GetRandomPos);
            }

            public void Update()
            {
                if (Vector3.Distance(mBird.transform.position, mFlyLines[mCurIndex]) < 0.1f)
                {
                    mCurIndex++;
                    if (mCurIndex >= mSplitCount)
                    {
                        SpawnLines(mBird.transform.position, GetRandomPos, GetRandomPos);
                        mCurIndex = 0;
                    }
                    mBird.transform.LookAt(mFlyLines[mCurIndex]);
                }
                mBird.transform.position = Vector3.MoveTowards(mBird.transform.position, mFlyLines[mCurIndex], mMaxMoveDelta);
            }
            private void SpawnLines(Vector3 pos1, Vector3 pos2, Vector3 pos3)
            {
                mFlyPoints[0] = pos1;
                mFlyPoints[1] = pos2;
                mFlyPoints[2] = pos3;
                mFlyLines = BesierLineTools.GetBeizerList_2(mFlyPoints[0], mFlyPoints[1], mFlyPoints[2], mSplitCount);
            }
        }
    }

}