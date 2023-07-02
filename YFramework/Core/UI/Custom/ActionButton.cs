using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YFramework
{
    /// <summary>
    /// 响应事件按钮
    /// </summary>
    public class ActionButton : Button
    {
        private Action OnPointerDownEvent;
        private Action OnPointerDownUp;
        private Action OnPointerDownExit;
        /// <summary>
        /// 添加按下事件监听
        /// </summary>
        /// <param name="pointDown"></param>
        public void SetPointDownEvent(Action pointDown) {
            OnPointerDownEvent = pointDown;
        }
        /// <summary>
        /// 添加抬起事件监听
        /// </summary>
        /// <param name="pointUp"></param>
        public void SetOnPointerDownUp(Action pointUp)
        {
            OnPointerDownUp = pointUp;
        }
        /// <summary>
        /// 添加移出事件监听
        /// </summary>
        /// <param name="pointExit"></param>
        public void SetOnPointerDownExit(Action pointExit)
        {
            OnPointerDownExit = pointExit;
        }
        public  override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            OnPointerDownEvent?.Invoke();
        }
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            OnPointerDownUp?.Invoke();
        }
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            OnPointerDownExit?.Invoke();
        }
    } 
}