using System.Collections.Generic;
namespace YFramework
{
    public class RequestData
    {
        private Dictionary<short, IRequestHandle> mAllRequestDict;
        public RequestData()
        {
            mAllRequestDict = new Dictionary<short, IRequestHandle>();
        }

        public void AddRequestHandle(IRequestHandle requestHandle) {
            if (mAllRequestDict.ContainsKey(requestHandle.requestCode)) {
                return;
            }
            if (requestHandle == null) {
                return;
            }
            mAllRequestDict.Add(requestHandle.requestCode, requestHandle);
        }
        public IRequestHandle GetRequestHandle(byte requestCode) {
            if (mAllRequestDict.ContainsKey(requestCode)) return mAllRequestDict[requestCode];
            return null;
        }
    }
}
