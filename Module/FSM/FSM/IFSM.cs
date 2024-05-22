namespace YFramework
{
    public interface IFSM<T>
    {
        //当前状态
        IFSMState<T> curState { get; }
        /// <summary>
        /// 条件
        /// </summary>
        T condition { get; set; }
        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="condition"></param>
        void SetCondition(T condition);
        void Performance(int stateID);
        IFSMState<T> GetState(int stateID);
        void Update();
        void AddState(IFSMState<T> state);
    }
}
