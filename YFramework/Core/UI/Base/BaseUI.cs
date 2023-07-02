using UnityEngine;

namespace YFramework
{
    public abstract class BaseUI : IUI
    {
        public Transform transform { get; private set; }
        protected RectTransform rectTransform { get; private set; }
        protected GameObject gameObject { get; private set; }
        public bool isShow { get { return gameObject.activeInHierarchy; } set { gameObject.SetActive(value); } }
        public string uiName =>transform.name;
        public BaseUI()
        {

        }
        public BaseUI( Transform trans)
        {
            SetTrans(trans);
        }
        /// <summary>
        /// 设置UI的父对象
        /// </summary>
        /// <param name="trans"></param>
         public void SetTrans(Transform trans) {
            if (trans == null || trans.GetComponent<RectTransform>() == null)
            {
                return;
            }
            transform = trans;
            gameObject = trans.gameObject;
            rectTransform = trans.GetComponent<RectTransform>();
        }
        //虚方法
        public virtual void Show()
        {
            if (gameObject == null)
            {
                return;
            }
            gameObject.SetAvtiveExtend(true);
            //transform.SetAsLastSibling();
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
            if (transform.GetComponent<T>() != null) 
            {
                return transform.GetComponent<T>();
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
    }
}
