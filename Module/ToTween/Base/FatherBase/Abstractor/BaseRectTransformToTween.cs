using System;
using UnityEngine;

namespace YFramework
{
    /// <summary>
    /// ToTween基类
    /// </summary>
    public abstract class BaseRectTransformToTween : IRectTransformToTween
    {
        /// <summary>
        /// 计时器
        /// </summary>
        protected float mTimer { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        protected float mTime { get;  set; }
        /// <summary>
        /// 完成时执行的回调
        /// </summary>
        public Action complete { get; private set; }
        /// <summary>
        /// 曲线类型
        /// </summary>
        public CurveType curveType { get; private set; } = CurveType.FastToLow;
        /// <summary>
        /// 当前transform
        /// </summary>
        public RectTransform rectTransform { get; private set; }
        /// <summary>
        /// 是否处于使用状态
        /// </summary>
        public bool isPop { get; set; }
        /// <summary>
        /// 下一个Tween
        /// </summary>
        public IToTween nextToTween { get; set; }
        /// <summary>
        /// 上一个Tween
        /// </summary>
        public IToTween previewToTween { get; set; } 
        /// <summary>
        /// 是否处于运行状态
        /// </summary>
        public bool isRun { get; private set; }
        /// <summary>
        /// 设置基础属性
        /// </summary>
        /// <param name="time"></param>
        /// <param name="transform"></param>
        protected virtual void SetBaseToTween(float time, RectTransform transform)
        {
            mTime = time;
            this.rectTransform = transform;
        }

        #region 生命周期
        public virtual void Awake()
        {
            if (mTime < 0)
            {
                Finish();
                return;
            }
            isRun = true;
        }
        public virtual void Clear() { }
        public virtual void FixedUpdate() { }
        public virtual void LaterUpdate() { }
        public virtual void OnDestory()
        {
            isRun = false;
        }
        public virtual void Start() { }
        public virtual void Update() { }
        public abstract void Recycle();

        #endregion
        /// <summary>
        /// 使用时执行的方法
        /// </summary>
        public virtual void PopPool()
        {
            mTimer = 0;
        }
        /// <summary>
        /// 用完后执行的方法
        /// </summary>
        public virtual void PushPool() {
            complete = null;
        }
        /// <summary>
        /// 完成执行的方法
        /// </summary>
        protected virtual void Finish()
        {
            complete?.Invoke();
            TotweenModule.Instance.RemoveToTween(this);
            if (nextToTween != null)
            {
                TotweenModule.Instance.AddToTween(nextToTween);
            }
        }
        /// <summary>
        /// 添加完成回调
        /// </summary>
        /// <param name="complete"></param>
        /// <returns></returns>
        public IToTween AddCompleteCallBack(Action complete)
        {
            this.complete = complete;
            return this;
        }
        /// <summary>
        /// 设置曲线
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public IToTween SetCurve(CurveType type)
        {
            curveType = type;
            return this;
        }
        /// <summary>
        /// 连接下一个Tween
        /// </summary>
        /// <param name="tween"></param>
        /// <returns></returns>
        public IToTween Concat(IToTween tween)
        {
            nextToTween = tween;
            tween.previewToTween = this;
            return nextToTween;
        }
        /// <summary>
        /// 打断（不会执行完成回调）
        /// </summary>
        public void Interrupt()
        {
            IToTween tween = GetCurToTween();
            if(tween != null) {
                TotweenModule.Instance.RemoveToTween(tween);
            }
        }
        /// <summary>
        /// 获取当前执行的Tween
        /// </summary>
        /// <returns></returns>
        private IToTween GetCurToTween()
        {
            IToTween temp = this;
            while (temp != null)
            {
                if (temp.isRun)
                {
                    return temp;
                }
                else
                {
                    temp = temp.previewToTween;
                }
            }
            return null;
        }
        /// <summary>
        /// 完成 （会执行回调）
        /// </summary>
        public void Complete()
        {
            IToTween tween = GetCurToTween();
            if (tween != null)
            {
                tween.complete?.Invoke();
                TotweenModule.Instance.RemoveToTween(tween);
            }
        }
    }
}
