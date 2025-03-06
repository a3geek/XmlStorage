using System;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    public sealed class XmlDataElementModel
    {
        public string Key = "";
        public object Value = null;
        public string TypeName = "";


        public XmlDataElementModel() { }

        internal XmlDataElementModel(XmlDataElement element)
        {
            this.Key = element.Key;
            this.Value = Serializer.Serialize(element.ValueType, element.Value);
            this.TypeName = element.ValueType.AssemblyQualifiedName;
        }
    }
}
