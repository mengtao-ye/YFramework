using System;
using static YFramework.Utility;

namespace YFramework
{
    public class Message
    {
        public const int MAX_SIZE = 1024 * 1024 * 100;//读取的信息最大缓存量 10M
        private byte[] mData;//缓存数据区域
        public byte[] data { get { return mData; } set { } }
        private int mIndex = 0;//当前存储的所有的数据的下标
        public int index { get { return mIndex; } private set { } }
        public int resize { get { return MAX_SIZE - mIndex; } }
        private int length = 0;
        private int recordIndex = 0;//临时存储解析后的数据下标
        private int readDataIndex = 0;//读到哪里了
        private short actionCode;
        private byte[] mTempBytes;//缓存字节数据
        public Message()
        {
            mData = new byte[MAX_SIZE];
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        /// <param name="size">读取到的数据大小</param>
        /// <param name="callback">解析后的回调</param>
        public void Read(int size, Action<short, short, byte[]> callback)
        {
            mIndex += size;//size为本次接收到的数据长度
            if (mIndex < 4)//开始4个字节存储的本次数据的长度，如果数据小于4说明数据无法解析
            {
                return;
            }
            recordIndex = 0;
            while (recordIndex < mIndex)
            {
                length = BitConverter.ToInt32(mData, recordIndex);//这里的Len加上了数据长度的4个字节长度与2个字节的ActionCode
                if (recordIndex + length > mIndex) //数据不完整
                {
                    recordIndex = 0;
                    return;
                }
                actionCode = BitConverter.ToInt16(mData, recordIndex + 4);
                readDataIndex = recordIndex + 6;
                mTempBytes = ByteTools.SubBytes(mData, readDataIndex, length - 6);
                callback((short)((actionCode / CommonData.REQUESTCODE_SPAN) * CommonData.REQUESTCODE_SPAN), actionCode, mTempBytes);
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
    }
}
