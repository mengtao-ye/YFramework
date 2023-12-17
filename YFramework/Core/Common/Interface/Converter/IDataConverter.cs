namespace YFramework
{
    /// <summary>
    /// 数据转换接口
    /// </summary>
    public interface IDataConverter
    {
        /// <summary>
        /// 将类转换成字节数组
        /// </summary>
        /// <returns></returns>
        byte[] ToBytes();
        /// <summary>
        /// 将字节数组转换成类的数据
        /// </summary>
        /// <param name="data"></param>
        void ToValue(byte[] data);
    }
}
