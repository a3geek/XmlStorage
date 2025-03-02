using System;
using System.Collections.Generic;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Data
{
    internal sealed class Data
    {
         internal readonly Dictionary<Type, Dictionary<string, object>> data = new();


         internal Data() { }

         internal Data(Dictionary<Type, Dictionary<string, object>> data)
         {
             this.data = data;
         }

         internal void Update<T>(in string key, in T value)
         {
             var type = typeof(T);
             if(!this.data.ContainsKey(type))
             {
                 this.data[type] = new Dictionary<string, object>();
             }

             this.data[type][key] = value;
         }

         internal bool TryGet<T>(in string key, out T value)
         {
             var type = typeof(T);
             if(!this.data.TryGetValue(type, key, out var obj) || obj is not T t)
             {
                 value = default;
                 return false;
             }

             value = t;
             return true;
         }

         internal bool TryGet(in Type type, out Dictionary<string, object> value)
         {
             return this.data.TryGetValue(type, out value);
         }

         internal void Merge(in Data other)
         {
             foreach(var (type, key, value) in other)
             {
                 var dic = this.data.GetOrAdd(type);
                 dic.TryAdd(key, value);
             }
         }
         
         public IEnumerator<(Type type, string key, object value)> GetEnumerator()
         {
             foreach(var (type, data) in this.data)
             {
                 foreach(var (key, value) in data)
                 {
                     yield return (type, key, value);
                 }
             }
         }
    }
}
