namespace UnityTools.Extensions
{
    public static class StringExtensions
    {
        public static string GetBetween(this string originalString, string startString, string endString)
        {
            int startIndex = originalString.IndexOf(startString) + startString.Length;
            int endIndex = originalString.IndexOf(endString);

            return originalString.Substring(startIndex, endIndex - startIndex);
        }

        public static string Replace(this string originalString, string newValue, params string[] stringsToReplace)
        {
            foreach (var item in stringsToReplace)
            {
                originalString = originalString.Replace(item, newValue);
            }

            return originalString;
        }

        public static string TrimStart(this string originalString, string stringToRemove)
        {
            if (originalString.StartsWith(stringToRemove))
            {
                return originalString.Remove(0, stringToRemove.Length);
            }
            else
            {
                return originalString;
            }
        }

        public static string TrimEnd(this string originalString, string stringToRemove)
        {
            if (originalString.EndsWith(stringToRemove))
            {
                return originalString.Remove(originalString.Length - stringToRemove.Length);
            }
            else
            {
                return originalString;
            }
        }

        public static string Trim(this string originalString, string stringToRemove)
        {
            string trimmedString = originalString;

            trimmedString = trimmedString.TrimStart(stringToRemove);
            trimmedString = trimmedString.TrimEnd(stringToRemove);

            return trimmedString;
        }
    }
}