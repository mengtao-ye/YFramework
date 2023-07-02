using System;
using System.Collections.Generic;
using System.Text;

namespace YFramework
{
    public class Message
    {
        public const int MAX_SIZE = 1024 * 1024 * 10;//读取的信息最大缓存量 1M
        private byte[] mData;
        public byte[] data { get { return mData; } set { } }
        private int mIndex = 0;//当前存储的所有的数据的下标
        public int index { get { return mIndex; } private set { } }
        public int resize { get { return MAX_SIZE - mIndex; } }
        int length = 0;
        short actionCode = 0;
        ushort tempID = 0;
        int recordIndex = 0;//临时存储解析后的数据下标
        int readDataIndex = 0;
        byte keyCount = 0;
        string keyData = "";
        int valueCount = 0;
        byte[] valueData = null;
        byte[] lengthByte;
        //byte[] concatData = new byte[MAX_SIZE]; //10M缓存
        byte[] concatKey;
        //byte[] concatValue;
        byte[] valueByte;
        int concatIndex = 0;
        public ushort ID { get; private set; } = 0;//信息的ID
        public Message()
        {
            mData = new byte[MAX_SIZE];
        }

        public void Read(int size, Action<short, short, ushort, Dictionary<string, byte[]>> callback)
        {
            mIndex += size;//size为本次接收到的数据长度
            if (mIndex < 4)//开始4个字节存储的本次数据的长度，如果数据小于4说明数据无法解析
            {
                return;
            }
            recordIndex = 0;
            while (recordIndex < mIndex)
            {
                length = BitConverter.ToInt32(mData, recordIndex);
                if (recordIndex + length > mIndex) //数据不完整
                {
                    recordIndex = 0;
                    return;
                }
                actionCode = BitConverter.ToInt16(mData, recordIndex + 4);
                tempID = BitConverter.ToUInt16(mData, recordIndex + 6);
                readDataIndex = recordIndex + 8;
                Dictionary<string, byte[]> tempDataDict = new Dictionary<string, byte[]>();
                while (readDataIndex < length)
                {
                    keyCount = mData[readDataIndex];
                    if (keyCount == 0)
                    {
                        return;
                    }
                    keyData = Encoding.UTF8.GetString(mData, readDataIndex + 1, keyCount);
                    valueCount = BitConverter.ToInt32(mData, readDataIndex + keyCount + 1);
                    if (valueCount == 0)
                    {
                        valueData = null;
                    }
                    else
                    {
                        valueData = new byte[valueCount];
                        Array.ConstrainedCopy(mData, readDataIndex + keyCount + 5, valueData, 0, valueCount);
                    }
                    readDataIndex += 5 + valueCount + keyCount;//5 为key的长度+value的长度,key的长度为1，value的长度为4
                    if (!tempDataDict.ContainsKey(keyData))
                    {
                        tempDataDict.Add(keyData, valueData);
                    }
                }
                callback((short)((actionCode / CommonData.REQUESTCODE_SPAN) * CommonData.REQUESTCODE_SPAN), actionCode, tempID, tempDataDict);
                recordIndex += length;//当前解析的数据往后移
            }
            if (recordIndex == mIndex)  //说明接收的数据已经解析完
            {
                mIndex = 0;
            }
            else
            {
                Array.Copy(mData, recordIndex, mData, 0, mIndex - recordIndex);
                mIndex -= recordIndex;
            }
        }

        public byte[] ConcatResponse(short actionCode, Dictionary<string, byte[]> data, ushort eventID)
        {
            concatIndex = MAX_SIZE - 1 - 8;//这里-8是为了数据长度及数据类型的内存与事件ID，数据长度占4个字节，数据类型占2个字节，事件ID占2个字节
            if (data != null && data.Count != 0)
            {
                foreach (var item in data)
                {

                    if (item.Key == null)
                    {
                        return null;
                    }
                    concatKey = Encoding.UTF8.GetBytes(item.Key);
                    //concatValue = Encoding.UTF8.GetBytes(item.Value == null ? "" : item.Value);
                    //把key值长度放入数组里面
                    if (mIndex >= concatIndex)  //表示当前缓存已经存满，无法存储
                    {
                        return null;
                    }
                    mData[concatIndex--] = (byte)concatKey.Length;

                    //把key值放入数组里面
                    if (mIndex >= concatIndex - concatKey.Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < concatKey.Length; i++)
                    {
                        mData[concatIndex--] = concatKey[i];
                    }

                    //把value值的长度放入数组里面
                    valueByte = BitConverter.GetBytes(item.Value.Length);
                    if (mIndex >= concatIndex - valueByte.Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < valueByte.Length; i++)
                    {
                        mData[concatIndex--] = valueByte[i];
                    }

                    //把value值值加入数组里面
                    if (mIndex >= concatIndex - item.Value.Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        mData[concatIndex--] = item.Value[i];
                    }
                }
            }
            //把事件码放入数组里面
            byte[] actionCodeBytes = BitConverter.GetBytes(actionCode);
            mData[MAX_SIZE - 5] = actionCodeBytes[0];
            mData[MAX_SIZE - 6] = actionCodeBytes[1];
            byte[] eventIDBytes = BitConverter.GetBytes(eventID);
            mData[MAX_SIZE - 7] = eventIDBytes[0];
            mData[MAX_SIZE - 8] = eventIDBytes[1];
            //把数据长度放入队列里面
            lengthByte = BitConverter.GetBytes(MAX_SIZE - concatIndex - 1);
            for (int i = 0; i < lengthByte.Length; i++)
            {
                mData[MAX_SIZE - i - 1] = lengthByte[i];
            }
            return mData.GetBytes(concatIndex + 1, MAX_SIZE - concatIndex - 1, true);
        }

        public byte[] Concat(short actionCode, Dictionary<string, byte[]> data)
        {
            concatIndex = MAX_SIZE - 1 - 8;//这里-8是为了数据长度及数据类型的内存与事件ID，数据长度占4个字节，数据类型占2个字节，事件ID占2个字节
            if (data != null && data.Count != 0)
            {
                foreach (var item in data)
                {

                    if (item.Key == null)
                    {
                        return null;
                    }
                    concatKey = Encoding.UTF8.GetBytes(item.Key);
                    //concatValue = Encoding.UTF8.GetBytes(item.Value == null?"":item.Value);
                    //把key值长度放入数组里面
                    if (mIndex >= concatIndex)  //表示当前缓存已经存满，无法存储
                    {
                        return null;
                    }
                    mData[concatIndex--] = (byte)concatKey.Length;

                    //把key值放入数组里面
                    if (mIndex >= concatIndex - concatKey.Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < concatKey.Length; i++)
                    {
                        mData[concatIndex--] = concatKey[i];
                    }

                    //把value值的长度放入数组里面
                    valueByte = BitConverter.GetBytes(item.Value.Length);
                    if (mIndex >= concatIndex - valueByte.Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < valueByte.Length; i++)
                    {
                        mData[concatIndex--] = valueByte[i];
                    }

                    //把value值值加入数组里面
                    if (mIndex >= concatIndex - item.Value.Length)
                    {
                        return null;
                    }
                    for (int i = 0; i < item.Value.Length; i++)
                    {
                        mData[concatIndex--] = item.Value[i];
                    }
                }
            }
            //把事件码放入数组里面
            byte[] actionCodeBytes = BitConverter.GetBytes(actionCode);
            mData[MAX_SIZE - 5] = actionCodeBytes[0];
            mData[MAX_SIZE - 6] = actionCodeBytes[1];
            ID = (ushort)((++ID) % (ushort.MaxValue - 1));
            byte[] eventIDBytes = BitConverter.GetBytes(ID);
            mData[MAX_SIZE - 7] = eventIDBytes[0];
            mData[MAX_SIZE - 8] = eventIDBytes[1];
            //把数据长度放入队列里面
            lengthByte = BitConverter.GetBytes(MAX_SIZE - concatIndex - 1);
            for (int i = 0; i < lengthByte.Length; i++)
            {
                mData[MAX_SIZE - i - 1] = lengthByte[i];
            }
            return mData.GetBytes(concatIndex + 1, MAX_SIZE - concatIndex - 1, true);
        }
    }
}
