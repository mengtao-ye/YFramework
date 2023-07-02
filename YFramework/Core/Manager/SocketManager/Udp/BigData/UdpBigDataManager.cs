using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using static YFramework.Utility;

namespace YFramework
{
    public class UdpBigDataManager
    {
        private Dictionary<short, ushort> mMsgIDDict;
        private Socket mSocket;//Socket对象
        private Dictionary<int, List<UdpBigData>> mSendDict;//Key为用户ID，value为用户对应的数据
        public PlatformType platform;
        private int mMsgID;
        private int MAX = int.MaxValue - 1;
        public int msgID { get { return (mMsgID ++) % MAX; } }//防止越界
        public UdpBigDataManager(Socket socket, PlatformType type)
        {
            if (socket == null) return;
            platform = type;
            mSocket = socket;
            mMsgIDDict = new Dictionary<short, ushort>();
            mSendDict = new Dictionary<int, List<UdpBigData>>();
        }
        /// <summary>
        /// 重新设置Socket
        /// </summary>
        /// <param name="socket"></param>
        public void ResetSocket(Socket socket)
        {
            if (socket == null) return;
            mSocket = socket;
        }
        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="udpCode"></param>
        /// <param name="sendData"></param>
        /// <param name="sendTarget"></param>
        public void SendBigData(short udpCode, byte[] sendData, int userID, EndPoint sendTarget)
        {
            try
            {
                UdpBigData bigData = Split(sendData, SocketData.UDP_SPLIT_LENGTH, udpCode, userID, msgID);
                bigData.SendData(mSocket, sendTarget);
                if (!mSendDict.ContainsKey(userID))
                {
                    mSendDict.Add(userID, new List<UdpBigData>());
                }
                mSendDict[userID].Add(bigData);
            }
            catch 
            {
            }
        }
        /// <summary>
        /// 实时刷新发送数据
        /// </summary>
        public void RefreshSendData()
        {
            try
            {
                int[] keys = mSendDict.Keys.ToArray();
                for (int i = 0; i < keys.Length; i++)
                {
                    List<UdpBigData> bigData = null;
                    mSendDict.TryGetValue(keys[i], out bigData);
                    if (bigData != null)
                    {
                        for (int j = 0; j < bigData.Count; j++)
                        {
                            if (bigData.Count > i)
                            {
                                bigData[i].CheckDataIsFinish();
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }
        /// <summary>
        ///  接收到数据回调时的反应
        /// </summary>
        /// <param name="udpCode"></param>
        /// <param name="userID">收到了谁的回调消息</param>
        /// <param name="index"></param>
        /// <param name="msgCode"></param>
        public int ReceiveCallBack(int userID, short udpCode, ushort index)
        {
            UdpBigData bigData = FindBigData(userID, udpCode);
            if (bigData == null) return -1;
            return bigData.Receive(index);
        }
        /// <summary>
        /// 查找大数据块
        /// </summary>
        /// <param name="udpCode"></param>
        /// <param name="msgCode"></param>
        /// <returns></returns>
        private UdpBigData FindBigData(int userID, short udpCode)
        {
            if (!mSendDict.ContainsKey(userID)) return null;
            List<UdpBigData> mSendList = mSendDict[userID];
            for (int i = 0; i < mSendList.Count; i++)
            {
                if (mSendList[i].udpCode == udpCode)
                {
                    return mSendList[i];
                }
            }
            return null;
        }
        /// <summary>
        /// 切割数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="splitLen"></param>
        /// <param name="udpCode"></param>
        /// <param name="msgID"></param>
        /// <returns></returns>
        private UdpBigData Split(byte[] data, int splitLen, short udpCode, int userID, int msgID)
        {
            UdpBigData bigData = new UdpBigData(userID, udpCode, this);
            if (data == null || data.Length < splitLen)
            {
                bigData.Add(new UdpBigDataItem(0, 0, userID, data, msgID));
                return bigData;
            }
            int chunks = data.Length / splitLen;
            int remain = data.Length % splitLen;
            int lastIndex = remain == 0 ? chunks - 1 : chunks;
            for (int i = 0; i < chunks; i++)
            {
                byte[] tempData = ByteTools.SubBytes(data, i * splitLen, splitLen);
                bigData.Add(new UdpBigDataItem((ushort)i, (ushort)lastIndex, userID, tempData, msgID));
            }
            if (remain != 0)
            {
                byte[] tempData = ByteTools.SubBytes(data, chunks * splitLen, remain);
                bigData.Add(new UdpBigDataItem((ushort)chunks, (ushort)lastIndex, userID, tempData, msgID));
            }
            return bigData;
        }
        /// <summary>
        /// 移除已经完成的消息
        /// </summary>
        /// <param name="bigData"></param>
        public void Remove(int userID, short udpCode)
        {
            try
            {
                if (!mSendDict.ContainsKey(userID)) return;
                mSendDict[userID].RemoveAll(item => item.udpCode == udpCode);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 清空玩家的大数据片段
        /// </summary>
        /// <param name="userID"></param>
        public void ClearPlayerData(int userID)
        {
            if (mSendDict.ContainsKey(userID))
            {
                mSendDict.Remove(userID);
            }
        }
        /// <summary>
        /// 更新目标对象
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="target"></param>
        public void UpdateTargetPoint(int userID,EndPoint target)
        {
            try
            {
                if (mSendDict.ContainsKey(userID))
                {
                    List<UdpBigData> data = mSendDict[userID];
                    for (int i = 0; i < data.Count; i++)
                    {
                        data[i].SetTarget(target);
                    }
                }
            }
            catch 
            {
            }
        }
     
    }
}
