namespace YFramework
{
    /// <summary>
    /// 模块基类
    /// </summary>
    public abstract class BaseModule :IModule
    {
        /// <summary>
        /// 是否运行了
        /// </summary>
        public bool isRun { get; set; }
        /// <summary>
        /// 中心模块
        /// </summary>
        public Center center { get; protected set; }
        public BaseModule(Center center)
        {
            this.center = center;
        }
        #region 生命周期
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnDestory() { }
        public virtual void Clear() { }
        public virtual void FixedUpdate() { }
        public virtual void LaterUpdate() { } 
        #endregion
    }
}
