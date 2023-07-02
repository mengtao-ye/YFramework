using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace YFramework
{
    public static class UIExtend
    {
        /// <summary>
        /// 添加UI响应事件
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="type"></param>
        /// <param name="callBack"></param>
        public static void AddEventTrigger(this RectTransform rectTransform, EventTriggerType type, Action<BaseEventData> callBack)
        {
            EventTrigger eventTrigger = rectTransform.GetComponent<EventTrigger>();
            if (eventTrigger == null) eventTrigger = rectTransform.gameObject.AddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback = new EventTrigger.TriggerEvent();
            entry.callback.AddListener((eventData) => { callBack(eventData); });
            eventTrigger.triggers.Add(entry);
        }
    } 
}
