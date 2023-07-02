using System.Collections.Generic;
using UnityEngine;

namespace YFramework
{
    public class UdpRequestHandleManager
    {
        private UdpSocketManager mServer;
        private IMap<short, IUdpRequestHandle> mHandleMap;
        public UdpRequestHandleManager(UdpSocketManager server, IMap<short, IUdpRequestHandle> map)
        {
            mHandleMap = map;
            mServer = server;
        }
       
        public void Response(short udpCode,byte[] data)
        {
            short requestCode =  (short)((udpCode / CommonData.REQUESTCODE_SPAN) * CommonData.REQUESTCODE_SPAN);
            if (mHandleMap.Contains(requestCode))
            {
                mHandleMap.Get(requestCode).Response(udpCode, data);
            }
            else
            {
                Debug.Log("ReqeustCode:"+requestCode+" not exist!");
            }
        }
    }
}
