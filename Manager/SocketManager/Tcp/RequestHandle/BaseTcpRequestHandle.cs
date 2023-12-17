using System;
using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public abstract class BaseTcpRequestHandle : ITcpRequestHandle
    {
        protected abstract short mRequestCode { get; }
        public short requestCode { get { return mRequestCode; } }
        protected Dictionary<short, Action<byte[]>> mActionDict;
        public BaseTcpRequestHandle()
        {
            mActionDict = new Dictionary<short, Action<byte[]>>();
            ComfigActionCode();
        }
        public void Add(short actionCode, Action<byte[]> callBack)
        {
            if (mActionDict.ContainsKey(actionCode)) {
                Debug.LogError("已包含ActionCode:"+actionCode);
                return;
            }
            if (callBack == null) {
                Debug.LogError("cakkBack is null");
                return;
            }
            mActionDict.Add(actionCode,callBack);
        }

        public void Response(short actionCode,  byte[] data)
        {
            if (mActionDict.ContainsKey(actionCode))
            {
                mActionDict[actionCode].Invoke(data);
            }
        }
        protected abstract void ComfigActionCode();
    }
}
