using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityTools.Extensions;

namespace UnityTools.Tools
{
    public class AutoIncrementBundleVersion : ScriptableObject
    {
        private const string menuPath = "Tools/UnityTools/Auto increment version";
        private const string settingsAssetName = "AutoIncrementBundleVersion";
        private const string settingsAssetPath = "Assets/UnityTools/Resources/ToolSettings/" + settingsAssetName + ".asset";

        private static AutoIncrementBundleVersion _instance;

        [SerializeField] private bool enabled = false;

        public static AutoIncrementBundleVersion Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<AutoIncrementBundleVersion>(settingsAssetName);

                    if (_instance == null)
                    {
                        _instance = CreateInstance<AutoIncrementBundleVersion>();
                        _instance.name = settingsAssetName;

                        AssetDatabase.CreateAsset(_instance, settingsAssetPath);

                        AssetDatabase.Refresh();
                    }

                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        // Menu item
        [MenuItem(menuPath, priority = 1)]
        private static void ToggleAutoBuildVersionIncrement()
        {
            Instance.enabled = !Instance.enabled;
        }

        // Validate function for above menu item
        [MenuItem(menuPath, true)]
        private static bool ToggleAutoBuildVersionIncrementValidate()
        {
            Menu.SetChecked(menuPath, Instance.enabled);

            return true;
        }

        [PostProcessBuild]
        private static void IncrementBuildNumberOnBuild(BuildTarget targetPlatform, string buildPath)
        {
            if (!Instance.enabled)
                return;

            string version = PlayerSettings.bundleVersion;

            string[] versionNumbers = version.Split('.');

            int number = int.Parse(versionNumbers.Last()) + 1;

            versionNumbers.Last(number.ToString());

            PlayerSettings.bundleVersion = string.Join(".", versionNumbers);
        }
    }
}