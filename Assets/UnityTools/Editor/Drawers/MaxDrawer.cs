using System;
using UnityEngine;
using UnityEditor;
using UnityTools.Extensions;

namespace UnityTools.Attributes
{
    [CustomPropertyDrawer(typeof(MaxAttribute))]
    public class MaxDrawer : PropertyDrawer
    {

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            MaxAttribute maxAttribute = attribute as MaxAttribute;

            object value = property.GetSerializedObjectValue();

            Type type = value.GetType();

            if (type == typeof(int))
            {
                if ((int)value > (int)maxAttribute.maxValue)
                {
                    property.intValue = (int)maxAttribute.maxValue;
                }
            }
            else if (type == typeof(float))
            {
                if ((float)value > (maxAttribute.maxValue))
                {
                    property.floatValue = maxAttribute.maxValue;
                }
            }
            else
            {
                Debug.LogError(GetType().Name + ": Unsupported type! The Max attribute can only be used on fields of type float or int.");
            }

            EditorGUI.PropertyField(position, property);
        }
    }

}