namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// CRC32编码生成工具
        /// </summary>
        public static class CRC32Tool
        {
            private static ulong[] Crc32Table;
            //生成CRC32码表
            private static void GetCRC32Table()
            {
                ulong Crc;
                Crc32Table = new ulong[256];
                int i, j;
                for (i = 0; i < 256; i++)
                {
                    Crc = (ulong)i;
                    for (j = 8; j > 0; j--)
                    {
                        if ((Crc & 1) == 1)
                            Crc = (Crc >> 1) ^ 0xEDB88320;
                        else
                            Crc >>= 1;
                    }
                    Crc32Table[i] = Crc;
                }
            }
            //获取字符串的CRC32校验值
            public static ulong GetCRC32(string sInputString)
            {
                //生成码表
                GetCRC32Table();
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sInputString);
                ulong value = 0xffffffff;
                int len = buffer.Length;
                for (int i = 0; i < len; i++)
                {
                    value = (value >> 8) ^ Crc32Table[(value & 0xFF) ^ buffer[i]];
                }
                return value ^ 0xffffffff;
            }
        }
    }
}