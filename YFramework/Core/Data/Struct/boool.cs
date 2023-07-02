namespace YFramework
{
    public class Boool
    {
        private byte data;
        public Boool()
        {
            Unknow();
        }
        public Boool(byte value)
        {
            if (value > 2)
            {
                data = 2;
            }
            else 
            {
                data = value;
            }
        }
        public Boool(bool b)
        {
            if (b)
            {
                data = 1;
            }
            else 
            {
                data = 0;
            }
        }
        /// <summary>
        /// 设置为未知数
        /// </summary>
        public void Unknow() {
            data = 2;
        }
        public bool IsUnknow
        {
            get
            {
                return data == 2;
            }
        }
        public void Set(bool b) 
        {
            if (b)
            {
                data = 1;
            }
            else 
            {
                data = 0;
            }
        }
        public static bool operator ==(Boool a, bool b) {
            if (a.IsUnknow) return false;
            if (a.data == 1 && b) return true;
            return false;
        }
        public static bool operator !=(Boool a, bool b)
        {
            if (a.IsUnknow) return true;
            if (a.data == 1 && !b) return true;
            return false;
        }

    }
}
