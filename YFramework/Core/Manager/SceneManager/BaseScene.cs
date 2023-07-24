using UnityEngine;
namespace YFramework
{
    public abstract class BaseScene :IScene
    {
        protected abstract string mSceneName { get; }
        public string sceneName { get { return mSceneName; } }
        private ISceneManager mSceneManager;
        public ISceneManager sceneManager { get { return mSceneManager; } private set { } }
        public virtual ICanvas canvas { get; protected set; }
        public virtual IModel model { get; protected set; }
        public virtual IController controller { get; protected set; }
        public void SetSceneManager(ISceneManager manager)
        {
            mSceneManager = manager;
        }
        public virtual void Awake()
        {
            if (canvas != null) canvas.Awake();
            if (model != null) model.Awake();
            if (controller != null) controller.Awake();
            GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            if (mainCamera == null) {
                
            }
        }
        public virtual void Start()
        {
            if (canvas != null) canvas.Start();
            if (model != null) model.Start();
            if (controller != null) controller.Start();
        }
        public virtual void Update()
        {
            if (canvas != null) canvas.Update();
            if (model != null) model.Update();
            if (controller != null) controller.Update();

        }
        public virtual void FixedUpdate() {

            if (canvas != null) canvas.FixedUpdate();
            if (model != null) model.FixedUpdate();
            if (controller != null) controller.FixedUpdate();
        }
        public virtual void LaterUpdate() {
            if (canvas != null) canvas.LaterUpdate();
            if (model != null) model.LaterUpdate();
            if (controller != null) controller.LaterUpdate();
        }
        public virtual void OnDestory() {
            if (canvas != null) canvas.OnDestory();
            if (model != null) model.OnDestory();
            if (controller != null) controller.OnDestory();
        }
        public virtual void Clear()
        {
            if (canvas != null) canvas.Clear();
            if (model != null) model.Clear();
            if (controller != null) controller.Clear();
        }
    }
}
