using System.Collections.Generic;

namespace YFramework
{
    public abstract class BaseController : BaseSceneModule , IController
    {
        private List<IChildController> mControllerList;
        public BaseController(IScene scene) : base(scene)
        {
            mControllerList = new List<IChildController>();
            ConfigChildController();
        }
        public T GetChildController<T>() where T :class, IChildController
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                if (mControllerList[i] is T) {
                    return mControllerList[i] as T;
                }
            }
            return null;
        }
        protected abstract void ConfigChildController();
        protected void AddChildController(IChildController controller) {

            if (controller == null) return;
            if (mControllerList.Contains(controller)) return;
            mControllerList.Add(controller);
        }
        public new virtual void Awake(){
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Awake();
            }
        }
        public new virtual void Start() {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Start();
            }
        }
        public new virtual void OnDestory(){
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].OnDestory();
            }
        }
        public new virtual void Update(){
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Update();
            }
        }


        public new virtual void FixedUpdate()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].FixedUpdate();
            }
        }
        public new virtual void Clear()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].Clear();
            }
        }

        public new virtual void LaterUpdate()
        {
            for (int i = 0; i < mControllerList.Count; i++)
            {
                mControllerList[i].LaterUpdate();
            }
        }
    }
}
