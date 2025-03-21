using System.Collections.Generic;

namespace XmlStorage.Utils.Extensions
{
    internal static class DictionaryExtensions
    {
        public static T2 GetOrAdd<T1, T2>(this Dictionary<T1, T2> dictionary, in T1 key) where T2 : class, new()
        {
            if(dictionary.TryGetValue(key, out var value))
            {
                return value;
            }

            value = new();
            return dictionary[key] = value;
        }
    }
}
