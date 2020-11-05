using UnityEditor;
using UnityEditor.Callbacks;
using UnityTools.Extensions;

namespace UnityTools.Build
{
    public class AutoIncrementBundleVersion : Editor
    {
        const string menuPath = "Tools/UnityTools/Auto increment version";

        private static bool enabled = true;

        private const string enabledKey = "UnityTools.AutoIncrementBundleVersion.enabled";

        private void Awake()
        {
            LoadEditorPrefs();

            Menu.SetChecked(menuPath, enabled);
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            LoadEditorPrefs();

            Menu.SetChecked(menuPath, enabled);
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

        [MenuItem(menuPath)]
        private static void ToggleAutoBuildVersionIncrement()
        {
            enabled = !enabled;

            Menu.SetChecked(menuPath, enabled);

            SaveEditorPrefs();
        }

        [PostProcessBuild]
        private static void IncrementBuildNumberOnBuild(BuildTarget targetPlatform, string buildPath)
        {
            if (!enabled)
                return;

            string version = PlayerSettings.bundleVersion;

            string[] versionNumbers = version.Split('.');

            int number = int.Parse(versionNumbers.GetLast()) + 1;

            versionNumbers.SetLast(number.ToString());

            PlayerSettings.bundleVersion = string.Join(".", versionNumbers);
        }
    }
}