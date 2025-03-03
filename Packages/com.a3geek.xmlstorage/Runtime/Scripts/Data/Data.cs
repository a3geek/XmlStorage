using System;
using System.Collections.Generic;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Data
{
    internal sealed class Data
    {
        public readonly Dictionary<Type, Dictionary<string, object>> data = new();


        public Data() { }

        public Data(in Dictionary<Type, Dictionary<string, object>> data)
        {
            this.data = data;
        }

        public void Update<T>(in string key, in T value)
        {
            this.Update(key, typeof(T), value);
        }

        public void Update<T>(in string key, in Type valueType, in T value)
        {
            var data = this.data.GetOrAdd(valueType);
            data[key] = value;
        }

        // internal bool TryGet<T>(in string key, out T value)
        // {
        //     var type = typeof(T);
        //     if(!this.data.TryGetValue(type, key, out var obj) || obj is not T t)
        //     {
        //         value = default;
        //         return false;
        //     }
        //
        //     value = t;
        //     return true;
        // }
        //
        // internal bool TryGet(in Type type, out Dictionary<string, object> value)
        // {
        //     return this.data.TryGetValue(type, out value);
        // }

        public void Merge(in DataGroup other)
        {
            foreach (var (type, key, value) in other.GetData())
            {
                var dic = this.data.GetOrAdd(type);
                dic.TryAdd(key, value);
            }
        }

        public IEnumerator<(Type type, string key, object value)> GetEnumerator()
        {
            foreach (var (type, data) in this.data)
            {
                foreach (var (key, value) in data)
                {
                    yield return (type, key, value);
                }
            }
        }
    }
}
