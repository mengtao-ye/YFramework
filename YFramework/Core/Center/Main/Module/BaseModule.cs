namespace YFramework
{
    /// <summary>
    /// 模块基类
    /// </summary>
    public abstract class BaseModule :IModule
    {
        public bool isRun { get; set; }
        public Center center { get; protected set; }
        public BaseModule(Center center)
        {
            this.center = center;
        }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnDestory() { }
        public virtual void Clear() { }
        public virtual void FixedUpdate() { }
        public virtual void LaterUpdate() { }
        public virtual void LaterStartPriorityOne(){}
        public virtual void LaterStartPriorityTwo(){}
        public virtual void LaterStartPriorityThree(){}
        public virtual void LaterStartPriorityFour(){}
        public virtual void LaterStartPriorityFive(){ }
    }
}
