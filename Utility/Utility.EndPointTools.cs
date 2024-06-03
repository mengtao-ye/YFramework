using System.Net;

namespace YFramework
{
    public class EndPointData
    {
        public string ipAddress;    
        public int port;
    }

    public partial class Utility
    {
        public static class EndPointTools
        {
            public static IPEndPoint GetPoint(byte[] datas, int startIndex)
            {
                if (datas == null || datas.Length < 8)
                {
                    return null;
                }
                return new IPEndPoint(new IPAddress(ByteTools.SubBytes(datas, startIndex, 4)), datas.ToInt(startIndex + 4));
            }
            public static EndPointData GetPointData(byte[] datas, int startIndex)
            {
                if (datas == null || datas.Length < 8)
                {
                    return  null;
                }
                string ipAddress = string.Empty;
                byte[] ipAddressBytes = ByteTools.SubBytes(datas, startIndex, 4);
                for (int i = 0; i < ipAddressBytes.Length; i++)
                {
                    ipAddress += ipAddressBytes[i].ToString();
                    if(i != ipAddressBytes.Length-1)
                    ipAddress += ".";
                }
                return new EndPointData() { ipAddress = ipAddress, port = datas.ToInt(startIndex + 4) };
            }
        }
    }
}
