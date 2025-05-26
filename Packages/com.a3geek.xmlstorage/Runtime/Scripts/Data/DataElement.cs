using System;

namespace XmlStorage.Data
{
    internal sealed class DataElement
    {
        public readonly string Key = null;
        public object Value = null;
        public readonly Type ValueType = null;


        public DataElement(string key, object value, Type valueType)
        {
            this.Key = key;
            this.Value = value;
            this.ValueType = valueType;
        }
    }
}
