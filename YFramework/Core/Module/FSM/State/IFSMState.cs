namespace YFramework
{
    public interface IFSMState<T>
    {
        IFSM<T> fsm { get; }
        /// <summary>
        /// 状态ID
        /// </summary>
        int stateID { get; }
        /// <summary>
        /// 初始化方法
        /// </summary>
        void Init();
        /// <summary>
        /// 进入状态时
        /// </summary>
        void Enter();
        /// <summary>
        /// 离开状态时
        /// </summary>
        void Exit();
        /// <summary>
        /// 停留的当前状态时 帧方法
        /// </summary>
        void Stay();
        /// <summary>
        /// 检查当前状态
        /// </summary>
        void Check();
        /// <summary>
        /// 设置状态机
        /// </summary>
        /// <param name="fsm"></param>
        void SetFSM(IFSM<T> fsm);
    }
}
