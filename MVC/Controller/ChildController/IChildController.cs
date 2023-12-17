namespace YFramework
{
    /// <summary>
    /// 子控制器接口
    /// </summary>
    public interface IChildController : ILife
    {
        /// <summary>
        /// 控制器对象
        /// </summary>
        IController controller { get; }
    }
}
