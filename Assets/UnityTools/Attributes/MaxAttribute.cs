using System;
using UnityEngine;

namespace UnityTools.Attributes
{
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class MaxAttribute : PropertyAttribute
    {
        public readonly float maxValue;

        public MaxAttribute(float maxValue)
        {
            this.maxValue = maxValue;
        }
    }
}

