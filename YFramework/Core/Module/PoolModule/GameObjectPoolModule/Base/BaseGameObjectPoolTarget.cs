using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 对象池基类
    /// </summary>
    public abstract class BaseGameObjectPoolTarget<T> : IGameObjectPoolTarget where T : IGameObjectPoolTarget,new()
    {
        /// <summary>
        /// 游戏对象
        /// </summary>
        public  GameObject Target { get; private set; }
        /// <summary>
        /// 类型ID
        /// </summary>
        public abstract int Type { get; }
        /// <summary>
        /// 元对象，非实例化对象
        /// </summary>
        public abstract GameObject Original { get; }
        /// <summary>
        /// 是否已经出栈了
        /// </summary>
        public bool IsPop { get; private set; }
        /// <summary>
        /// 实体对象ID
        /// </summary>
        public int ID { get; private set; }

        public IGameObjectPoolTarget Clone()
        {
            return new T();
        }

        public virtual void Init(GameObject target)
        {
            Target = target;
        }

        public virtual void Pop()
        {
            if (Target != null)
            {
                Target.SetActive(true);
            }
            IsPop = true;
        }

        public virtual void Push()
        {
            if (Target != null)
            {
                Target.SetActive(false);
            }
            IsPop = false;
        }
        /// <summary>
        /// 更新函数
        /// </summary>
        public virtual void Update() { }
        /// <summary>
        /// 设置实体对象ID
        /// </summary>
        /// <param name="id"></param>
        public void SetID(int id)
        {
            ID = id;
        }
    }
}
