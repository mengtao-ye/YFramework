using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 流程控制器
    /// </summary>
    public class ProcessController
    {
        private IList<IProcessManager> mProcessList;
        public ProcessController()
        {
            mProcessList = new List<IProcessManager>();
        }
        /// <summary>
        /// 启动流程模块
        /// </summary>
        /// <param name="processManager"></param>
        public void Add(IProcessManager processManager)
        { 
            if (processManager == null) return;
            if (!mProcessList.Contains(processManager))
            {
                processManager.SetProcessManager(this);
                mProcessList.Add(processManager);
            }
        }
        /// <summary>
        /// 创建processManager
        /// </summary>
        /// <returns></returns>
        public IProcessManager Create() 
        {
            ProcessManager processManager = new ProcessManager();
            Add(processManager);
            return processManager;
        }
        /// <summary>
        /// 创建processManager
        /// </summary>
        /// <returns></returns>
        public IProcessManager Create<T>() where T : class, new()
        {
            ProcessManager<T> processManager = new ProcessManager<T>();
            processManager.condition = new T();
            Add(processManager);
            return processManager;
        }

        /// <summary>
        ///刷新
        /// </summary>
        public void Update()
        {
            for (int i = 0; i < mProcessList.Count; i++)
            {
                mProcessList[i].Update();
            }
        }
        /// <summary>
        /// 移除流程管理模块
        /// </summary>
        /// <param name="process"></param>
        public void Remove(IProcessManager process)
        {
            if (process == null) return;
            if (mProcessList.Contains(process))
                mProcessList.Remove(process);
        }
    }
}
