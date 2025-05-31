using System;

namespace XmlStorage.Utilities.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsNeedSerialize(this Type type)
        {
            return type.IsClass || !type.IsSerializable;
        }
    }
}
