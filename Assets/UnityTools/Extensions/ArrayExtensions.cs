namespace UnityTools.Extensions
{
    public static class ArrayExtensions
    {
        public static T GetLast<T>(this T[] array) where T : class
        {
            return array[array.Length - 1];
        }

        public static void SetLast<T>(this T[] array, T value) where T : class
        {
            array[array.Length - 1] = value;
        }
    }
}