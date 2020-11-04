using System.Reflection;
using UnityEditor;

namespace UnityTools.Extensions
{
    public static class SerializedPropertyExtension
    {
        public static object GetSerializedObjectValue(this SerializedProperty property)
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
    }
}