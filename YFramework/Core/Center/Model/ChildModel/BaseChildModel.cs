using UnityEngine;

namespace YFramework
{
    public abstract class BaseChildModel : IChildModel
    {
        protected IModel mModel;
        public IModel model { get { return mModel; } }
        protected GameObject mGameObject;
        public GameObject gameObject { get { return mGameObject; } }
        public Transform transform { get { return mGameObject.transform; } }
        public BaseChildModel(IModel model, GameObject target)
        {
            mModel = model;
            mGameObject = target;
        }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnDestory() { }
        public virtual void Clear() { }
        public virtual void FixedUpdate() { }
        public virtual void LaterUpdate() { }
    }
}
