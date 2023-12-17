using System.IO;
using System.Security.Cryptography;
using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        /// <summary>
        /// MD5码生成工具
        /// </summary>
        [System.Serializable]
        public static class MD5Tool
        {
            /// <summary>
            /// 获取MD5码
            /// </summary>
            /// <param name="fliePath">绝对地址路径，包括文件后缀名</param>
            /// <returns></returns>
            public static string Md5(string fliePath)
            {
                string filemd5 = null;
                try
                {
                    using (var fileStream = File.OpenRead(fliePath))
                    {
                        var md5 = MD5.Create();
                        var fileMD5Bytes = md5.ComputeHash(fileStream);//计算指定Stream 对象的哈希值                                     
                        filemd5 = FormatMD5(fileMD5Bytes);
                    }
                }
                catch (System.Exception ex)
                {
                    Debug.LogError(ex);
                }
                return filemd5;
            }
            /// <summary>
            /// 将byte数组转换成MD5码
            /// </summary>
            /// <param name="data"></param>
            /// <returns></returns>
            private static string FormatMD5(byte[] data)
            {
                return System.BitConverter.ToString(data).Replace("-", "").ToLower();//将byte[]装换成字符串
            }
        }
    }

}
