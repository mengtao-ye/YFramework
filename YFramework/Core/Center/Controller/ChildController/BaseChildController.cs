namespace YFramework
{
    public abstract class BaseChildController  : IChildController
    {
        public IController controller { get; }
        public BaseChildController(IController controller)
        {
            this.controller = controller;
        }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void OnDestory() { }
        public virtual void Update() { }
        public virtual void Clear() { }
        public virtual void FixedUpdate()  {}
        public virtual void LaterUpdate()  {  }
        public virtual void LaterStartPriorityOne()  {  }
        public virtual void LaterStartPriorityTwo()  {  }
        public virtual void LaterStartPriorityThree()  {  }
        public virtual void LaterStartPriorityFour()  {  }
        public virtual void LaterStartPriorityFive()  { }
    }
}
