namespace YFramework
{
    public partial class Utility 
    {
        /// <summary>
        /// 算法
        /// </summary>
        public static class Algorithm
        {
            /// <summary>
            /// 按照从小到大排序
            /// </summary>
            public static void Sort<T>(T[] areas, int low, int high) where T : struct
            {
                if (typeof(T) != typeof(int) || typeof(T) != typeof(float) || typeof(T) != typeof(double))
                {
                    return;
                }
                if (low >= high) return;
                int index = SortUnit(areas, low, high);
                Sort(areas, low, index - 1);
                Sort(areas, index + 1, high);
            }
            private static int SortUnit<T>(T[] areas, int low, int high) where T : struct
            {
                decimal[] area = areas as decimal[];
                decimal key = area[0];
                if (typeof(T) != typeof(int) || typeof(T) != typeof(float) || typeof(T) != typeof(double))
                {
                    return -1;
                }
                T keyArea = areas[low];
                while (low < high)
                {
                    while (area[high] >= key && low < high)
                    {
                        --high;
                    }
                    areas[low] = areas[high];
                    while (area[low] <= key && low < high)
                    {
                        ++low;
                    }
                    areas[high] = areas[low];
                }
                areas[low] = keyArea;
                return low;
            }
        }
    }
}
