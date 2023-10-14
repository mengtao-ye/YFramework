namespace YFramework
{
    public interface ITcpRequestHandle
    {
        short requestCode { get; }
        void Response(short actionCode, byte[] data);
    }
}
