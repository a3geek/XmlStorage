using System;
using System.Xml.Serialization;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    internal sealed class XmlDataElementModel
    {
        [XmlElement("Key")]
        public string Key = "";
        [XmlElement("Value")]
        public object Value = null;
        [XmlElement("TypeName")]
        public string TypeName = "";


        public XmlDataElementModel() { }

        public XmlDataElementModel(XmlDataElement element)
        {
            this.Key = element.Key;
            this.Value = element.Value;
            this.TypeName = element.ValueType.FullName;
        }

        // public XmlDataElementModel(in string key, in object value, in Type type)
        // {
        //     if (string.IsNullOrEmpty(key))
        //     {
        //         throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");
        //     }
        //     if (type == null)
        //     {
        //         throw new ArgumentNullException(nameof(type), "Type cannot be null.");
        //     }
        //
        //     this.Key = key;
        //     this.Value = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null.");
        //     this.TypeName = type.FullName;
        // }
    }
}
