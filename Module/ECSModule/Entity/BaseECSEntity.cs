using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public abstract class BaseECSEntity: IECSEntity 
    {
        private List<IECSComponent> mComponentList;
        public long ID { get; private set; } = 0;
        public IECSSystem system { get; private set; }
        public GameObject gameObject { get; private set; }
        public BaseECSEntity(long id,GameObject target)
        {
            Init();
            SetID(id);
            this.gameObject = target;
        }

        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void FixedUpdate() { }
        public virtual void Update()
        {
            if (mComponentList.Count != 0)
            {
                for (int i = 0; i < mComponentList.Count; i++)
                {
                    mComponentList[i].Update();
                }
            }
        }
        public virtual void LaterUpdate() { }
        public virtual void OnDestory() {
            for (int i = 0; i < mComponentList.Count; i++)
            {
                mComponentList[i].OnDestory();
            }
        }
        public virtual void Clear() { }
        private void Init()
        {
            mComponentList = new List<IECSComponent>();
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(IECSComponent entity)
        {
            if (entity == null)
            {
                return;
            }
            if (mComponentList.Contains(entity)) 
            {
                mComponentList.Remove(entity);
            }
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        public void AddComponent(IECSComponent entity)
        {
            if (entity == null)
            {
                return;
            }
            mComponentList.Add(entity);
            entity.SetEntity(this);
            entity.Awake();
            entity.Start();
        }
        /// <summary>
        /// 设置标识符
        /// </summary>
        /// <param name="id"></param>
        public void SetID(long id)
        {
            ID = id;
        }
        /// <summary>   
        /// 设置实体数据
        /// </summary>
        /// <param name="data"></param>
        public abstract void SetData(byte[] data);
        public IECSComponent GetComponent(int type) 
        {
            for (int i = 0; i < mComponentList.Count; i++)
            {
                if (mComponentList[i].ComponentID == type)
                {
                    return mComponentList[i];
                }
            }
            return null;
        }

        public T GetComponent<T>() where T : class, IECSComponent
        {
            for (int i = 0; i < mComponentList.Count; i++)
            {
                if (mComponentList[i].GetType().Name == typeof(T).Name)
                {
                    return mComponentList[i] as T;
                }
            }
            return null;
        }

        /// <summary>
        /// 设置系统
        /// </summary>
        /// <param name="system"></param>
        public void SetSystem(IECSSystem system)
        {
            this.system = system;
        }

    }
}
