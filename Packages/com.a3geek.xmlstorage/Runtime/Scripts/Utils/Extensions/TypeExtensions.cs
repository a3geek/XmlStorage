using System;

namespace XmlStorage.Utils.Extensions
{
    public static class TypeExtensions
    {
        public static bool IsNeedSerialize(this Type type)
        {
            return type.IsClass || !type.IsSerializable;
        }
    }
}
