
using System;
using System.Collections.Generic;

namespace YFramework
{
    public abstract class BaseRequestHandle : IRequestHandle
    {
        protected abstract short mRequestCode { get; }
        public short requestCode { get { return mRequestCode; } }
        protected  Center mGameCenter;
        protected Dictionary<short, Action<Dictionary<string, byte[]>>> mActionDict;
        public BaseRequestHandle(Center gameCenter)
        {
            mGameCenter = gameCenter;
            mActionDict = new Dictionary<short, Action<Dictionary<string, byte[]>>>();
            ComfigActionCode();
        }
        public void Response(short actionCode, Dictionary<string, byte[]> data)
        {
            if (mActionDict.ContainsKey(actionCode))
            {
                mActionDict[actionCode].Invoke(data);
            }
        }
        protected abstract void ComfigActionCode();
    }
}
