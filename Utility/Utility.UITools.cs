using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// UI工具
        /// </summary>
        public partial  class UITools
        {
            /// <summary>
            /// 设置为全屏状态
            /// </summary>
            /// <param name="rect"></param>
            public static void SetFullScreen(RectTransform rect)
            {

                rect.anchorMin = Vector2.zero;
                rect.anchorMax = Vector2.one;
                rect.localPosition = Vector2.zero;
                rect.sizeDelta = Vector2.zero;
            }


            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="data"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public static Texture2D LoadImage(byte[] data, int width, int height)
            {
                Texture2D texture2D = new Texture2D(width, height);
                texture2D.LoadImage(data);
                return texture2D;
            }

            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="data"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public static Sprite LoadSprite(byte[] data, int width, int height)
            {
                Texture2D texture2D = LoadImage(data, width, height);
                return Sprite.Create(texture2D, new Rect(0, 0, width, height), Vector2.one * 0.5f);
            }


            /// <summary>
            /// 加载图片
            /// </summary>
            /// <param name="data"></param>
            /// <param name="width"></param>
            /// <param name="height"></param>
            /// <returns></returns>
            public static Sprite LoadSprite(Texture2D texture, int width, int height)
            {
                return Sprite.Create(texture, new Rect(0, 0, width, height), Vector2.one * 0.5f);
            }
            public static void AddEventTrigger(MaskableGraphic obj, EventTriggerType eventType, UnityAction<BaseEventData> callback)
            {
                EventTrigger.Entry entry = null;
                EventTrigger trigger = obj.GetComponent<EventTrigger>();

                if (trigger != null) // 已有EventTrigger
                {
                    // 查找是否已经存在要注册的事件
                    foreach (EventTrigger.Entry existingEntry in trigger.triggers)
                    {
                        if (existingEntry.eventID == eventType)
                        {
                            entry = existingEntry;
                            break;
                        }
                    }
                }
                else // 添加新的EventTrigger
                {
                    trigger = obj.gameObject.AddComponent<EventTrigger>();
                }

                // 若是这个事件不存在，就建立新的实例
                if (entry == null)
                {
                    entry = new EventTrigger.Entry();
                    entry.eventID = eventType;
                    // todo 若是已经存在这个事件，它的callback是否还须要new？
                    entry.callback = new EventTrigger.TriggerEvent();
                }

                // 添加触发回调并注册事件
                entry.callback.AddListener(callback);
                trigger.triggers.Add(entry);
            }
            /// <summary>
            /// 获取内容在UI上的总长度
            /// </summary>
            /// <param name="text"></param>
            /// <param name="message"></param>
            /// <returns></returns>
            public static int GetStringLength(Text text, string message)
            {
                int totalLength = 0;
                Font myFont = text.font;
                myFont.RequestCharactersInTexture(message, text.fontSize, text.fontStyle);
                CharacterInfo characterInfo = new CharacterInfo();
                char[] arr = message.ToCharArray();
                foreach (char c in arr)
                {
                    myFont.GetCharacterInfo(c, out characterInfo, text.fontSize);

                    totalLength += characterInfo.advance;
                }
                return totalLength;
            }
        }
    }
}
