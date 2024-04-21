using UnityEngine;

namespace YFramework
{
    public abstract class BaseUI : IUI
    {
        public RectTransform rectTransform { get; private set; }
        public Transform transform { get { return rectTransform; } }
        protected GameObject gameObject { get; private set; }
        public bool isShow { get { return gameObject.activeInHierarchy; } set { gameObject.SetActive(value); } }
        public string uiName => rectTransform.name;
        public ICanvas mUICanvas { get; private set; }
        private bool mIsFirstShow;//是否是首次打开
        public BaseUI()
        {

        }
        public BaseUI(Transform trans)
        {
            SetTrans(trans);
        }
        /// <summary>
        /// 设置UI的父对象
        /// </summary>
        /// <param name="trans"></param>
        public void SetTrans(Transform trans)
        {
            if (trans == null || trans.GetComponent<RectTransform>() == null)
            {
                return;
            }
            gameObject = trans.gameObject;
            rectTransform = trans.GetComponent<RectTransform>();
        }
        public void SetCanvas(ICanvas canvas)
        {
            mUICanvas = canvas;
        }
        //虚方法
        public virtual void Show()
        {
            if (gameObject == null)
            {
                return;
            }
            if (!mIsFirstShow)
            {
                FirstShow();
                mIsFirstShow = true;
            }
            gameObject.SetAvtiveExtend(true);

        }
        public virtual void Hide()
        {
            if (gameObject == null)
            {
                return;
            }
            gameObject.SetActive(false);
        }
        /// <summary>
        /// 反转当前显示状态
        /// </summary>
        public void InverseActive()
        {
            if (isShow)
            {
                Hide();
            }
            else
            {
                Show();
            }
            isShow = !isShow;
        }
        public T GetComponent<T>() where T : Component
        {
            if (rectTransform.GetComponent<T>() != null)
            {
                return rectTransform.GetComponent<T>();
            }
            return null;
        }
        public virtual void Awake() { }
        public virtual void Start() { }
        public virtual void Update() { }
        public virtual void OnDestory() { }
        public virtual void Clear() { }
        public virtual void FixedUpdate() { }
        public virtual void LaterUpdate() { }
        public virtual void FirstShow() { }
        public virtual void Refresh() { }
    }
}
