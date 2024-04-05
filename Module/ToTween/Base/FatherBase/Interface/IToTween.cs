using System;
using UnityEngine;
using YFramework;

namespace YFramework
{
    /// <summary>
    /// totween流程接口
    /// </summary>
    public interface IToTween : IPool,ILife
    {
        /// <summary>
        /// 是否处于运行状态
        /// </summary>
        bool isRun { get;  }
 
        /// <summary>
        /// 曲线类型
        /// </summary>
        CurveType curveType { get; }
        /// <summary>
        /// 完成时的回调
        /// </summary>
        Action complete { get; }
        /// <summary>
        /// 添加完成回调
        /// </summary>
        /// <param name="complete"></param>
        /// <returns></returns>
        IToTween AddCompleteCallBack(Action complete);
        /// <summary>
        /// 设置曲线
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        IToTween SetCurve(CurveType type);
        /// <summary>
        /// 连接下一个Tween
        /// </summary>
        /// <param name="tween"></param>
        /// <returns></returns>
        IToTween Concat(IToTween tween);
        /// <summary>
        /// 上一个Tween
        /// </summary>
        IToTween previewToTween { get; set; }
        /// <summary>
        /// 下一个Tween
        /// </summary>
        IToTween nextToTween { get; set; }
        /// <summary>
        /// 打断（不会执行完成回调）
        /// </summary>
        void Interrupt();
        /// <summary>
        /// 完成（会执行完成回调）
        /// </summary>
        void Complete();
    }
}
