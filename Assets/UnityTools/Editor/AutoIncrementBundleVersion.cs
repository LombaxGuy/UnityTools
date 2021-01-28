using System.Diagnostics;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityTools.Extensions;

namespace UnityTools.Build
{
    public class AutoIncrementBundleVersion : Editor
    {
        private const string menuPath = "Tools/UnityTools/Auto increment version";

        private static bool enabled = true;

        private const string enabledKey = "UnityTools.AutoIncrementBundleVersion.enabled";

        [InitializeOnLoadMethod]
        private static void OnRecompile()
        {
            LoadEditorPrefs();
        }

        private static void LoadEditorPrefs()
        {
            if (EditorPrefs.HasKey(enabledKey))
            {
                enabled = EditorPrefs.GetBool(enabledKey);
            }
        }

        private static void SaveEditorPrefs()
        {
            EditorPrefs.SetBool(enabledKey, enabled);
        }

        // Menu item
        [MenuItem(menuPath, priority = 1)]
        private static void ToggleAutoBuildVersionIncrement()
        {
            enabled = !enabled;

            SaveEditorPrefs();
        }

        // Validate function for above menu item
        [MenuItem(menuPath, true)]
        private static bool ToggleAutoBuildVersionIncrementValidate()
        {
            Menu.SetChecked(menuPath, enabled);

            return true;
        }

        [PostProcessBuild]
        private static void IncrementBuildNumberOnBuild(BuildTarget targetPlatform, string buildPath)
        {
            if (!enabled)
                return;

            string version = PlayerSettings.bundleVersion;

            string[] versionNumbers = version.Split('.');

            int number = int.Parse(versionNumbers.Last()) + 1;

            versionNumbers.Last(number.ToString());

            PlayerSettings.bundleVersion = string.Join(".", versionNumbers);
        }
    }
}