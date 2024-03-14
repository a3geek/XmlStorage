using System;

namespace XmlStorage.Utils.Extensions
{
    internal static class TypeExtensions
    {
        public static bool IsNeedSerialize(this Type type)
        {
            return type.IsClass || !type.IsSerializable;
        }
    }
}
