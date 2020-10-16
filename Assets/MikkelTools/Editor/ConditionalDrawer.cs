using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(ConditionalAttribute))]
public class ConditionalDrawer : PropertyDrawer
{
    ConditionalAttribute conditional;

    SerializedProperty comparedField;
    
    private float propertyHeight;

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return propertyHeight;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        conditional = attribute as ConditionalAttribute;
        comparedField = property.serializedObject.FindProperty(conditional.FieldName);

        string comparedType = comparedField.type;

        bool conditionMet = false;

        switch (comparedType)
        {
            case "bool":
                conditionMet = CompareBool(conditional, comparedField);
                break;

            case "float":
                conditionMet = CompareFloat(conditional, comparedField);
                break;

            case "int":
                conditionMet = CompareInt(conditional, comparedField);
                break;

            default:
                Debug.LogError( GetType().Name + ": " + "Unsupported type!");
                return;
        }

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

    private bool CompareBool(ConditionalAttribute conditional, SerializedProperty comparedField)
    {
        switch (conditional.ComparisonType)
        {
            case ComparisonType.Equal:
                if (comparedField.boolValue.Equals(conditional.ComparedToValue))
                {
                    return true;
                }
                break;

            case ComparisonType.NotEqual:
                if (!comparedField.boolValue.Equals(conditional.ComparedToValue))
                {
                    return true;
                }
                break;

            default:
                return false;
        }

        return false;
    }

    private bool CompareFloat(ConditionalAttribute conditional, SerializedProperty comparedField)
    {
        float value = comparedField.floatValue;
        float comparedToValue = (float)conditional.ComparedToValue;

        switch (conditional.ComparisonType)
        {
            case ComparisonType.Equal:
                if (value == comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.NotEqual:
                if (value != comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.GreaterThan:
                if (value > comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.SmallerThan:
                if (value < comparedToValue)
                {
                    break;
                }
                break;

            case ComparisonType.GreaterOrEqual:
                if (value >= comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.SmallerOrEqual:
                if (value <= comparedToValue)
                {
                    return true;
                }
                break;
            
            default:
                return false;
        }

        return false;
    }

    private bool CompareInt(ConditionalAttribute conditional, SerializedProperty comparedField)
    {
        int value = comparedField.intValue;
        int comparedToValue = (int)conditional.ComparedToValue;

        switch (conditional.ComparisonType)
        {
            case ComparisonType.Equal:
                if (value == comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.NotEqual:
                if (value != comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.GreaterThan:
                if (value > comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.SmallerThan:
                if (value < comparedToValue)
                {
                    break;
                }
                break;

            case ComparisonType.GreaterOrEqual:
                if (value >= comparedToValue)
                {
                    return true;
                }
                break;

            case ComparisonType.SmallerOrEqual:
                if (value <= comparedToValue)
                {
                    return true;
                }
                break;

            default:
                return false;
        }

        return false;
    }
}
