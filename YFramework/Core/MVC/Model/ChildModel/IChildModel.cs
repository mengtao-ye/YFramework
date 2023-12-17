using UnityEngine;

namespace YFramework
{
    public interface IChildModel : ILife
    {
        /// <summary>
        /// 模型控制器
        /// </summary>
        IModel model { get; }
        /// <summary>
        /// 模型控制器游戏对象
        /// </summary>
         GameObject gameObject { get; }
        /// <summary>
        /// transform对象
        /// </summary>
         Transform transform { get; }
    }
}
