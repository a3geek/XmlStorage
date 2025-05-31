using System;
using System.Collections.Generic;
using System.Text;

namespace XmlStorage.Data
{
    internal sealed class Data
    {
        private const char Connector = '_';

        private readonly StringBuilder builder = new();
        private readonly Dictionary<string, DataElement> data = new(); // Key: key + "_" + valueType.FullName


        public void Update<T>(string key, T value, Type valueType)
        {
            var globalKey = this.GetGlobalKey(key, valueType);
            if (this.TryGet(globalKey, out var element))
            {
                element.Value = value;
            }
            else
            {
                this.data[globalKey] = new DataElement(key, value, valueType);
            }
        }

        public Dictionary<string, DataElement>.ValueCollection GetElements()
        {
            return this.data.Values;
        }

        public bool TryGet(string key, Type valueType, out DataElement result)
        {
            return this.TryGet(this.GetGlobalKey(key, valueType), out result);
        }

        public bool Delete(string key, Type valueType)
        {
            var globalKey = this.GetGlobalKey(key, valueType);
            return this.data.Remove(globalKey);
        }

        public void DeleteAll()
        {
            this.data.Clear();
        }

        private bool TryGet(string globalKey, out DataElement result)
        {
            return this.data.TryGetValue(globalKey, out result);
        }

        private string GetGlobalKey(string key, Type valueType)
        {
            this.builder.Clear();
            return this.builder
                .Append(key)
                .Append(Connector)
                .Append(valueType.FullName)
                .ToString();
        }
    }
}
