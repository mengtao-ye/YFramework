using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace YFramework
{
    /// <summary>
    /// 刷新ScrollView
    /// </summary>
    public class FlashScrollView  : ScrollRect
    {
        float f = 0;
        //是否加载
        bool isRef = false;
        //是否刷新
        bool isLoad = false;
        //是否处于拖动
        bool isDrag = false;
        private Action upDrag;//上拉
        private Action downDrag;//下拉
        float size = 0;
        public float sensitive = 2f;//下拉或上拉刷新的灵敏值
        private RectTransform rect;
        protected override void Awake()
        {
            base.Awake();
            onValueChanged.AddListener(ScrollValueChanged);
            rect = GetComponentInChildren<ContentSizeFitter>().GetComponent<RectTransform>();
        }
        /// <summary>
        /// 当ScrollRect被拖动时
        /// </summary>
        /// <param name="vector">被拖动的距离与Content的大小比例</param>
        void ScrollValueChanged(Vector2 vector)
        {
            //如果不拖动 当然不执行之下的代码
            if (!isDrag)
                return;
            f = 100 / rect.rect.height * Mathf.Max( sensitive,0.1f);
            if (verticalScrollbar.size + f < size)
            {
                if (verticalScrollbar.value >= 1)
                    isRef = true;
                else if(verticalScrollbar.value < 0.1f)
                    isLoad = true;
            }
            else
            {
                isRef = false;
                isLoad = false;
            }
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            base.OnBeginDrag(eventData);
            isDrag = true;
            size = verticalScrollbar.size;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {

            base.OnEndDrag(eventData);
            if (isRef)
                upDrag?.Invoke();
            if (isLoad)
                downDrag?.Invoke();
            isRef = false;
            isLoad = false;
            isDrag = false;
        }
        /// <summary>
        /// 添加向上拉动刷新的事件监听
        /// </summary>
        public void AddUpDragFrash(Action callBack) {
            upDrag =  callBack;
        }
        /// <summary>
        /// 添加向下拉动刷新功能
        /// </summary>
        /// <param name="callBack"></param>
        public void AddDownDragFrash(Action callBack)
        {
            downDrag = callBack;
        }

    }
}
