namespace YFramework
{
    /// <summary>
    /// 场景模块
    /// </summary>
    public abstract class BaseSceneModule :ISceneModule
    {
        protected IScene mScene;
        public BaseSceneModule(IScene scene)
        {
            mScene = scene;
        }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Clear() { }
        public virtual void FixedUpdate() { }
        public virtual void LaterUpdate() { }
        public virtual void OnDestory() { }
        public virtual void Update() { }
        public virtual void LaterStartPriorityOne() { }
        public virtual void LaterStartPriorityTwo() { }
        public virtual void LaterStartPriorityThree() { }
        public virtual void LaterStartPriorityFour() { }
        public virtual void LaterStartPriorityFive() { }
    }
}
