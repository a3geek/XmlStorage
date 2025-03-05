using System;
using System.Xml.Serialization;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    public sealed class XmlDataElementModel
    {
        [XmlElement("Key")]
        public string Key = "";
        [XmlElement("Value")]
        public object Value = null;
        [XmlElement("TypeName")]
        public string TypeName = "";


        public XmlDataElementModel() { }

        internal XmlDataElementModel(XmlDataElement element)
        {
            this.Key = element.Key;
            this.Value = Serializer.Serialize(element.ValueType, element.Value);
            this.TypeName = element.ValueType.FullName;
        }
    }
}
