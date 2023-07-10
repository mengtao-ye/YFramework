using System.Collections.Generic;

namespace YFramework
{
    public abstract class BaseECSSystem<T> : IECSSystem where T : IECSEntity
    {
        protected List<T> mEntity;
        public List<T> entitys { get { return mEntity; } }
        public BaseECSSystem()
        {
            mEntity =new List<T>();
        }
        public virtual void Awake(){}
        public virtual void Start(){}
        public virtual void Update()
        {
            if (mEntity.Count == 0) return;
            for (int i = 0; i < mEntity.Count; i++)
            {
                mEntity[i].Update();
            }
        }
        public virtual void Clear() {
            if (mEntity.Count == 0) return;
            for (int i = 0; i < mEntity.Count; i++)
            {
                mEntity[i].Clear();
            }
        }
        public virtual void FixedUpdate() {
            if (mEntity.Count == 0) return;
            for (int i = 0; i < mEntity.Count; i++)
            {
                mEntity[i].FixedUpdate();
            }
        }
        public virtual void LaterUpdate() {
            if (mEntity.Count == 0) return;
            for (int i = 0; i < mEntity.Count; i++)
            {
                mEntity[i].LaterUpdate();
            }
        }
        public virtual void OnDestory()
        {
            if (mEntity.Count == 0) return;
            for (int i = 0; i < mEntity.Count; i++)
            {
                mEntity[i].OnDestory();
            }
        }
        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="entity"></param>
        public void RemoveEntity(T entity)
        {
            if (mEntity.Contains(entity))
                mEntity.Remove(entity);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity"></param>
        public void AddEntity(T entity)
        {
            if (entity == null)
            {
                return;
            }
            if (mEntity.Contains(entity)) return;
            mEntity.Add( entity);
            entity.SetSystem(this);
            entity.Awake();
            entity.Start();
        }
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindEntity(int id)
        {
            for (int i = 0; i < mEntity.Count; i++)
            {
                if (mEntity[i].ID == id) return mEntity[i];
            }
            return default(T) ;
        }
       
    }
}
