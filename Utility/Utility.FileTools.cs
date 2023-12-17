using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace YFramework
{
    public partial class Utility 
    {
        public partial class FileTools
        {
            /// <summary>
            /// 在数据头部写入数据
            /// </summary>
            /// <param name="path">写入数据的地址</param>
            /// <param name="data">写入的数据</param>
            public static void AppendHeadWrite(string path, byte[] data)
            {
                if (string.IsNullOrEmpty(path) || data == null) return;
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    byte[] tempData = null;
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        tempData = br.ReadBytes((int)fs.Length);
                    }
                    byte[] datas = ByteTools.ConcatParam(BitConverter.GetBytes(data.Length), data, tempData);
                    File.WriteAllBytes(path,datas);
                }
            }
            /// <summary>
            /// 写入数据
            /// </summary>
            /// <param name="path">写入数据的地址</param>
            /// <param name="data">写入的数据</param>
            public static void AppendWrite(string path, byte[] data)
            {
                if (string.IsNullOrEmpty(path) || data == null) return;
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate)) {
                    using (BinaryWriter bw = new BinaryWriter(fs)) {
                        bw.Seek((int)fs.Length, SeekOrigin.Begin);
                        bw.Write(ByteTools.Concat( BitConverter.GetBytes(data.Length),data));
                    }
                }
            }
            /// <summary>
            /// 获取数据
            /// </summary>
            /// <param name="path">写入数据的地址</param>
            /// <param name="data">写入的数据</param>
            public static List<byte[]> GetData(string path)
            {
                if (string.IsNullOrEmpty(path)) return null;
                byte[] data = File.ReadAllBytes(path);
                int index = 0;
                int len = 0;
                List<byte[]> datas = new List<byte[]>();
                while (index < data.Length)
                {
                    len = BitConverter.ToInt32(data, index);
                    byte[] msg = ByteTools.SubBytes(data, index + 4, len);
                    datas.Add(msg);
                }
                return datas;
            }
            /// <summary>
            /// 写入数据
            /// </summary>
            /// <param name="path">写入数据的地址</param>
            /// <param name="data">写入的数据</param>
            public static void AppendWrite(string path, string content)
            {
                if (string.IsNullOrEmpty(path) || content == null) return;
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                File.AppendAllText(path,content);
            }
            /// <summary>
            /// 写入数据,如果路径存在指定的文件则把指定的文件删除重新生成新的文件并写入
            /// </summary>
            /// <param name="path">写入数据的地址</param>
            /// <param name="data">写入的数据</param>
            public static void Write(string path, byte[] data)
            {
                if (string.IsNullOrEmpty(path) || data == null) return;
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                Stream stream = File.Create(path);
                stream.Write(data, 0, data.Length);
                stream.Close();
                stream.Dispose();
            }
            /// <summary>
            /// 写入数据
            /// </summary>
            /// <param name="path">写入数据的地址</param>
            /// <param name="data">写入的数据</param>
            public static void Write(string path, string data)
            {
                Write(path, Encoding.UTF8.GetBytes(data));
            }
            /// <summary>
            /// 清空Txt里面的内容
            /// </summary>
            /// <param name="path"></param>
            public static void ClearTxt(string path)
            {
                if (string.IsNullOrEmpty(path)) return;
                if (!File.Exists(path)) return;
                FileStream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Write);
                stream.Seek(0, SeekOrigin.Begin);
                stream.SetLength(0);
                stream.Close();
            }
            /// <summary>
            /// 创建文件
            /// </summary>
            /// <param name="path"></param>
            public static void CreateFile(string path)
            {
                if (string.IsNullOrEmpty(path)) return;
                string dir = Path.GetDirectoryName(path);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (!File.Exists(path))
                {
                    File.Create(path).Close();
                }
            }
        }
    }
}
