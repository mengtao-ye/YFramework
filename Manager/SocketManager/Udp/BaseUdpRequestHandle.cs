using System;
using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public abstract class BaseUdpRequestHandle : IUdpRequestHandle
    {
        protected abstract short mRequestCode { get; }
        public short requestCode { get { return mRequestCode; } private set { } }
        private Dictionary<short, Action<byte[]>> mActionDict;
        public BaseUdpRequestHandle()
        {
            mActionDict = new Dictionary<short, Action<byte[]>>();
            ComfigActionCode();
        }
        public void Response(short udpCode, byte[] data) {
            if (mActionDict.ContainsKey(udpCode))
            {
                mActionDict[udpCode].Invoke(data);
            }
            else
            {
                Debug.Log("UdpCode:" + udpCode + " not exist!");
            }
        }
        protected abstract void ComfigActionCode();
        public void Add(short code, Action<byte[]> action) {
            if (mActionDict.ContainsKey(code)) {
                Debug.LogError("Code:"+code+"已注册");
                return;
            }
            mActionDict.Add(code,action);
        }
    }
}
