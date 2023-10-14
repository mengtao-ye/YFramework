using System.Net;
using System.Net.Sockets;
using System;
using static YFramework.Utility;
using UnityEngine;

namespace YFramework
{
    public class TCPServer : BaseModule
    {
        private Socket mTcpSocket;
        private Message mMsg;
        private RequestHandleManager mRequestHandleManager;
        public bool IsConnect { get { if (mTcpSocket == null) return false; return mTcpSocket.Connected; } }
        public TCPServer(Center center, IMap<short, ITcpRequestHandle> map) : base(center)
        {
            mRequestHandleManager = new RequestHandleManager(this, map);
        }
        public override void Update()
        {
            base.Update();
            if (!IsConnect) return;
            mRequestHandleManager.Update();
        }
        public void Open(string ipAddress, int port)
        {
            mMsg = new Message();
            Run(ipAddress, port);
        }

        private void Run(string ipAddress, int port)
        {
            mTcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            mTcpSocket.Connect(new IPEndPoint(IPAddress.Parse(ipAddress), port));
            Receive();
        }
        private void Receive()
        {
            mTcpSocket.BeginReceive(mMsg.data, mMsg.index, mMsg.resize, SocketFlags.None, new AsyncCallback(CallBack), null);
        }
        private void CallBack(IAsyncResult ar)
        {
            int size = mTcpSocket.EndReceive(ar);
            if (size <= 0)
            {
                return;
            }
            mMsg.Read(size, Response);
            Receive();
        }
        private void Response(short requestCode, short actionCode, byte[] data)
        {
            mRequestHandleManager.Response(requestCode, actionCode, data);
        }
        public void TcpSend(short actionCode, byte[] data)
        {
            int len = 4 + 2 + data.Length;
            mTcpSocket.Send(ByteTools.ConcatParam(len.ToBytes(), actionCode.ToBytes(), data));
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close()
        {
            mTcpSocket.Shutdown(SocketShutdown.Both);
            mTcpSocket.Close();
        }
    }
}

