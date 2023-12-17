using System;
using UnityEngine;
using static YFramework.Utility;

namespace YFramework
{
    public struct FixVector3 : IDataConverter
    {
        public Fix64 x;
        public Fix64 y;
        public Fix64 z;
        public FixVector3(Vector3 vector3)
        {
            this.x = (Fix64)vector3.x;
            this.y = (Fix64)vector3.y;
            this.z = (Fix64)vector3.z;
        }
        public FixVector3(float x, float y, float z)
        {
            this.x = (Fix64)x;
            this.y = (Fix64)y;
            this.z = (Fix64)z;
        }

        public FixVector3(Fix64 x, Fix64 y, Fix64 z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public FixVector3(FixVector3 v)
        {
            this.x = v.x;
            this.y = v.y;
            this.z = v.z;
        }

        public Fix64 this[int index]
        {
            get
            {
                if (index == 0)
                    return x;
                else if (index == 1)
                    return y;
                else
                    return z;
            }
            set
            {
                if (index == 0)
                    x = value;
                else if (index == 1)
                    y = value;
                else
                    y = value;
            }
        }
        public static FixVector3 Zero = new FixVector3(Fix64.Zero, Fix64.Zero, Fix64.Zero);

        public static FixVector3 operator +(FixVector3 a, FixVector3 b)
        {
            Fix64 x = a.x + b.x;
            Fix64 y = a.y + b.y;
            Fix64 z = a.z + b.z;
            return new FixVector3(x, y, z);
        }

        public static FixVector3 operator -(FixVector3 a, FixVector3 b)
        {
            Fix64 x = a.x - b.x;
            Fix64 y = a.y - b.y;
            Fix64 z = a.z - b.z;
            return new FixVector3(x, y, z);
        }

        public static FixVector3 operator *(Fix64 d, FixVector3 a)
        {
            Fix64 x = a.x * d;
            Fix64 y = a.y * d;
            Fix64 z = a.z * d;
            return new FixVector3(x, y, z);
        }

        public static FixVector3 operator *(FixVector3 a, Fix64 d)
        {
            Fix64 x = a.x * d;
            Fix64 y = a.y * d;
            Fix64 z = a.z * d;
            return new FixVector3(x, y, z);
        }

        public static FixVector3 operator /(FixVector3 a, Fix64 d)
        {
            Fix64 x = a.x / d;
            Fix64 y = a.y / d;
            Fix64 z = a.z / d;
            return new FixVector3(x, y, z);
        }

        public static bool operator ==(FixVector3 lhs, FixVector3 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
        }

        public static bool operator !=(FixVector3 lhs, FixVector3 rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
        }

        public static Fix64 SqrMagnitude(FixVector3 a)
        {
            return a.x * a.x + a.y * a.y + a.z * a.z;
        }

        public static Fix64 Distance(FixVector3 a, FixVector3 b)
        {
            return Magnitude(a - b);
        }

        public static Fix64 Magnitude(FixVector3 a)
        {
            return Fix64.Sqrt(FixVector3.SqrMagnitude(a));
        }

        public void Normalize()
        {
            Fix64 n = x * x + y * y + z * z;
            if (n == Fix64.Zero)
                return;

            n = Fix64.Sqrt(n);

            if (n < (Fix64)0.0001)
            {
                return;
            }

            n = 1 / n;
            x *= n;
            y *= n;
            z *= n;
        }

        public FixVector3 GetNormalized()
        {
            FixVector3 v = new FixVector3(this);
            v.Normalize();
            return v;
        }

        public override string ToString()
        {
            return string.Format("x:{0} y:{1} z:{2}", x, y, z);
        }

        public override bool Equals(object obj)
        {
            return obj is FixVector2 && ((FixVector3)obj) == this;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() + this.y.GetHashCode() + this.z.GetHashCode();
        }

        public static FixVector3 Lerp(FixVector3 from, FixVector3 to, Fix64 factor)
        {
            return from * (1 - factor) + to * factor;
        }
        public UnityEngine.Vector3 ToVector3()
        {
            return new UnityEngine.Vector3((float)x, (float)y, (float)z);
        }
        public byte[] ToBytes()
        {
            return ByteTools.ConcatParam(x.ToBytes(), y.ToBytes(),z.ToBytes());
        }
        public void ToValue(byte[] data)
        {
            x = Fix64.ToFix64(BitConverter.ToInt64(data, 0));
            y = Fix64.ToFix64(BitConverter.ToInt64(data, Fix64.length));
            z = Fix64.ToFix64(BitConverter.ToInt64(data, Fix64.length*2));
        }
        public static FixVector3 StaticToValue(byte[] data,int startIndex)
        {
            Fix64 x = Fix64.ToFix64(BitConverter.ToInt64(data, startIndex));
            Fix64 y = Fix64.ToFix64(BitConverter.ToInt64(data, startIndex+ Fix64.length));
            Fix64 z = Fix64.ToFix64(BitConverter.ToInt64(data, startIndex + Fix64.length * 2));
            return new FixVector3(x, y, z);
        }
        public static FixVector3 StaticToValue(byte[] data)
        {
            return StaticToValue(data,0);
        }
        public static byte[] ToBytes(Vector3 vector3)
        {
            return new FixVector3(vector3).ToBytes();
        }
    }

}