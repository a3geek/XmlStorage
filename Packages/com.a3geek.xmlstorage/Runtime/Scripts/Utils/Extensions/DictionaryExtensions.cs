using System.Collections.Generic;

namespace XmlStorage.Utils.Extensions
{
    public static class DictionaryExtensions
    {
        public static T2 GetOrAdd<T1, T2>(this IDictionary<T1, T2> dictionary, T1 key)
            where T2 : class, new()
        {
            if(!dictionary.TryGetValue(key, out var value))
            {
                value = new();
                dictionary[key] = value;
            }

            return value;
        }
    }
}
