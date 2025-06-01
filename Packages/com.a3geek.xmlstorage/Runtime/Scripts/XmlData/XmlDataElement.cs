using System;
using XmlStorage.Data;
using XmlStorage.Utilities.CustomExtensions;

namespace XmlStorage.XmlData
{
    [Serializable]
    public class XmlDataElement
    {
        public string Key;
        public object Value;
        public string TypeName;


        public XmlDataElement() { }

        internal XmlDataElement(DataElement element)
        {
            this.Key = element.Key;
            this.Value = Serializer.Serialize(element.Value, element.ValueType);
            this.TypeName = element.ValueType.AssemblyQualifiedName;
        }

        internal bool TryGetDataElementTuple(out (string key, object value, Type valueType) tuple)
        {
            if (!this.TypeName.TryGetTypeAsTypeName(out var type))
            {
                tuple = default;
                return false;
            }

            tuple = (this.Key, Serializer.Deserialize(this.Value, type), type);
            return true;
        }
    }
}
