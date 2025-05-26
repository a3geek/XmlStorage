using XmlStorage.Data;
using XmlStorage.Utils.Extensions;
using XmlStorage.XmlData.Models;

namespace XmlStorage.XmlData
{
    internal sealed class XmlDataElement : XmlDataElementModel
    {
        public XmlDataElement(in DataElement element) : base(element.Key, element.Value, element.ValueType) { }

        public XmlDataElement(in XmlDataElementModel model)
        {
            this.Key = model.Key;
            this.TypeName = model.TypeName;

            this.Value = model.TypeName.TryGetTypeAsTypeName(out var valurType)
                ? Serializer.Deserialize(valurType, model.Value)
                : null;
        }
    }
}
