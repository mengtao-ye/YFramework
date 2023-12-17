using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 子模型基类
    /// </summary>
    public abstract class BaseModel :IModel
    {
        /// <summary>
        /// 子模型游戏对象
        /// </summary>
        protected GameObject mGameObject;
        /// <summary>
        /// 子模型游戏对象
        /// </summary>
        public GameObject gameObject { get { return mGameObject; } }
        /// <summary>
        /// 子模型游Transform对象
        /// </summary>
        public Transform transform{ get { return mGameObject.transform; } }
        /// <summary>
        /// 场景对象
        /// </summary>
        public IScene scene { get; private set; }
        /// <summary>
        /// 子模型数组
        /// </summary>
        private List<IChildModel> mChildModel;
        public BaseModel(IScene scene, GameObject gameObject) 
        {
            this.scene = scene;
            mChildModel = new List<IChildModel>();
            mGameObject = gameObject;
            ConfigChildModel();
        }
        #region 操作
        /// <summary>
        /// 配置子模型控制器
        /// </summary>
        protected abstract void ConfigChildModel();
        /// <summary>
        /// 获取子模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetChildModel<T>() where T : class, IChildModel
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                if (mChildModel[i] is T) return mChildModel[i] as T;
            }
            return null;
        }
        /// <summary>
        /// 添加子模型
        /// </summary>
        /// <param name="model"></param>
        public void AddChildModel(IChildModel model)
        {
            if (model == null) return;
            if (mChildModel.Contains(model)) return;
            mChildModel.Add(model);
        } 
        /// <summary>
        /// 移除子模型
        /// </summary>
        /// <param name="model"></param>
        public void RemoveChildModel(IChildModel model)
        {
            if (model == null) return;
            for (int i = 0; i < mChildModel.Count; i++)
            {
                if (mChildModel[i] == model)
                {
                    mChildModel.RemoveAt(i);
                    break;
                }
            }
        }
        #endregion
        #region 生命周期
        public  virtual void Awake()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Awake();
            }
        }
        public  virtual void Start()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Start();
            }
        }
        public  virtual void Update()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Update();
            }
        }
        public virtual void OnDestory()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].OnDestory();
            }
        }

        public virtual void FixedUpdate()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].FixedUpdate();
            }
        }

        public  virtual void LaterUpdate()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].LaterUpdate();
            }
        } 
        #endregion
    }
}