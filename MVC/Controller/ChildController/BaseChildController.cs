namespace YFramework
{
    /// <summary>
    /// 子控制器
    /// </summary>
    public abstract class BaseChildController : IChildController
    {
        /// <summary>
        /// 控制器对象
        /// </summary>
        public IController controller { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="controller">控制器对象</param>
        public BaseChildController(IController controller)
        {
            this.controller = controller;
        }
        #region 生命周期
        /// <summary>
        /// 初始化方法
        /// </summary>
        public virtual void Awake() { }
        /// <summary>
        /// 开始方法
        /// </summary>
        public virtual void Start() { }
        /// <summary>
        /// 销毁方法
        /// </summary>
        public virtual void OnDestory() { }
        /// <summary>
        /// 帧方法
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// 清理方法
        /// </summary>
        public virtual void Clear() { }
        /// <summary>
        /// 固定帧方法
        /// </summary>
        public virtual void FixedUpdate() { }
        /// <summary>
        /// 延迟帧方法
        /// </summary>
        public virtual void LaterUpdate() { } 
        #endregion
    }
}
