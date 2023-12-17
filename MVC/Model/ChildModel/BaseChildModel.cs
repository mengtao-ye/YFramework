using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 子模型抽象类
    /// </summary>
    public abstract class BaseChildModel : IChildModel
    {
        /// <summary>
        /// 模型管理器
        /// </summary>
        protected IModel mModel;
        /// <summary>
        /// 获取模型管理器对象
        /// </summary>
        public IModel model { get { return mModel; } }
        /// <summary>
        /// 模型控制器游戏对象
        /// </summary>
        protected GameObject mGameObject;
        /// <summary>
        /// 模型控制器游戏对象
        /// </summary>
        public GameObject gameObject { get { return mGameObject; } }
        /// <summary>
        /// transform对象
        /// </summary>
        public Transform transform { get { return mGameObject.transform; } }
        public BaseChildModel(IModel model, GameObject target)
        {
            mModel = model;
            mGameObject = target;
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
