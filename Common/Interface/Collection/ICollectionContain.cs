namespace YFramework 
{
    /// <summary>
    /// 数组包含接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICollectionContain<in T>
    {
        /// <summary>
        /// 是否包含数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Contains(T data);
    }
}
