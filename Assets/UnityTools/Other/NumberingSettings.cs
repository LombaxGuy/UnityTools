using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NumberingSettings", menuName = "UnityTools/Numbering Settings")]
public class NumberingSettings : ScriptableObject
{
    public enum NamingScheme { Suffix, Prefix }

    public bool renameInSelectedOrder = false;

    public int startValue = 0;

    public NamingScheme numberingPlacement = NamingScheme.Suffix;

    [Conditional("numberingPlacement", ComparisonType.Equal, NamingScheme.Suffix)]
    public string suffix = "_xx";

    [Conditional("numberingPlacement", ComparisonType.Equal, NamingScheme.Prefix)]
    public string prefix = "xx_";
}
