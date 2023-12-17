namespace YFramework
{
    /// <summary>
    /// 控制器接口
    /// </summary>
    public interface IController : ILife
    {
        IScene scene { get; }
        /// <summary>
        /// 获取子控制器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetChildController<T>() where T : class, IChildController;
        /// <summary>
        /// 添加子控制器
        /// </summary>
        /// <param name="controller"></param>
        void AddChildController(IChildController controller);
        /// <summary>
        /// 移除子控制器
        /// </summary>
        /// <param name="controller"></param>
        void RemoveChildController(IChildController controller);
    }
}
