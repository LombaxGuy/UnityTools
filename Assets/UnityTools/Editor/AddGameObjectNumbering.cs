using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class AddGameObjectNumbering : Editor
{
    // hotkey ctrl, alt, R
    [MenuItem("Tools/UnityTools/Add Numbering %&r")]
    private static void AddNumbersToSelected()
    {
        GameObject[] selected = Selection.gameObjects;

        // if not objects are selected, return
        if (selected.Length <= 0)
            return;

        NumberingSettings settings = (NumberingSettings)Resources.Load("Settings/NumberingSettings");

        // reorder the array if we want to use the order in the hierarchy
        if (!settings.renameInSelectedOrder)
        {
            Array.Sort(selected, new GameObjectComparer());
        }

        for (int i = 0; i < selected.Length; i++)
        {
            if (settings.numberingPlacement == NumberingSettings.NamingScheme.Prefix)
            {
                // add prefix to name
                selected[i].name = ConvertIntToFormattedString(settings.startValue + i, settings.prefix) + selected[i].name; 
            }
            else
            {
                // add suffix to name
                selected[i].name += ConvertIntToFormattedString(settings.startValue + i, settings.suffix);
            }
        }
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
