using System.IO;
using System.IO.Compression;

namespace YFramework
{
    public partial class Utility {
        /// <summary>
        /// 压缩工具
        /// </summary>
        public static class CompressTools
        {
            /// <summary>
            /// 压缩字节
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            public static byte[] CompressBytes(byte[] bytes)
            {
                using (MemoryStream compressStream = new MemoryStream())
                {
                    using (var zipStream = new GZipStream(compressStream, CompressionMode.Compress))
                        zipStream.Write(bytes, 0, bytes.Length);
                    return compressStream.ToArray();
                }
            }

            /// <summary>
            /// 解压缩字节
            /// </summary>
            /// <param name="bytes"></param>
            /// <returns></returns>
            public static byte[] Decompress(byte[] bytes)
            {
                using (var compressStream = new MemoryStream(bytes))
                {
                    using (var zipStream = new GZipStream(compressStream, CompressionMode.Decompress))
                    {
                        using (var resultStream = new MemoryStream())
                        {
                            zipStream.CopyTo(resultStream);
                            return resultStream.ToArray();
                        }
                    }
                }
            }
        }
    }
}
