using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 对象池基类
    /// </summary>
    public abstract class BaseGameObjectPoolTarget<T> : IGameObjectPoolTarget where T : IGameObjectPoolTarget, new()
    {
        /// <summary>
        /// 游戏对象
        /// </summary>
        public GameObject Target { get; private set; }

        public Transform transform { get { return Target.transform; } }
        /// <summary>

        /// 是否已经出栈了
        /// </summary>
        public bool GameObjectIsPop { get; private set; }
        /// <summary>
        /// 实体对象ID
        /// </summary>
        public long ID { get;  set; }

        /// <summary>
        /// 资源地址
        /// </summary>
        public abstract string assetPath { get; }

        /// <summary>
        /// 是否是UI对象
        /// </summary>
        public abstract bool isUI { get; }

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
            GameObjectIsPop = true;
        }

        public virtual void Push()
        {
            if (Target != null)
            {
                Target.SetActive(false);
            }
            GameObjectIsPop = false;
        }
        /// <summary>
        /// 回收
        /// </summary>
        public abstract void Recycle();

        /// <summary>
        /// 更新函数
        /// </summary>
        public virtual void Update() { }
    }
}
