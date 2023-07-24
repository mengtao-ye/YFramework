using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 流程控制器
    /// </summary>
    public class ProcessController
    {
        private IList<ProcessManager> mProcessList;
        public ProcessController()
        {
            mProcessList = new List<ProcessManager>();
        }
        /// <summary>
        /// 启动流程模块
        /// </summary>
        /// <param name="processManager"></param>
        public void Add(ProcessManager processManager)
        { 
            if (processManager == null) return;
            if (processManager.curProcess == null) return;
            if (!mProcessList.Contains(processManager))
            {
                processManager.SetProcessManager(this);
                mProcessList.Add(processManager);
                processManager.curProcess.Enter();
            }
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
        public void Remove(ProcessManager process)
        {
            if (process == null) return;
            if (mProcessList.Contains(process))
                mProcessList.Remove(process);
        }
    }
}
