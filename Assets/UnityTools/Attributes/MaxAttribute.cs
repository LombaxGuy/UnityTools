using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
public class MaxAttribute : PropertyAttribute
{
    public readonly float maxValue;

    public MaxAttribute(float maxValue)
    {
        this.maxValue = maxValue;
    }
}
