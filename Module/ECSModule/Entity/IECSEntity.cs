using UnityEngine;

namespace YFramework
{
    public interface IECSEntity:ILife
    {
        /// <summary>
        /// 系统管理类
        /// </summary>
        IECSSystem system { get; }
        /// <summary>
        /// 唯一标识符
        /// </summary>
        int ID { get; }
        /// <summary>
        /// 目标对象
        /// </summary>
        GameObject gameObject { get; }
        /// <summary>
        /// 设置实体数据
        /// </summary>
        /// <param name="data"></param>
        void SetData( byte[] data);
        /// <summary>
        /// 设置系统管理器
        /// </summary>
        /// <param name="system"></param>
        void SetSystem(IECSSystem system);
        /// <summary>
        /// 查找组件
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IECSComponent FindComponent(int type);
    }
}
