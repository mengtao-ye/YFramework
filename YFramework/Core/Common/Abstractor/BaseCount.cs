using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 引用计数器
    /// </summary>
    public abstract class BaseCount
    {
        private int mCount = 0;
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
        protected abstract void Enter();
        protected abstract void Exit();
    }
}
