using UnityEngine;

namespace YFramework
{
    public partial class Utility
    {
        public static partial class VersionTools 
        {
            /// <summary>
            /// 根据版本号获取版本编码
            /// </summary>
            /// <returns></returns>
            public static int GetVersionCode(string version)
            {
                try
                {
                    string[] strs = version.Split('.');
                    int bigVersion = int.Parse(strs[0]);
                    int dieDaiVersion = int.Parse(strs[1]);
                    int bugVersion = int.Parse(strs[2]);
                    return int.Parse(bigVersion.ToString("000") + dieDaiVersion.ToString("000") + bugVersion.ToString("000"));
                }
                catch
                {
                    Debug.LogError("版本异常：" + version);
                    return 0;
                }
            }

        }
    }
}
