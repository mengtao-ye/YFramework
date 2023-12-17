namespace YFramework
{
    public abstract class BaseFSMState<T>: IFSMState<T>
    {
        /// <summary>
        /// 状态ID
        /// </summary>
        public int stateID { get; private set; }
        /// <summary>
        /// 状态机对象
        /// </summary>
        public IFSM<T> fsm { get; private set; }
        public BaseFSMState(int stateID)
        {
            this.stateID = stateID;
        }
        /// <summary>
        /// 设置状态机
        /// </summary>
        /// <param name="fsm"></param>
        public void SetFSM(IFSM<T> fsm)
        {
            this.fsm = fsm;
        }

        /// <summary>
        /// 进入当前状态时
        /// </summary>
        public abstract void Enter();
        /// <summary>
        /// 离开当前状态时
        /// </summary>
        public abstract void Exit();
        /// <summary>
        /// 停留在当前状态时
        /// </summary>
        public abstract void Stay();
        /// <summary>
        /// 检查当前状态
        /// </summary>
        public abstract void Check();
        /// <summary>
        /// 初始化方法
        /// </summary>
        public abstract void Init();
    }
}
