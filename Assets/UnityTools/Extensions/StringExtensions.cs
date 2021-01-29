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

        public static string Replace(this string originalString, int characterIndex, char replacementCharacter)
        {
            if (characterIndex > originalString.Length - 1 || characterIndex < 0)
            {
                return null;
            }

            string result = originalString.Remove(characterIndex, 1);
            result = result.Insert(characterIndex, replacementCharacter.ToString());

            return result;
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

        public static int CountOf(this string originalString, char character, bool ignoreCaseing = false)
        {
            int count = 0;

            for (int i = 0; i < originalString.Length; i++)
            {
                if (ignoreCaseing)
                {
                    if (char.ToLower(originalString[i]) == character)
                    {
                        count++;
                    }
                }
                else
                {
                    if (originalString[i] == character)
                    {
                        count++;
                    }
                }
            }

            return count;
        }
    }
}