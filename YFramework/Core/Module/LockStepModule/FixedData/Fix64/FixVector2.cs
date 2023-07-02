using System;
using static YFramework.Utility;

namespace YFramework
{
    public struct FixVector2: IDataConverter
    {
        public Fix64 x;
        public Fix64 y;

        public FixVector2(Fix64 x, Fix64 y)
        {
            this.x = x;
            this.y = y;
        }
        public FixVector2(Fix64 x, int y)
        {
            this.x = x;
            this.y = (Fix64)y;
        }

        public FixVector2(int x, int y)
        {
            this.x = (Fix64)x;
            this.y = (Fix64)y;
        }
        public FixVector2(FixVector2 v)
        {
            this.x = v.x;
            this.y = v.y;
        }
        public static FixVector2 operator -(FixVector2 a, int b)
        {
            Fix64 x = a.x - b;
            Fix64 y = a.y - b;
            return new FixVector2(x, y);
        }

        public Fix64 this[int index]
        {
            get { return index == 0 ? x : y; }
            set
            {
                if (index == 0)
                {
                    x = value;
                }
                else
                {
                    y = value;
                }
            }
        }

        public static FixVector2 Zero
        {
            get { return new FixVector2(Fix64.Zero, Fix64.Zero); }
        }

        public static FixVector2 operator +(FixVector2 a, FixVector2 b)
        {
            Fix64 x = a.x + b.x;
            Fix64 y = a.y + b.y;
            return new FixVector2(x, y);
        }

        public static FixVector2 operator -(FixVector2 a, FixVector2 b)
        {
            Fix64 x = a.x - b.x;
            Fix64 y = a.y - b.y;
            return new FixVector2(x, y);
        }

        public static FixVector2 operator *(Fix64 d, FixVector2 a)
        {
            Fix64 x = a.x * d;
            Fix64 y = a.y * d;
            return new FixVector2(x, y);
        }

        public static FixVector2 operator *(FixVector2 a, Fix64 d)
        {
            Fix64 x = a.x * d;
            Fix64 y = a.y * d;
            return new FixVector2(x, y);
        }

        public static FixVector2 operator /(FixVector2 a, Fix64 d)
        {
            Fix64 x = a.x / d;
            Fix64 y = a.y / d;
            return new FixVector2(x, y);
        }

        public static bool operator ==(FixVector2 lhs, FixVector2 rhs)
        {
            return lhs.x == rhs.x && lhs.y == rhs.y;
        }

        public static bool operator !=(FixVector2 lhs, FixVector2 rhs)
        {
            return lhs.x != rhs.x || lhs.y != rhs.y;
        }

        public override bool Equals(object obj)
        {
            return obj is FixVector2 && ((FixVector2)obj) == this;
        }

        public override int GetHashCode()
        {
            return this.x.GetHashCode() + this.y.GetHashCode();
        }


        public static Fix64 SqrMagnitude(FixVector2 a)
        {
            return a.x * a.x + a.y * a.y;
        }

        public static Fix64 Distance(FixVector2 a, FixVector2 b)
        {
            return Magnitude(a - b);
        }

        public static Fix64 Magnitude(FixVector2 a)
        {
            return Fix64.Sqrt(FixVector2.SqrMagnitude(a));
        }

        public void Normalize()
        {
            Fix64 n = x * x + y * y;
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
        }

        public FixVector2 GetNormalized()
        {
            FixVector2 v = new FixVector2(this);
            v.Normalize();
            return v;
        }

        public override string ToString()
        {
            return string.Format("x:{0} y:{1}", x, y);
        }

        public UnityEngine.Vector2 ToVector2()
        {
            return new UnityEngine.Vector2((float)x, (float)y);
        }

        public byte[] ToBytes()
        {
            return ByteTools.Concat(x.ToBytes(),y.ToBytes());
        }

        public void ToValue(byte[] data)
        {
            x = Fix64.ToFix64(BitConverter.ToInt64(data, 0));
            y = Fix64.ToFix64(BitConverter.ToInt64(data, 8));
        }
    } 
}