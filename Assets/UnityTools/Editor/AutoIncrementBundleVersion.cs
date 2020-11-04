using UnityEditor;
using UnityEditor.Callbacks;

namespace UnityTools.Build
{
    public class AutoIncrementBundleVersion : Editor
    {
        [PostProcessBuild]
        private static void IncrementBuildNumberOnBuild(BuildTarget targetPlatform, string buildPath)
        {
            string version = PlayerSettings.bundleVersion;

            string[] versionNumbers = version.Split('.');

            int number = int.Parse(versionNumbers[versionNumbers.Length - 1]) + 1;

            versionNumbers[versionNumbers.Length - 1] = number.ToString();

            PlayerSettings.bundleVersion = string.Join(".", versionNumbers);
        }
    }
}