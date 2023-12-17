namespace YFramework
{
    public abstract class BaseECSComponent : IECSComponent
    {
        /// <summary>
        /// 组件ID
        /// </summary>
        public abstract int ComponentID { get; }
        /// <summary>
        /// 实体对象
        /// </summary>
        public IECSEntity entity { get; private set; }
        public virtual void Awake() {}
        public virtual void Start(){}
        public virtual void FixedUpdate(){ }
        public virtual void Update(){}
        public virtual void LaterUpdate(){ }
        public virtual void Clear(){}
        public virtual void OnDestory() {}
       
        public abstract void SetData(byte[] data);
        public void SetEntity(IECSEntity entity)
        {
            this.entity = entity;
        }
    }
}
