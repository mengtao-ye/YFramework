using System.Collections.Generic;

namespace YFramework
{
    public class SocketMsg : IReset
    {
        public short requestCode;
        public short actionCode;
        public ushort eventID;
        public Dictionary<string, byte[]> data;

        public void Reset()
        {
            data.Clear();
        }
    } 
}
