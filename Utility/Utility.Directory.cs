using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YFramework
{
    public partial class Utility 
    {
        public static class DirectoryTools
        {
            /// <summary>
            /// 清空目录下面的所有文件
            /// </summary>
            /// <param name="dirPath"></param>
            public static void ClearDir(string dirPath)
            {
                if (string.IsNullOrEmpty(dirPath))
                {
                    Debug.Log("需要删除的目录为空！");
                    return;
                }
                if (!Directory.Exists(dirPath))
                {
                    Debug.Log("需要删除的目录不存在，地址：" + dirPath);
                    return;
                }
                Directory.Delete(dirPath, true);
                Directory.CreateDirectory(dirPath);
            }

            /// <summary>
            /// 拷贝文件夹
            /// </summary>
            /// <param name="srcPath">需要被拷贝的文件夹路径</param>
            /// <param name="tarPath">拷贝目标路径</param>
            public static void Copy(string srcPath, string tarPath)
            {
                if (!Directory.Exists(srcPath))
                {
                    Debug.Log("CopyFolder is NULL!");
                    return;
                }

                if (!Directory.Exists(tarPath))
                {
                    Directory.CreateDirectory(tarPath);
                }

                //获得源文件下所有文件
                List<string> files = new List<string>(Directory.GetFiles(srcPath));
                files.ForEach(f =>
                {
                    string destFile = Path.Combine(tarPath, Path.GetFileName(f));
                    File.Copy(f, destFile, true); //覆盖模式
                });

                //获得源文件下所有目录文件
                List<string> folders = new List<string>(Directory.GetDirectories(srcPath));
                folders.ForEach(f =>
                {
                    string destDir = Path.Combine(tarPath, Path.GetFileName(f));
                    Copy(f, destDir); //递归实现子文件夹拷贝
                });
            }

            /// <summary>
            /// 判断文件是否存在在文件夹中
            /// </summary>
            /// <param name="directory"></param>
            /// <param name="fileName"></param>
            /// <returns></returns>
            public static bool CheckFileIsIn(string directory, string fileName)
            {
                if (string.IsNullOrEmpty(directory) || string.IsNullOrEmpty(fileName))
                {
                    Debug.LogError("文件夹或者文件名为空，无法查找！");
                    return false;
                }

                if (!Directory.Exists(directory))
                {
                    Debug.LogError(string.Format("文件夹{0}不存在！", directory));
                    return false;
                }
                DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                string extend = Path.GetExtension(fileName);
                FileInfo[] files = directoryInfo.GetFiles("*" + extend, SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    if (files[i].Name == fileName) return true;
                }
                return false;
            }

        }
    }
}
