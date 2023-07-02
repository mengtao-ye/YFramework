using System.Collections.Generic;

namespace YFramework
{
    /// <summary>
    /// 窗口UI
    /// </summary>
    public abstract class BaseForm :BaseUI ,IForm
    {
        private List<ISubUI> mSubUIList;//子模块
        public BaseForm()
        {
            mSubUIList = new List<ISubUI>();
        }
        public void AddSubUI( ISubUI subUI)
        {
            if (subUI == null) throw new System.Exception("Sub UI is null");
            mSubUIList.Add(subUI);
            subUI.Awake();
            subUI.Start();
        }
        public ISubUI FindSubUI(string targetName)
        {
            if (targetName == null) return null;
            for (int i = 0; i < mSubUIList.Count; i++)
            {
                if (targetName == mSubUIList[i].uiName) return mSubUIList[i];
            }
            return null;
        }
        public T FindSubUI<T>() where T : class, ISubUI
        {
            string name = typeof(T).FullName;
            for (int i = 0; i < mSubUIList.Count; i++)
            {
                if (name == mSubUIList[i].GetType().FullName) return mSubUIList[i] as T;
            }
            return null;
        }

        public override void Update()
        {
            base.Update();
            for (int i = 0; i < mSubUIList.Count; i++)
            {
                if (mSubUIList[i].isShow) 
                {
                    mSubUIList[i].Update();
                }
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            for (int i = 0; i < mSubUIList.Count; i++)
            {
                if (mSubUIList[i].isShow)
                {
                    mSubUIList[i].FixedUpdate();
                }
            }
        }
        public override void LaterUpdate()
        {
            base.LaterUpdate();
            for (int i = 0; i < mSubUIList.Count; i++)
            {
                if (mSubUIList[i].isShow)
                {
                    mSubUIList[i].LaterUpdate();
                }
            }
        }
        public override void OnDestory()
        {
            base.OnDestory();
            for (int i = 0; i < mSubUIList.Count; i++)
            {
                    mSubUIList[i].OnDestory();
            }
        }
    }
}
