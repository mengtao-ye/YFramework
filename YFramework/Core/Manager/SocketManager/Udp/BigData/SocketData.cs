namespace YFramework
{
    public static class SocketData
    {
        public const int BIG_DATA_REQUEST_TIME = 10;//大数据请求的时长
        public const float BIG_DATA_REFRESH_TIME =0.5f;//发送大数据刷新的时间
        public const ushort UDP_SPLIT_LENGTH = 1024;//UDP通信时一段数据的长度
        public const int UDP_MAX_SIZE = 10240000;//接收UDP数据数组的最大长度
        public const ushort TIMEOUT = 5000;//UDP数据响应时长
    }
}
