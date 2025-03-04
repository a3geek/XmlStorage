using System;
using XmlStorage.Utils.Extensions;
using XmlStorage.XmlData.Models;

namespace XmlStorage.XmlData
{
    internal sealed class XmlDataElement
    {
        public readonly string Key = null;
        public readonly object Value = null;
        public readonly Type ValueType = null;


        public XmlDataElement(in XmlDataElementModel model)
        {
            this.Key = model.Key;

            if (model.TypeName.TryGetTypeAsTypeName(out this.ValueType))
            {
                this.Value = Serializer.Deserialize(this.ValueType, model.Value);
            }
        }
    }
}
