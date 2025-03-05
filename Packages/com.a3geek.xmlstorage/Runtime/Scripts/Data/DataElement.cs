using System;

namespace XmlStorage.Data
{
    internal sealed class DataElement
    {
        public readonly string Key = null;
        public object Value = null;
        public readonly Type ValueType = null;


        public DataElement(in string key, in object value, in Type valueType)
        {
            this.Key = key;
            this.Value = value;
            this.ValueType = valueType;
        }
    }
}
