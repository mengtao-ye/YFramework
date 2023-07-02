namespace YFramework
{
    public interface IDataConverter
    {
        byte[] ToBytes();
        void ToValue(byte[] data);
    }
}
