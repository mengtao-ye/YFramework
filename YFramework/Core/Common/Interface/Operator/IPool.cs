namespace YFramework
{
    public interface IPool : IRecycle
    {
        bool isPop { get; set; }
        /// <summary>
        /// 从对象池出来时
        /// </summary>
        void PopPool();
        /// <summary>
        ///从对象池进来
        /// </summary>
        void PushPool();
    }
}
