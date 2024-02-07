namespace YFramework
{
    public class ProcessManager<T> : BaseProcessManager
    {
        /// <summary>
        /// 先决条件
        /// </summary>
        public T condition { get; set; }
    }
}
