namespace YFramework
{
    public static class ListDataExtend
    {
        public static bool IsNullOrEmpty<T>(this IListData<T> listData)
        {
            return listData == null || listData.list.IsNullOrEmpty();
        }
    }
}
