using System;
using static YFramework.Utility;

namespace YFramework
{
    public class UdpBigDataItem : IDataConverter
    {
        public ushort index;//当前数据所处的下标
        public ushort lastIndex;//最后一个数据的下标
        public int userID;//该消息的发起人
        public byte[] Data = null;
        public bool isReceiveCallBack;//请求方是否收到消息
        public int msgID;
        public UdpBigDataItem()
        {

        }
        public UdpBigDataItem(ushort index, ushort lastIndex,int userID,byte[] data,int msgID)
        {
            this.Data = data;
            this.index = index;
            this.lastIndex = lastIndex;
            this.userID = userID;
            isReceiveCallBack = false;
            this.msgID = msgID;
        }
        public byte[] ToBytes()
        {
            int len =12;
            if (Data != null && Data.Length != 0)
            {
                len += Data.Length;
            }
            byte[] tempData = new byte[len];
            int index = 0;
            tempData[index++] = BitConverter.GetBytes(this.index)[0];
            tempData[index++] = BitConverter.GetBytes(this.index)[1];
            tempData[index++] = BitConverter.GetBytes(lastIndex)[0];
            tempData[index++] = BitConverter.GetBytes(lastIndex)[1];
            byte[] tempUserIDbytes = BitConverter.GetBytes(userID);
            tempData[index++] = tempUserIDbytes[0];
            tempData[index++] = tempUserIDbytes[1];
            tempData[index++] = tempUserIDbytes[2];
            tempData[index++] = tempUserIDbytes[3];

            tempUserIDbytes = BitConverter.GetBytes(msgID);
            tempData[index++] = tempUserIDbytes[0];
            tempData[index++] = tempUserIDbytes[1];
            tempData[index++] = tempUserIDbytes[2];
            tempData[index++] = tempUserIDbytes[3];

            if (Data != null && Data.Length != 0)
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    tempData[index++] = Data[i];
                }
            }
            return tempData;
        }

        public void ToValue(byte[] data)
        {
            if (data.Length <12) return;
            this.index = BitConverter.ToUInt16(data,0);
            this.lastIndex = BitConverter.ToUInt16(data,2);
            this.userID = BitConverter.ToInt32(data, 4);
            this.msgID = BitConverter.ToInt32(data, 8);
            this.Data = ByteTools.SubBytes(data, 12);
        }
    }
}
