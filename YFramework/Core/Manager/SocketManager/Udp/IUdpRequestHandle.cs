namespace YFramework
{
    public interface IUdpRequestHandle
    {
        short requestCode { get; }
        void Response(short udpCode, byte[] data);
    }
}
