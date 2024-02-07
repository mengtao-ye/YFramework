namespace YFramework
{
    public abstract class BaseProcessManager : IProcessManager
    {
        /// <summary>
        /// 当前的流程
        /// </summary>
        public IProcess curProcess { get; private set; }
        /// <summary>
        /// 流程控制器
        /// </summary>
        private ProcessController mProcessController;
        /// <summary>
        /// 启动
        /// </summary>
        public void Launcher()
        {
            curProcess?.Enter();
        }
        /// <summary>
        /// 设置流程控制器
        /// </summary>
        /// <param name="processController"></param>
        public void SetProcessManager(ProcessController processController)
        {
            mProcessController = processController;
        }
        /// <summary>
        /// 链接流程
        /// </summary>
        /// <param name="process"></param>
        public IProcess Concat(IProcess process)
        {
            curProcess = process;
            process.processManager = this;
            process.Init();
            return curProcess;
        }
        /// <summary>
        ///执行下一个流程
        /// </summary>
        public void DoNext()
        {
            if (curProcess == null)
            {
                mProcessController.Remove(this);
                return;
            }
            curProcess.Exit();
            if (curProcess.Next != null)
            {
                curProcess = curProcess.Next;
                curProcess.Enter();
            }
            else
            {
                mProcessController.Remove(this);
                return;
            }
        }
        /// <summary>
        /// 更新函数
        /// </summary>
        public void Update()
        {
            if (curProcess != null)
            {
                curProcess.Update();
            }
        }
    }
}
