
namespace YFramework
{
    /// <summary>
    /// 单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> where T : class, new()
    {
        /// <summary>
        /// 单例对象
        /// </summary>
        private static T mInstance;
        /// <summary>
        /// 单例对象
        /// </summary>
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