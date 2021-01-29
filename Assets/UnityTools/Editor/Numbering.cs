using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityTools.Extensions;

namespace UnityTools
{
    public class Numbering : ScriptableObject
    {
        public enum AffixType { Suffix, Prefix }

        private const string menuPath = "Tools/UnityTools/Assign numbers to selected %&r"; // %&r is hotkey code for CTLR+ALT+R
        private const string menuSettingsPath = "Tools/UnityTools/Numbering settings";
        private const string settingsAssetName = "NumberingToolSettings";
        private const string settingsAssetPath = "Assets/UnityTools/Resources/ToolSettings/" + settingsAssetName + ".asset";
        private const string defaultTemplate = "(0)";

        [Header("Numbering")]
        [SerializeField] private int startValue = 1;
        [Tooltip("Give objects numbers in the order they were selected instead of their order in the hierarchy.")]
        [SerializeField] private bool useSelectedOrder = false;

        [Header("Affix")]
        [Tooltip("What type of affix should be used.\nSuffixes are place after the name of the game object and Prefixes are placed infront.")]
        [SerializeField] private AffixType affixType = AffixType.Suffix;
        [Tooltip("The template use to add numbers to objects. All zeroes will be replaced with numbers when numbering is applyed. If no template is provided the default template '(0)' is used.")]
        [SerializeField] private string template = "_00";

        #region Singleton
        private static Numbering _instance;
        public static Numbering Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Numbering>(settingsAssetPath);

                    if (_instance == null)
                    {
                        _instance = CreateInstance<Numbering>();
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
        #endregion

        [MenuItem(menuPath, priority = 201)]
        private static void ApplyNumberingMenu()
        {
            Instance.ApplyNumbering();
        }

        [MenuItem(menuSettingsPath, priority = 202)]
        private static void ShowToolSettingsMenu()
        {
            Selection.activeObject = Instance;
        }

        private void ApplyNumbering()
        {
            GameObject[] selected = Selection.gameObjects;

            // if not objects are selected, return
            if (selected.Length <= 0)
                return;

            // reorder the array if we want to use the order in the hierarchy
            if (!useSelectedOrder)
            {
                Array.Sort(selected, new SiblingIndexComparer());
            }

            Undo.SetCurrentGroupName("GameObject numbering");
            int undoIndex = Undo.GetCurrentGroup();

            for (int i = 0; i < selected.Length; i++)
            {
                if (affixType == AffixType.Prefix)
                {
                    Undo.RecordObject(selected[i], "Prefix added...");

                    selected[i].name = CreateAffix(startValue + i) + selected[i].name;
                }
                else
                {
                    Undo.RecordObject(selected[i], "Suffix added...");

                    selected[i].name = selected[i].name + CreateAffix(startValue + i);
                }
            }

            Undo.CollapseUndoOperations(undoIndex);
        }

        private string CreateAffix(int number)
        {
            if (string.IsNullOrEmpty(template))
            {
                return number.ToString(@defaultTemplate);
            }

            string numberString = number.ToString();
            string result = @template;
            int numberIndex = numberString.Length - 1;

            for (int i = @template.Length - 1; i >= 0; i--)
            {
                if (@template[i] == '0')
                {
                    if (numberIndex >= 0)
                    {
                        result = result.Replace(i, numberString[numberIndex]);
                    }

                    numberIndex--;
                }
            }

            return result;
        }
    }

    public class SiblingIndexComparer : IComparer
    {
        public int Compare(object obj, object compareTo)
        {
            GameObject go1 = (GameObject)obj;
            GameObject go2 = (GameObject)compareTo;

            int index1 = go1.transform.GetSiblingIndex();
            int index2 = go2.transform.GetSiblingIndex();

            return index1.CompareTo(index2);
        }
    }
}