namespace YFramework
{
    /// <summary>
    /// 模块接口
    /// </summary>
    public interface IModule : ILife
    {
        /// <summary>
        /// 是否运行
        /// </summary>
        bool isRun { get; set; }
        /// <summary>
        // 中心模块
        /// </summary>
        Center center { get; }
    }
}
