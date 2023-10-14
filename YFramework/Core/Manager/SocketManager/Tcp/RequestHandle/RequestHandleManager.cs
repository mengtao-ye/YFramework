using System.Collections.Generic;

namespace YFramework
{
    public class RequestHandleManager
    {
        private TCPServer mTcpServer;
        private Queue<SocketMsg> mSocketMsg;
        private IMap<short, ITcpRequestHandle> mResponseMap;
        public RequestHandleManager(TCPServer socketModule, IMap<short, ITcpRequestHandle> map)
        {
            mResponseMap = map;
            mTcpServer = socketModule;
            mSocketMsg = new Queue<SocketMsg>();
        }

        public void Response( short requestCode, short actionCode, byte[] data)
        {
            SocketMsg mTempMsg =  ClassPool<SocketMsg>.Pop();
            mTempMsg.requestCode = requestCode;
            mTempMsg.actionCode = actionCode;
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
                mResponseMap.Get(mTempSocketMsg.requestCode).Response(mTempSocketMsg.actionCode, mTempSocketMsg.data);
                mTempSocketMsg.Recycle();
            }
        }
    }
}
