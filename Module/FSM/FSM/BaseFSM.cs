using System.Collections.Generic;
using YFramework;

namespace YFramework
{
    /// <summary>
    /// 状态机管理类
    /// </summary>
    public abstract class BaseFSM<T> : IFSM<T>
    {
        private Dictionary<int, IFSMState<T>>  mStateDict;
        public IFSMState<T> curState { get; private set; }//当前状态
        public IFSMState<T> previewState { get; private set; }//上一个状态状态
        public T condition { get;  set; }
        public BaseFSM()
        {
            mStateDict = new Dictionary<int, IFSMState<T>>();
        }
        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public void AddState(IFSMState<T> state)
        {
            if (state == null) return;
            if (mStateDict.ContainsKey(state.stateID)) {
                LogHelper.LogError($"已经包含了{state.stateID}");
                return;
            }
            state.SetFSM(this);
            mStateDict.Add(state.stateID, state);
            state.Init();
        }
        /// <summary>
        /// 执行状态
        /// </summary>
        public void Performance(int stateID)
        {
            if (!mStateDict.ContainsKey(stateID)) {
                LogHelper.LogError($"状态{stateID}不存在");
                return;
            }
            if (curState == null)
            {
                curState = mStateDict[stateID];
                curState.Enter();
                return;
            }
            curState.Exit();
            previewState = curState;
            curState = mStateDict[stateID];
            curState.Enter();
        }
        /// <summary>
        /// 帧函数
        /// </summary>
        public void Update()
        {
            if (curState != null)
            {
                curState.Check();
                curState.Stay();
            }
        }
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="condition"></param>
        public void SetCondition(T condition)
        {
            this.condition = condition;
        }
        /// <summary>
        /// 获取状态
        /// </summary>
        /// <param name="stateID"></param>
        /// <returns></returns>
        public IFSMState<T> GetState(int stateID)
        {
            if (mStateDict.ContainsKey(stateID)) return mStateDict[stateID];
            return null;
        }
    }
}
