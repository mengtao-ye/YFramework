using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class BaseController : IController
    {
        /// <summary>
        /// 子控制器数组
        /// </summary>
        private List<IChildController> mControllerList;
        /// <summary>
        /// 场景对象
        /// </summary>
        public IScene scene { get; private set; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="scene">控制器场景</param>
        public BaseController(IScene scene) 
        {
            this.scene = scene;
            mControllerList = new List<IChildController>();
            ConfigChildController();
        }
        /// <summary>
        /// 配置子控制器
        /// </summary>
        protected abstract void ConfigChildController();
        #region 操作

        /// <summary>
        /// 获取子控制器 
        /// </summary>
        /// <typeparam name="T">没有时为null</typeparam>
        /// <returns></returns>
        public T GetChildController<T>() where T : class, IChildController
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                if (mControllerList[i] is T)
                {
                    return mControllerList[i] as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 添加子控制器
        /// </summary>
        /// <param name="controller"></param>
        public void AddChildController(IChildController controller)
        {

            if (controller == null) return;
            if (mControllerList.Contains(controller)) return;
            mControllerList.Add(controller);
        }
        /// <summary>
        /// 移除子控制器
        /// </summary>
        /// <param name="controller"></param>
        public void RemoveChildController(IChildController controller)
        {
            if (controller == null) return;
            for (int i = 0; i < mControllerList.Count; i++)
            {
                if (mControllerList[i] == controller)
                {
                    mControllerList.RemoveAt(i);
                    break;
                }
            }
        } 
        #endregion
        #region 生命周期
        /// <summary>
        /// 初始化方法
        /// </summary>
        public  virtual void Awake()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Awake();
            }
        }
        /// <summary>
        /// 开始方法
        /// </summary>
        public  virtual void Start()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Start();
            }
        }
        /// <summary>
        /// 销毁方法
        /// </summary>
        public  virtual void OnDestory()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].OnDestory();
            }
        }
        /// <summary>
        /// 帧方法
        /// </summary>
        public  virtual void Update()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Update();
            }
        }

        /// <summary>
        /// 固定帧方法
        /// </summary>
        public  virtual void FixedUpdate()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].FixedUpdate();
            }
        }
        /// <summary>
        /// 延迟帧方法
        /// </summary>
        public  virtual void LaterUpdate()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].LaterUpdate();
            }
        } 
        #endregion
      
    }
}
