using System;
using UnityEngine;

namespace UnityTools.Attributes
{
    public enum ComparisonType
    {
        Equal,
        NotEqual,
        GreaterThan,
        SmallerThan,
        GreaterOrEqual,
        SmallerOrEqual
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class ConditionalAttribute : PropertyAttribute
    {
        public string FieldName { get; private set; }
        public object ComparedToValue { get; private set; }
        public ComparisonType ComparisonType { get; private set; }


        public ConditionalAttribute(string fieldName, ComparisonType comparisonType, object comparedToValue)
        {
            FieldName = fieldName;
            ComparisonType = comparisonType;
            ComparedToValue = comparedToValue;
        }
    }
}