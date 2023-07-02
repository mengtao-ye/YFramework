using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System;
using UnityEngine;

namespace YFramework
{
    public class SocketManager : BaseModule
    {
        private Socket mTcpSocket;
        private Message mMsg;
        private RequestHandleManager mRequestHandleManager;
        private Dictionary<string, byte[]> mSendDataDict;
        private Dictionary<ushort, Action<Dictionary<string, byte[]>>> mCallBackDict;
        public Dictionary<ushort, Action<Dictionary<string, byte[]>>> callBackDict { get { return mCallBackDict; }private set { } }
        public SocketManager(Center center,IMap<short, IRequestHandle> map) : base(center)
        {
            mRequestHandleManager = new RequestHandleManager(this, map);
        }
        public override void Update()
        {
            base.Update();
            if (!IsConnect) return;
            mRequestHandleManager.Update();
        }
        public void Open(string ipAddress,int port)
        {
            mCallBackDict = new Dictionary<ushort, Action<Dictionary<string, byte[]>>>();
            mMsg = new Message();
            mSendDataDict = new Dictionary<string, byte[]>();
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
        private void Response(short requestCode, short actionCode,ushort eventID,Dictionary<string, byte[]> data)
        {
            mRequestHandleManager.Response(requestCode, actionCode,eventID, data);
        }
        public void TcpSend(short actionCode, Dictionary<string, byte[]> data,Action<Dictionary<string, byte[]>> callback = null)
        {
            byte[] sendData = mMsg.Concat(actionCode, data);
            if (callback != null && !mCallBackDict.ContainsKey(mMsg.ID))
            {
                mCallBackDict.Add(mMsg.ID, callback);
            }
            mTcpSocket.Send(sendData);
        }
        public void TcpSend(short actionCode, string key, byte[] value,Action<Dictionary<string, byte[]>> callback = null)
        {
            if (key == null || value == null) return;
            mSendDataDict.Clear();
            mSendDataDict.Add(key, value);
            TcpSend(actionCode, mSendDataDict,callback);
        }
        public bool IsConnect
        {
            get
            {
                if (mTcpSocket == null) return false;
                return mTcpSocket.Connected;
            }
        }
        /// <summary>
        /// 关闭连接
        /// </summary>
        public void Close() {
            mTcpSocket.Shutdown( SocketShutdown.Both);
            mTcpSocket.Close();
        }
    } 
}

