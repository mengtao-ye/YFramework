using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 对象池对象基类
    /// </summary>
    public interface IGameObjectPoolTarget : IRecycle
    {
        bool isUI { get; }
        GameObject Target { get;  }
        string assetPath { get; }
        long ID { get; set; }
        bool GameObjectIsPop { get; }
        void Pop();
        void Push();
        void Init(GameObject target);
        IGameObjectPoolTarget Clone();
        void Update();
    }
}
