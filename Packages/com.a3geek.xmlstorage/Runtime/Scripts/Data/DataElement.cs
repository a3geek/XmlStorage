using System;

namespace XmlStorage.Data
{
    internal sealed class DataElement
    {
        public readonly string Key;
        public object Value;
        public readonly Type ValueType;


        public DataElement(string key, object value, Type valueType)
        {
            this.Key = key;
            this.Value = value;
            this.ValueType = valueType;
        }
    }
}
