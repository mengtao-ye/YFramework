using System.Collections.Generic;

namespace YFramework
{
    public interface IRequestHandle
    {
        short requestCode { get; }
        void Response(short actionCode, Dictionary<string, byte[]> data);
    }
}
