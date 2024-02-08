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
        /// 流程管理器
        /// </summary>
        public IProcessManager processManager { get; set; }
        /// <summary>
        /// 链接流程对象
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public IProcess Concat(IProcess process)
        {
            Next = process;
            process.processManager = processManager;
            process.Init();
            return Next;
        }
        /// <summary>
        /// 执行下一个流程
        /// </summary>
        public void DoNext()
        {
            processManager.DoNext();
        }
        /// <summary>
        /// 帧函数
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// 进入流程方法
        /// </summary>
        public virtual void Enter() { }
        /// <summary>
        /// 退出流程方法
        /// </summary>
        public virtual void Exit() { }
        /// <summary>
        /// 初始化方法
        /// </summary>
        public virtual void Init() { }

    }
}
