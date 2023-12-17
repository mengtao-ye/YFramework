using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 引用计数器
    /// </summary>
    public abstract class BaseCount
    {
        /// <summary>
        /// 计数个数
        /// </summary>
        private int mCount = 0;
        /// <summary>
        /// 计数个数
        /// </summary>
        public int count { get { return mCount; } set 
            {
                if (mCount == value) return;
                if (mCount < value)
                { //增加
                    if (mCount == 0)
                    {
                        Enter();
                    }
                }
                else {
                    if (mCount>0) 
                    {
                        if (value <= 0)
                        {
                            Exit();
                        }
                    }
                }
                mCount = Mathf.Clamp(value,0,int.MaxValue);
            }
        }
        /// <summary>
        /// 进入计数器
        /// </summary>
        protected abstract void Enter();
        /// <summary>
        /// 离开计数器
        /// </summary>
        protected abstract void Exit();
    }
}
