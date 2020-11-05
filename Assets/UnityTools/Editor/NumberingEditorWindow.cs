using System;
using System.Collections;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;

namespace UnityTools
{
    public class NumberingEditorWindow : EditorWindow
    {
        public enum AffixType { Suffix, Prefix }

        private static bool useSelectedOrder = false;
        private static int startValue = 1;
        private static AffixType affixType = AffixType.Suffix;
        private static int digits = 2;
        private static string seperator = "_";
        private static bool saveOnApply = true;

        private const string keyPrefix = "UnityTools.NumberingEditorWindow.";
        private const string areKeysSetKey = "areKeysSet";
        private const string useSelectedOrderKey = keyPrefix + "useSelectedOrder";
        private const string startValueKey = keyPrefix + "startValue";
        private const string affixTypeKey = keyPrefix + "affixType";
        private const string digitsKey = keyPrefix + "digits";
        private const string seperatorKey = keyPrefix + "seperator";
        private const string saveOnApplyKey = keyPrefix + "saveOnApply";

        private void Awake()
        {
            LoadEditorPrefs();
        }

        // load prefs when scripts recomplie
        [DidReloadScripts]
        private static void OnRecompile()
        {
            LoadEditorPrefs();
        }

        private static void SaveEditorPrefs()
        {
            // mark keys as set
            EditorPrefs.SetBool(areKeysSetKey, true);

            EditorPrefs.SetBool(useSelectedOrderKey, useSelectedOrder);
            EditorPrefs.SetInt(startValueKey, startValue);
            EditorPrefs.SetInt(affixTypeKey, (int)affixType);
            EditorPrefs.SetInt(digitsKey, digits);
            EditorPrefs.SetString(seperatorKey, seperator);
            EditorPrefs.SetBool(saveOnApplyKey, saveOnApply);
        }

        private static void LoadEditorPrefs()
        {
            if (EditorPrefs.HasKey(areKeysSetKey))
            {
                useSelectedOrder = EditorPrefs.GetBool(useSelectedOrderKey);
                startValue = EditorPrefs.GetInt(startValueKey);
                affixType = (AffixType)EditorPrefs.GetInt(affixTypeKey);
                digits = EditorPrefs.GetInt(digitsKey);
                seperator = EditorPrefs.GetString(seperatorKey);
                saveOnApply = EditorPrefs.GetBool(saveOnApplyKey);
            }
        }

        [MenuItem("Tools/UnityTools/Numbering settings")]
        private static void Initialize()
        {
            NumberingEditorWindow window = (NumberingEditorWindow)GetWindow(typeof(NumberingEditorWindow), false, "Numbering Settings");

            window.minSize = new Vector2(225f, 190f);

            window.Show();
        }

        // hotkey ctrl, alt, R
        [MenuItem("Tools/UnityTools/Add Numbering %&r")]
        private static void AddNumbersToSelected()
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

                    // add prefix to name
                    if (IsSeperatorBrackets())
                    {
                        selected[i].name = seperator + ConvertIntToFormattedString(startValue + i) + GetCloseBrackets() + " " + selected[i].name;
                    }
                    else
                    {

                        selected[i].name = ConvertIntToFormattedString(startValue + i) + seperator + selected[i].name;
                    }
                }
                else
                {
                    Undo.RecordObject(selected[i], "Suffix added...");

                    // add suffix to name
                    if (IsSeperatorBrackets())
                    {
                        selected[i].name = selected[i].name + " " + seperator + ConvertIntToFormattedString(startValue + i) + GetCloseBrackets();
                    }
                    else
                    {
                        selected[i].name = selected[i].name + seperator + ConvertIntToFormattedString(startValue + i);
                    }
                }
            }

            Undo.CollapseUndoOperations(undoIndex);

            SaveEditorPrefs();
        }

        private static string ConvertIntToFormattedString(int value)
        {
            string affix = "";
            string stringValue = value.ToString();

            if (stringValue.Length <= digits)
            {
                for (int i = stringValue.Length - 1; i >= 0; i--)
                {
                    affix = affix.Insert(0, stringValue[i].ToString());
                }

                int missingDigits = digits - affix.Length;

                for (int i = 0; i < missingDigits; i++)
                {
                    affix = affix.Insert(0, "0");
                }
            }
            else
            {
                for (int i = digits - 1; i >= 0; i--)
                {
                    affix = affix.Insert(0, stringValue[stringValue.Length - (1 + i)].ToString());
                }
            }

            return affix;
        }

        private void OnGUI()
        {
            GUILayout.Label("General settings", EditorStyles.boldLabel);

            GUIContent content = new GUIContent("Use Selected Order", "Give objects in the hierarchy numbers base on the order in which they were selected.");
            useSelectedOrder = EditorGUILayout.Toggle(content, useSelectedOrder);

            content = new GUIContent("Start Value", "The number we start incrementing from.");
            startValue = EditorGUILayout.IntField(content, startValue);

            GUILayout.Space(10);

            GUILayout.Label("Affix settings", EditorStyles.boldLabel);

            content = new GUIContent("Affix Type", "The type of affix to use when numbering.");
            affixType = (AffixType)EditorGUILayout.EnumPopup(content, affixType);

            content = new GUIContent("Digits", "The number of digits in the added number.");
            digits = EditorGUILayout.IntField(content, digits);

            content = new GUIContent("Seperator", "The seperator added between the name and the number. If you want the number to be inclosed in brackets instead just put the open bracket here.");
            seperator = EditorGUILayout.TextField(content, seperator);

            GUILayout.Space(10);

            GUILayout.Label("Saving", EditorStyles.boldLabel);

            content = new GUIContent("Save on apply only", "The editor settings are only saved to EditorPrefs when numbering is applied. If this settings is disabled settings are saved whenever a change is made.");
            saveOnApply = EditorGUILayout.Toggle(content, saveOnApply);

            if (!saveOnApply && GUI.changed)
            {
                SaveEditorPrefs();
            }
        }

        private static bool IsSeperatorBrackets()
        {
            if (seperator == "(" || seperator == "[" || seperator == "{")
                return true;
            else
                return false;
        }

        private static string GetCloseBrackets()
        {
            if (seperator == "(")
                return ")";
            else if (seperator == "[")
                return "]";
            else if (seperator == "{")
                return "}";
            else
                return "";
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