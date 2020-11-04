using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

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

        [MenuItem("Tools/UnityTools/Numbering settings")]
        private static void Initialize()
        {
            NumberingEditorWindow window = (NumberingEditorWindow)EditorWindow.GetWindow(typeof(NumberingEditorWindow));

            window.minSize = new Vector2(225f, 125f);

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