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
            /// 修改文件内容
            /// </summary>
            /// <param name="path"></param>
            /// <param name="startIndex"></param>
            /// <param name="modifyBytes"></param>
            public static void ModifySubBytes(string path, int startIndex, byte[] modifyBytes)
            {
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    if (startIndex >= 0 && startIndex + modifyBytes.Length <= fs.Length)
                    {
                        byte[] buffer = new byte[fs.Length];
                        // 将文件内容读取到字节数组中
                        fs.Read(buffer, 0, buffer.Length);
                        for (int i = startIndex; i < startIndex + modifyBytes.Length; i++)
                        {
                            buffer[i] = modifyBytes[i - startIndex];
                        }
                        // 重新设置文件大小为原有大小减去被删除的长度
                        fs.SetLength(buffer.Length);
                        fs.Seek(0, SeekOrigin.Begin);
                        // 将更新后的字节数组写回文件
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }


            /// <summary>
            /// 插入文件内容到尾部
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="startIndex"></param>
            /// <param name="insert"></param>
            public static void InsertToTailSubBytes(string filePath, byte[] insert)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    long startIndex = fs.Length;
                    byte[] buffer = new byte[fs.Length + insert.Length];
                    // 将文件内容读取到字节数组中
                    fs.Read(buffer, 0, buffer.Length - insert.Length);
                    for (long i = startIndex; i < startIndex + insert.Length; i++)
                    {
                        buffer[i] = insert[i - startIndex];
                    }
                    // 重新设置文件大小为原有大小减去被删除的长度
                    fs.SetLength(buffer.Length);
                    fs.Seek(0, SeekOrigin.Begin);
                    // 将更新后的字节数组写回文件
                    fs.Write(buffer, 0, buffer.Length);
                }
            }

            /// <summary>
            /// 插入文件内容
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="startIndex"></param>
            /// <param name="insert"></param>
            public static void InsertSubBytes(string filePath, int startIndex, byte[] insert)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    if (startIndex >= 0 && startIndex <= fs.Length)
                    {
                        byte[] buffer = new byte[fs.Length + insert.Length];
                        // 将文件内容读取到字节数组中
                        fs.Read(buffer, 0, buffer.Length - insert.Length);
                        for (int i = buffer.Length - 1; i >= buffer.Length - insert.Length; i--)
                        {
                            int index = i - insert.Length;
                            if (index < 0) break;
                            buffer[i] = buffer[index];
                        }
                        for (int i = startIndex; i < startIndex + insert.Length; i++)
                        {
                            buffer[i] = insert[i - startIndex];
                        }
                        // 重新设置文件大小为原有大小减去被删除的长度
                        fs.SetLength(buffer.Length);
                        fs.Seek(0, SeekOrigin.Begin);
                        // 将更新后的字节数组写回文件
                        fs.Write(buffer, 0, buffer.Length);
                    }
                }
            }
            /// <summary>
            /// 删除文件内容
            /// </summary>
            /// <param name="filePath"></param>
            /// <param name="startIndex"></param>
            /// <param name="lengthToDelete"></param>
            public static void DeleteSubBytes(string filePath, int startIndex, int lengthToDelete)
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    if (startIndex >= 0 && startIndex + lengthToDelete <= fs.Length)
                    {
                        byte[] buffer = new byte[fs.Length];
                        // 将文件内容读取到字节数组中
                        fs.Read(buffer, 0, buffer.Length);
                        for (int i = startIndex; i < startIndex + lengthToDelete; i++)
                        {
                            int index = i + lengthToDelete;
                            if (index < fs.Length)
                            {
                                buffer[i] = buffer[i + lengthToDelete];
                            }
                            else
                            {
                                break;
                            }
                        }
                        // 重新设置文件大小为原有大小减去被删除的长度
                        fs.SetLength(fs.Length - lengthToDelete);
                        fs.Seek(0, SeekOrigin.Begin);
                        // 将更新后的字节数组写回文件
                        fs.Write(buffer, 0, buffer.Length - lengthToDelete);
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
            /// 写入数据（如果有之前的数据的话就把之前的数据删了重新新建一个）
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
            /// <summary>
            /// 强制删除
            /// </summary>
            /// <param name="path"></param>
            public static void ForceDelete(string path)
            {
                if (path.IsNullOrEmpty()) return;
                if (File.Exists(path)) {
                    File.Delete(path);
                }
            }
        }
    }
}
