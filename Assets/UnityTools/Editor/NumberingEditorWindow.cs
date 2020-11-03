using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class NumberingEditorWindow : EditorWindow
{
    public enum AffixType { Suffix, Prefix }

    private static bool useSelectedOrder = false;
    private static int startValue = 0;
    private static AffixType affixType = AffixType.Suffix;
    private static string affix = "xx";

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
            Array.Sort(selected, new GameObjectComparer());
        }

        Undo.SetCurrentGroupName("GameObject numbering");
        int undoIndex = Undo.GetCurrentGroup();

        for (int i = 0; i < selected.Length; i++)
        {
            if (affixType == AffixType.Prefix)
            {
                Undo.RecordObject(selected[i], "Prefix added...");
                // add prefix to name
                selected[i].name = ConvertIntToFormattedString(startValue + i, affix) + "_" + selected[i].name;
            }
            else
            {
                Undo.RecordObject(selected[i], "Suffix added...");
                // add suffix to name
                selected[i].name = selected[i].name + "_" + ConvertIntToFormattedString(startValue + i, affix);
            }
        }

        Undo.CollapseUndoOperations(undoIndex);
    }

    private static string ConvertIntToFormattedString(int value, string templateString)
    {
        // create a string with the value
        string stringValue = value.ToString();

        // create character arrays from the template and the value
        char[] templateChars = templateString.ToCharArray();
        char[] valueChars = stringValue.ToCharArray();

        // find the number of digits in the template
        int numbersCount = 0;
        foreach (var c in templateChars)
        {
            if (c == 'x' || c == 'X')
                numbersCount++;
        }

        int index = 0;

        // create an int array that holds the indexes of each digit in the template string
        int[] numberIndexes = new int[numbersCount];
        // we start form behind as numbers has the least significant digit last
        for (int i = templateChars.Length - 1; i >= 0; i--)
        {
            if (templateChars[i] == 'x' || templateChars[i] == 'X')
            {
                numberIndexes[index] = i;
                index++;
            }
        }

        index = 0;

        // create character array that contains the template
        char[] resultArray = templateString.ToCharArray();

        // swap out the template characters with the value characters, again we start from behind
        for (int i = valueChars.Length - 1; i >= 0; i--)
        {
            resultArray[numberIndexes[index]] = valueChars[i];
            index++;
        }

        // replace the remaining digits with 0's
        for (int i = 0; i < resultArray.Length; i++)
        {
            if (resultArray[i] == 'x' || resultArray[i] == 'X')
            {
                resultArray[i] = '0';
            }
        }

        return new string(resultArray);
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

        content = new GUIContent("Affix", "The template used for numbering. All 'x's will be replace by a number. (xxx or XXX)");
        affix = EditorGUILayout.TextField(content, affix);
    }
}

public class GameObjectComparer : IComparer
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