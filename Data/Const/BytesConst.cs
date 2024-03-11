using System;

namespace YFramework
{
    /// <summary>
    /// 字节Const数据
    /// </summary>
    public static class BytesConst
    {
        /// <summary>
        /// 空字节
        /// </summary>
        public static byte[] Empty { get; private set; } = new byte[0];
        /// <summary>
        /// true的常量字节数据
        /// </summary>
        public static byte[] TRUE_BYTES { get; private set; } = BitConverter.GetBytes(true);
        /// <summary>
        /// false 的常量字节数据
        /// </summary>
        public static byte[] FALSE_BYTES { get; private set; } = BitConverter.GetBytes(false);
        /// <summary>
        /// 空
        /// </summary>
        public static byte[] NULL_BYTES { get; private set; } = new byte[] { };
    }
}
