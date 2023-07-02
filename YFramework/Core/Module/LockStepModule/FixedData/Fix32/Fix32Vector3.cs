using UnityEngine;

namespace YFramework
{
    public static  class Fix32Vector3
    {
        public const int LEN = Fix32.LEN * 3;
        public static Vector3 ToValue(byte[] data)
        {
            if (data == null || data.Length != Fix32.LEN *3 ) return Vector3.zero;
            float x = Fix32.ToValue(data,0);
            float y = Fix32.ToValue(data,4);
            float z = Fix32.ToValue(data,8);
            return new Vector3(x,y,z);
        }
        public static byte[] ToByte(Vector3 vector)
        {
            byte[] bytes = new byte[Fix32.LEN * 3];
            int index = 0;
            byte[] tempBytes = Fix32.ToByte(vector.x);
            for (int i = 0; i < tempBytes.Length; i++)
            {
                bytes[index++] = tempBytes[i];
            }
            tempBytes = Fix32.ToByte(vector.y);
            for (int i = 0; i < tempBytes.Length; i++)
            {
                bytes[index++] = tempBytes[i];
            }
            tempBytes = Fix32.ToByte(vector.z);
            for (int i = 0; i < tempBytes.Length; i++)
            {
                bytes[index++] = tempBytes[i];
            }
            return bytes;
        }
    }
}
