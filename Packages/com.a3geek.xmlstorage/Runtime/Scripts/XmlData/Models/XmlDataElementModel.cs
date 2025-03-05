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
    }
}
