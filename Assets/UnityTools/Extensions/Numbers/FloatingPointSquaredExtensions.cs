namespace UnityTools.Extensions
{
    public static class FloatingPointSquaredExtensions
    {
        public static float Squared(this float n)
        {
            return n * n;
        }

        public static double Squared(this double n)
        {
            return n * n;
        }

        public static decimal Squared(this decimal n)
        {
            return n * n;
        }
    }
}