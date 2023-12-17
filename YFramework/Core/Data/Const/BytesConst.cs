namespace YFramework
{
    /// <summary>
    /// 字节Const数据
    /// </summary>
    public static class BytesConst
    {
        /// <summary>
        /// 对
        /// </summary>
        public static byte[] TRUE_BYTES { get; private set; } = new byte[] { 1 };
        /// <summary>
        /// 错
        /// </summary>
        public static byte[] FALSE_BYTES { get; private set; } = new byte[] { 0 };
        /// <summary>
        /// 空
        /// </summary>
        public static byte[] NULL_BYTES { get; private set; } = new byte[] { };
    }
}
