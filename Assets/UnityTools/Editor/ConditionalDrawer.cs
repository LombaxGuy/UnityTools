using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ConditionalAttribute))]
public class ConditionalDrawer : PropertyDrawer
{
    ConditionalAttribute conditionalAttribute;

    SerializedProperty comparedField;

    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        conditionalAttribute = attribute as ConditionalAttribute;
        comparedField = property.serializedObject.FindProperty(conditionalAttribute.FieldName);

        object value = GetSerializedObjectValue(comparedField);

        bool conditionMet = IsNumeric(value.GetType()) ? CompareNumeric(conditionalAttribute, value) : CompareObject(conditionalAttribute, value);

        propertyHeight = base.GetPropertyHeight(property, label);

        if (conditionMet)
        {
            EditorGUI.PropertyField(position, property);
        }
        else
        {
            propertyHeight = 0;
        }
    }

    private bool CompareObject(ConditionalAttribute attribute, object actualObjectValue)
    {
        Type compareType = attribute.ComparedToValue.GetType();
        Type actualType = actualObjectValue.GetType();

        if (actualType != compareType)
        {
            Debug.LogError(GetType().Name + ": Type mismatch! Trying to compare " + attribute.ComparedToValue.GetType().Name + " to " + actualObjectValue.GetType().Name);
            return false;
        }

        switch (attribute.ComparisonType)
        {
            case ComparisonType.Equal:
                if (attribute.ComparedToValue.Equals(actualObjectValue))
                {
                    return true;
                }
                break;

            case ComparisonType.NotEqual:
                if (!attribute.ComparedToValue.Equals(actualObjectValue))
                {
                    return true;
                }
                break;

            default:
                Debug.LogError(GetType().Name + ": Only 'Equal' and 'NotEqual' comparisons can be made on the type: " + compareType.Name);
                return false;
        }

        return false;
    }

    private bool CompareNumeric(ConditionalAttribute attribute, object actualObjectValue)
    {
        Type compareType = attribute.ComparedToValue.GetType();
        Type actualType = actualObjectValue.GetType();

        if (compareType != actualType)
        {
            Debug.LogError(GetType().Name + ": Type mismatch! Trying to compare " + attribute.ComparedToValue.GetType().Name + " to " + actualObjectValue.GetType().Name);
            return false;
        }

        int comp;

        try
        {
            IComparable actualValue = Convert.ChangeType(attribute.ComparedToValue, compareType) as IComparable;
            IComparable compareValue = Convert.ChangeType(actualObjectValue, compareType) as IComparable;

            comp = compareValue.CompareTo(actualValue);
        }
        catch
        {
            Debug.LogError(GetType().Name + ": Could not convert to 'IComparable' type!");
            return false;
        }

        switch (attribute.ComparisonType)
        {
            case ComparisonType.Equal:
                if (comp == 0)
                {
                    return true;
                }
                break;

            case ComparisonType.NotEqual:
                if (comp != 0)
                {
                    return true;
                }
                break;

            case ComparisonType.GreaterThan:
                if (comp > 0)
                {
                    return true;
                }
                break;

            case ComparisonType.SmallerThan:
                if (comp < 0)
                {
                    return true;
                }
                break;

            case ComparisonType.GreaterOrEqual:
                if (comp >= 0)
                {
                    return true;
                }
                break;

            case ComparisonType.SmallerOrEqual:
                if (comp <= 0)
                {
                    return true;
                }
                break;

            default:
                Debug.LogError(GetType().Name + ": Unknown comparison type: " + attribute.ComparisonType.ToString());
                return false;
        }

        return false;
    }

    private object GetSerializedObjectValue(SerializedProperty property)
    {
        if (property == null)
            return null;

        object obj = property.serializedObject.targetObject;

        FieldInfo field;

        string[] paths = property.propertyPath.Split('.');

        foreach (string path in paths)
        {
            var type = obj.GetType();
            field = type.GetField(path);
            obj = field.GetValue(obj);
        }

        return obj;
    }

    private bool IsNumeric(Type type)
    {
        HashSet<Type> numericTypes = new HashSet<Type>()
        {
            typeof(sbyte), typeof(byte), typeof(short), typeof(ushort),
            typeof(int), typeof(uint), typeof(long), typeof(ulong),
            typeof(float), typeof(double), typeof(decimal)
        };

        return numericTypes.Contains(type);
    }
}
