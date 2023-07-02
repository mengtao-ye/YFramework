using System;
using UnityEngine;

namespace YFramework
{
    public abstract class BaseTween : ITween
    {
        public float time { get; private set; }
        public Transform transform { get; private set; }
        private Action mComplete;
        public BaseTween(float time,Transform trans)
        {
            this.time = time;
            transform = trans;
        }
        public abstract void Complete();
        protected void OnFinish() {
            if (mComplete != null) mComplete.Invoke();
        }
        public void OnComplete(Action complete)
        {
            mComplete = complete;
        }
    }
}
