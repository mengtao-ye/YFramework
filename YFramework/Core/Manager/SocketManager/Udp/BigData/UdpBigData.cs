using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace YFramework
{
    public class UdpBigData
    {
        public short udpCode { get; }
        public List<UdpBigDataItem> dataList { get; private set; }
        private EndPoint mTarget;
        private Socket mSocket;
        private UdpBigDataManager mManager;
        private int mUserID;
        private int mRefreshCount = 0;
        private UdpMsg mTempUdpMsg;
        public UdpBigData(int userID, short udpCode, UdpBigDataManager manager)
        {
            this.udpCode = udpCode;
            mUserID = userID;
            mManager = manager;
            dataList = new List<UdpBigDataItem>();
            mTempUdpMsg = new UdpMsg();
        }
        public void Add(UdpBigDataItem msg)
        {
            if (msg != null)
            {
                dataList.Add(msg);
            }
        }
        /// <summary>
        /// 首次发送数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="target"></param>
        public void SendData(Socket socket, EndPoint target)
        {
            mSocket = socket;
            SetTarget(target);
            for (int i = 0; i < dataList.Count; i++)
            {
                SendData(dataList[i].ToBytes());
            }
        }
        /// <summary>
        /// 设置目标对象
        /// </summary>
        public void SetTarget(EndPoint point)
        {
            mTarget = point;
        }
        /// <summary>
        /// 检查数据是否都已经发送成功,如果没发送成功就继续发送
        /// </summary>
        public void CheckDataIsFinish()
        {
            try
            {
                mRefreshCount++;
                if (mRefreshCount > SocketData.BIG_DATA_REQUEST_TIME)
                {
                    mManager.Remove(mUserID, udpCode);
                    return;
                }
                for (int i = 0; i < dataList.Count; i++)
                {
                    if (!dataList[i].isReceiveCallBack) //如果接收方未接受到数据时
                    {
                        SendData(dataList[i].ToBytes());
                    }
                }
            }
            catch
            {

            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="data"></param>
        private void SendData(byte[] data)
        {
            try
            {
                mTempUdpMsg.SetUdpMsg(udpCode, (byte)UdpMsgType.BigData, data);
                mSocket.SendTo(mTempUdpMsg.ToBytes(), mTarget);
            }
            catch 
            {
            }
        }
        /// <summary>
        /// 接收已经完成的数据下标
        /// </summary>
        /// <param name="index"></param>
        public int Receive(ushort index)
        {
            int remainCount = 0;
            for (int i = 0; i < dataList.Count; i++)
            {
                if (dataList[i].index == index)
                {
                    dataList[i].isReceiveCallBack = true;
                    break;
                }
            }
            for (int i = 0; i < dataList.Count; i++)
            {
                if (!dataList[i].isReceiveCallBack)
                {
                    remainCount++;
                }
            }
            if (remainCount == 0)//如果所有的消息都接收到了
            {
                mManager.Remove(mUserID, udpCode);
            }
            return remainCount;
        }
    }
}
