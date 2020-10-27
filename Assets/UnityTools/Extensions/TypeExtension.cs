using System;
using System.Collections.Generic;

namespace UnityTools
{
    public static class TypeExtension
    {
        public static bool IsNumeric(this Type type)
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
}

