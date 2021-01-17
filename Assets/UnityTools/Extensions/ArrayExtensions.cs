namespace UnityTools.Extensions
{
    public static class ArrayExtensions
    {
        public static T Last<T>(this T[] array) where T : class
        {
            return array[array.Length - 1];
        }

        public static void Last<T>(this T[] array, T value) where T : class
        {
            array[array.Length - 1] = value;
        }

        public static bool Contains<T>(this T[] array, T element) where T : class
        {
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] == element)
                {
                    return true;
                }
            }

            return false;
        }
    }
}