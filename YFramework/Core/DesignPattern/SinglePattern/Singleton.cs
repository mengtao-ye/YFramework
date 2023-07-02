
namespace YFramework
{
    public class Singleton<T> where T : class, new()
    {
        private static T mInstance;
        public static T Instance
        {
            get
            {
                if (mInstance == null) mInstance = new T();
                return mInstance;
            }
        }
    }
}