namespace YFramework
{
    /// <summary>
    /// 流程功能基类
    /// </summary>
    public abstract class BaseProcess : IProcess
    {
        /// <summary>
        /// 下一个流程
        /// </summary>
        public IProcess Next { get; private set; }
        /// <summary>
        /// 流程管理类对象
        /// </summary>
        private ProcessManager mProcessManager;
        /// <summary>
        /// 初始化对象，传入流程管理类
        /// </summary>
        /// <param name="processManaegr"></param>
        public BaseProcess(ProcessManager processManaegr )
        {
            mProcessManager = processManaegr;
        }
        /// <summary>
        /// 链接流程对象
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public IProcess Concat(IProcess process)
        {
            Next = process;
            return Next;
        }
        /// <summary>
        /// 执行下一个流程
        /// </summary>
        public void DoNext()
        {
            mProcessManager.DoNext();
        }
        /// <summary>
        /// 帧函数
        /// </summary>
        public abstract void Update();
        /// <summary>
        /// 进入流程方法
        /// </summary>
        public abstract void Enter();
        /// <summary>
        /// 退出流程方法
        /// </summary>
        public abstract void Exit();
    }
}
