using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// 模型控制器接口
    /// </summary>
    public interface IModel : ILife
    {
        IScene scene { get; }
        /// <summary>
        /// transform对象
        /// </summary>
        Transform transform { get; }
        /// <summary>
        /// 游戏对象
        /// </summary>
        GameObject gameObject { get; }
        /// <summary>
        /// 获取子模型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetChildModel<T>() where T : class, IChildModel;
        /// <summary>
        /// 添加子模型
        /// </summary>
        /// <param name="model"></param>
        void AddChildModel(IChildModel model);
        /// <summary>
        /// 移除子模型
        /// </summary>
        /// <param name="model"></param>
        void RemoveChildModel(IChildModel model);
    }
}
