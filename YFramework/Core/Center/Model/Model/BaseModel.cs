using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public abstract class BaseModel : BaseSceneModule ,IModel
    {
        protected GameObject mGameObject;
        public GameObject gameObject { get { return mGameObject; } }
        public Transform transform{ get { return mGameObject.transform; } }
        private List<IChildModel> mChildModel;
        public BaseModel(IScene scene, GameObject gameObject) : base(scene)
        {
            mChildModel = new List<IChildModel>();
            mGameObject = gameObject;
            ConfigChildModel();
        }
        protected abstract void ConfigChildModel();
        public T GetChildModel<T>() where T : class,IChildModel
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                if (mChildModel[i] is T) return mChildModel[i] as T;
            }
            return null;
        }
        
        protected void AddChildModel(IChildModel model) {
            if (model == null) return;
            if (mChildModel.Contains(model)) return;
            mChildModel.Add(model) ;
        }
        public new virtual void Awake() {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Awake();
            }
        }
        public new virtual void Start() {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Start();
            }
        }
        public new virtual void Update() {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Update();
            }
        }
        public new virtual void OnDestory() {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].OnDestory();
            }
        }
        public new virtual void Clear() {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].Clear ();
            }
        }

        public new virtual void FixedUpdate()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].FixedUpdate();
            }
        }

        public new virtual void LaterUpdate()
        {
            for (int i = 0; i < mChildModel.Count; i++)
            {
                mChildModel[i].LaterUpdate();
            }
        }
    }
}