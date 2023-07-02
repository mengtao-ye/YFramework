using UnityEngine;

namespace YFramework
{
    public abstract class BaseSubUI : BaseUI, ISubUI
    {
        public BaseSubUI(Transform trans)
        {
            SetTrans(trans);
        }
    }
}
