using System;

namespace XmlStorage.Utilities.CustomExtensions
{
    internal static class TypeExtensions
    {
        public static bool IsNeedSerialize(this Type type)
        {
            return type.IsClass || !type.IsSerializable;
        }
    }
}
