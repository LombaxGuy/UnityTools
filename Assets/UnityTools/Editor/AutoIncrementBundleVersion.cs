using UnityEditor;
using UnityEditor.Callbacks;
using UnityTools.Extensions;

namespace UnityTools.Build
{
    public class AutoIncrementBundleVersion : Editor
    {
        [PostProcessBuild]
        private static void IncrementBuildNumberOnBuild(BuildTarget targetPlatform, string buildPath)
        {
            string version = PlayerSettings.bundleVersion;

            string[] versionNumbers = version.Split('.');

            int number = int.Parse(versionNumbers.GetLast()) + 1;

            versionNumbers.SetLast(number.ToString());

            PlayerSettings.bundleVersion = string.Join(".", versionNumbers);
        }
    }
}