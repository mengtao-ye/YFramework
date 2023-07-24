using System;
using System.Collections.Generic;
using static YFramework.Utility;

namespace YFramework
{
    public class LockStepData : IDataConverter
    {
        public int frameIndex;//帧下标
        public List<LockStepUserData> frameData;//帧数据
        public LockStepData()
        {

        }

        public byte[] ToBytes()
        {
            if (frameData == null || frameData.Count == 0)
            {
                return BitConverter.GetBytes(frameIndex);
            }
            return ByteTools.Concat(BitConverter.GetBytes(frameIndex), ConverterDataTools.ToByte(frameData));
        }

        public void ToValue(byte[] data)
        {
            frameIndex = BitConverter.ToInt32(data, 0);
            if (data.Length == 4) return;
            this.frameData = ConverterDataTools.ToListObject<LockStepUserData>(data, 4);
        }
    }
}
