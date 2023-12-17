namespace YFramework
{
    public interface IECSComponent:ILife
    {
        /// <summary>
        /// 组件ID
        /// </summary>
        int ComponentID { get; }
        /// <summary>
        /// 设置数据
        /// </summary>
        /// <param name="data"></param>
        void SetData(byte[] data);
        /// <summary>
        /// 实体对象
        /// </summary>
        IECSEntity entity { get; }
        /// <summary>
        /// 设置实体
        /// </summary>
        /// <param name="entity"></param>
        void SetEntity(IECSEntity entity);
    }
}
