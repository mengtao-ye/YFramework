using System;
using UnityEngine;

namespace YFramework
{
    public static  class Fix32Vector2
    {
        public const int LEN = Fix32.LEN * 2;
        public static Vector2 ToValue(byte[] data)
        {
            if (data == null || data.Length != Fix32.LEN *2 ) return Vector2.zero;
            float x = Fix32.ToValue(data,0);
            float y = Fix32.ToValue(data,4);
            return new Vector2(x,y);
        }
        public static byte[] ToByte(Vector2 vector)
        {
            byte[] bytes = new byte[Fix32.LEN * 2];
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
            return bytes;
        }
    }
}
