using System;
using System.Collections.Generic;
using System.Linq;
using static YFramework.Utility;

namespace YFramework
{
    /// <summary>
    /// 玩家帧同步数据
    /// </summary>
    public class LockStepUserData : IDataConverter
    {
        public int userID;//玩家ID
        public IListData<byte[]> data;//玩家帧数据
        public LockStepUserData()
        {

        }
        public LockStepUserData(int userID, IListData<byte[]> data)
        {
            this.userID = userID;
            this.data = data;
        }

        public byte[] ToBytes()
        {
            if (data == null || data.Count == 0)
            {
                return BitConverter.GetBytes(userID);
            }
            return ByteTools.Concat(BitConverter.GetBytes(userID), ListTools.GetBytes(data));
        }

        public void ToValue(byte[] data)
        {
            userID = BitConverter.ToInt32(data, 0);
            if (data.Length == 4) return;
            this.data = ListTools.ToList(data, 4);
        }
    }
}
