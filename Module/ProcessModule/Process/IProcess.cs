namespace YFramework
{
    /// <summary>
    /// 流程接口
    /// </summary>
    public interface IProcess
    {
        IProcessManager processManager { get; set; }
        /// <summary>
        /// 下一个流程
        /// </summary>
        IProcess Next { get; }
        /// <summary>
        /// 链接流程对象
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        IProcess Concat(IProcess process);
        /// <summary>
        /// 帧函数
        /// </summary>
        void Update();
        /// <summary>
        /// 执行下一个流程
        /// </summary>
        void DoNext();
        /// <summary>
        /// 进入流程方法
        /// </summary>
        void Enter();
        /// <summary>
        /// 退出流程方法
        /// </summary>
        void Exit();
        /// <summary>
        /// 初始化方法
        /// </summary>
        void Init();
    }
}
