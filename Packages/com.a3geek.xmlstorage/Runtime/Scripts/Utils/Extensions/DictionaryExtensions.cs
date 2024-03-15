using System.Collections.Generic;

namespace XmlStorage.Utils.Extensions
{
    internal static class DictionaryExtensions
    {
        public static T2 GetOrAdd<T1, T2>(this IDictionary<T1, T2> dictionary, in T1 key)
            where T2 : class, new()
        {
            if(!dictionary.TryGetValue(key, out var value))
            {
                value = new();
                dictionary[key] = value;
            }

            return value;
        }

        public static bool TryGetValue<T1, T2, T3>(
            this IDictionary<T1, Dictionary<T2, T3>> dictionary, in T1 key1, in T2 key2, out T3 value
        )
        {
            if(!dictionary.TryGetValue(key1, out var dic) || !dic.TryGetValue(key2, out value))
            {
                value = default;
                return false;
            }

            return true;
        }
    }
}
