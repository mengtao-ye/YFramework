using System.Collections.Generic;

namespace YFramework
{
    public class RequestHandleManager
    {
        private SocketManager mSocketManager;
        private Queue<SocketMsg> mSocketMsg;
        private IMap<short, IRequestHandle> mResponseMap;
        public RequestHandleManager(SocketManager socketModule, IMap<short, IRequestHandle> map)
        {
            mResponseMap = map;
            mSocketManager = socketModule;
            mSocketMsg = new Queue<SocketMsg>();
        }

        public void Response( short requestCode, short actionCode, ushort eventID,Dictionary<string, byte[]> data)
        {
            SocketMsg mTempMsg =  ClassPool<SocketMsg>.Pop();
            mTempMsg.requestCode = requestCode;
            mTempMsg.actionCode = actionCode;
            mTempMsg.eventID = eventID;
            mTempMsg.data = data;
            mSocketMsg.Enqueue(mTempMsg);
        }

        //主线程执行的Update
        public void Update()
        {
            if (mSocketMsg.Count == 0) return;
            for (int i = 0; i < mSocketMsg.Count; i++)
            {
                SocketMsg mTempSocketMsg = mSocketMsg.Dequeue();
                if (mTempSocketMsg.actionCode < 0)//EventCode，代表是服务器发送过来的请求 
                {
                    mResponseMap.Get(mTempSocketMsg.requestCode).Response(mTempSocketMsg.actionCode, mTempSocketMsg.data);
                }
                else 
                {
                    if (mSocketManager.callBackDict.ContainsKey(mTempSocketMsg.eventID)){
                       
                        mSocketManager.callBackDict[mTempSocketMsg.eventID].Invoke(mTempSocketMsg.data);
                        mSocketManager.callBackDict.Remove(mTempSocketMsg.eventID);
                    }
                }
                mTempSocketMsg.Recycle();
            }
        }
    }
}
