using System;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    public class XmlDataElementModel
    {
        public string Key;
        public object Value;
        public string TypeName;


        public XmlDataElementModel() : this(string.Empty, null, null) { }

        internal XmlDataElementModel(string key, object value, Type valueType)
        {
            this.Key = key;
            this.Value = value;
            this.TypeName = valueType?.AssemblyQualifiedName ?? string.Empty;
        }
    }
}
