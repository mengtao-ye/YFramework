using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using static YFramework.Utility;

namespace YFramework
{
    public abstract class UdpSocketManager : BaseModule
    {
        protected Socket mUdpSocket;//Udp对象
        private EndPoint mPoint;
        public EndPoint serverPoint { get { return mPoint; } }//服务器对象
        private UdpRequestHandleManager mUdpHandle;
        private EndPoint mUdpReceivePoint;
        private byte[] mBuffer;
        private Queue<UdpMsg> mMsgQueue;
        private string mIPAddress = null;
        private int mPort = 0;
        Thread receiveThread;
        private Action mReconnectAction;
        public UdpSocketManager(Center center, IMap<short, IUdpRequestHandle> map) : base(center)
        {
            mUdpHandle = new UdpRequestHandleManager(this, map);

        }
        /// <summary>
        /// 打开UDP连接
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public void Open(string ipAddress, int port)
        {
            if (!isRun) return;
            InitUdpSocket(ipAddress, port);
        }

        private void InitUdpSocket(string ipAddress, int port)
        {
            try
            {
                mIPAddress = ipAddress;
                mPort = port;
                mMsgQueue = new Queue<UdpMsg>();
                mBuffer = new byte[CommonData.UDP_BUFFER_SIZE];
                ReConnectServer();
                Debug.Log("UDP服务器启动成功！");
            }
            catch 
            {

            }
        }
        /// <summary>
        /// 重新连接服务器
        /// </summary>
        public void ReConnectServer()
        {
            try
            {
                if (mUdpSocket != null && mUdpSocket.Connected)
                {
                    mUdpSocket.Shutdown(SocketShutdown.Both);
                    mUdpSocket.Close();
                }
                mUdpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                mPoint = new IPEndPoint(IPAddress.Parse(mIPAddress), mPort);
                mUdpSocket.Connect(mPoint);
                mUdpSocket.ReceiveBufferSize = CommonData.UDP_BUFFER_SIZE;
                mUdpSocket.SendBufferSize = CommonData.UDP_BUFFER_SIZE;
                mUdpReceivePoint = new IPEndPoint(IPAddress.Any, 0);
                //uint IOC_IN = 0x80000000;
                //uint IOC_VENDOR = 0x18000000;
                //uint SIO_UDP_CONNRESET = IOC_IN | IOC_VENDOR | 12;
                //mUdpSocket.IOControl((IOControlCode)SIO_UDP_CONNRESET, new byte[] { Convert.ToByte(false) }, null);
                if (receiveThread != null) receiveThread.Abort();
                receiveThread = new Thread(UdpReceive);
                receiveThread.Start();
                mReconnectAction?.Invoke();
            }
            catch
            {

            }
        }

        public override void Update()
        {
            if (!isRun) return;
            try
            {
                if (mUdpSocket == null) return;
                while (mMsgQueue.Count != 0)
                {
                    UdpMsg msg = mMsgQueue.Dequeue();
                    if (mUdpHandle != null)
                    {
                        ResponseData(msg);
                        //mUdpHandle.Response(msg.udpCode, msg.Data);
                    }
                }
            }
            catch
            {
                ReConnectServer();
                mMsgQueue.Clear();
            }
        }

        /// <summary>
        /// 解析发送过来的数据
        /// </summary>
        /// <param name="msg"></param>
        public abstract void ResponseData(UdpMsg msg);
        /// <summary>
        /// UDP发送消息
        /// </summary>
        /// <param name="udpCode"></param>
        /// <param name="type">数据类型 0 为小数据，1为大数据</param>
        /// <param name="data"></param>
        public void UdpSend(short udpCode, byte type, byte[] data)
        {
            try
            {
                if (data != null && data.Length > CommonData.UDP_SPLIT_LENGTH)
                {
                    Debug.LogError("UdpCode:" + udpCode + "发送的数据过长");
                    return;
                }
                if (mUdpSocket != null && mPoint != null) 
                {
                    UdpMsg msg = new UdpMsg();
                    msg.SetUdpMsg(udpCode, type, data);
                    mUdpSocket.SendTo(msg.ToBytes(), mPoint);
                }
            }
            catch 
            {
            }
           
        }
        /// <summary>
        /// UDP数据接收
        /// </summary>
        private void UdpReceive()
        {
            if (!isRun) return;
            while (true)
            {
                try
                {
                    int length = mUdpSocket.ReceiveFrom(mBuffer, ref mUdpReceivePoint);
                    if (length < 3) continue; //如果没有接收到任何数据时 
                    UdpMsg msg = new UdpMsg();
                    msg.SetUdpMsg(BitConverter.ToInt16(mBuffer, 0), mBuffer[2], ByteTools.SubBytes(mBuffer, 3, length - 3));
                    mMsgQueue.Enqueue(msg);
                }
                catch
                {
                    Thread.Sleep(10);
                }
            }
        }
        /// <summary>
        /// 添加重新连接的回调
        /// </summary>
        /// <param name="callBack"></param>
        public void AddReconnectCallback(Action callBack)
        {
            mReconnectAction += callBack;
        }
        /// <summary>
        /// 解析返回的数据
        /// </summary>
        public void Response(short udpCode,byte[] data) 
        {
            mUdpHandle.Response(udpCode,data);
        }
    }
}
