using System;
using System.Collections.Generic;
using System.Text;
using XmlStorage.XmlData;

namespace XmlStorage.Data
{
    internal sealed class Data
    {
        private readonly StringBuilder builder = new();
        private readonly Dictionary<string, DataElement> data = new();  // Key: key + "_" + valueType.FullName


        public void Update<T>(in string key, in T value, in Type valueType)
        {
            var globalKey = this.GetGlobalKey(key, valueType);
            if (this.TryGet(globalKey, out var element))
            {
                element.Value = value;
            }
            else
            {
                this.data.Add(globalKey, new DataElement(key, value, valueType));
            }
        }

        public IEnumerable<DataElement> GetDataElements()
        {
            return this.data.Values;
        }

        public bool TryGet(in string key, in Type valueType, out DataElement result)
        {
            return this.TryGet(this.GetGlobalKey(key, valueType), out result);
        }

        private bool TryGet(in string globalKey, out DataElement result)
        {
            return this.data.TryGetValue(globalKey, out result);
        }
        
        private string GetGlobalKey(in string key, in Type valueType)
        {
            this.builder.Clear();
            return this.builder.Append(key).Append("_").Append(valueType.FullName).ToString();
        }
        
        public void Merge(in XmlDataGroup other)
        {
            foreach (var e in other)
            {
                this.Update(e.Key, e.Value, e.ValueType);
            }
        }

        // public IEnumerator<(Type type, string key, object value)> GetEnumerator()
        // {
        //     foreach (var (type, data) in this.data)
        //     {
        //         foreach (var (key, value) in data)
        //         {
        //             yield return (type, key, value);
        //         }
        //     }
        // }
    }
}
