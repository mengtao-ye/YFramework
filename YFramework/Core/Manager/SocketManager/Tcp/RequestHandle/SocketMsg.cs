namespace YFramework
{
    public class SocketMsg : IPool
    {
        public short requestCode;
        public short actionCode;
        public byte[] data;
        public bool isPop { get ; set ; }
        public void PopPool()
        {
        }

        public void PushPool()
        {
        }

        public void Recycle()
        {
            ClassPool<SocketMsg>.Push(this);
        }
    } 
}
