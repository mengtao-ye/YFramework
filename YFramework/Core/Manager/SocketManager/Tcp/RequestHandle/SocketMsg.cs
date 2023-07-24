using System.Collections.Generic;

namespace YFramework
{
    public class SocketMsg : IPool
    {
        public short requestCode;
        public short actionCode;
        public ushort eventID;
        public Dictionary<string, byte[]> data;
        public bool isPop { get ; set ; }
        public void PopPool()
        {
        }

        public void PushPool()
        {
            data.Clear();
        }

        public void Recycle()
        {
            ClassPool<SocketMsg>.Push(this);
        }
    } 
}
