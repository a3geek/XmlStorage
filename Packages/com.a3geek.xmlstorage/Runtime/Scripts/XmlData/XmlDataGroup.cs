using System.Collections.Generic;
using System.Linq;
using XmlStorage.Data;
using XmlStorage.XmlData.Models;

namespace XmlStorage.XmlData
{
    internal sealed class XmlDataGroup
    {
        public readonly string GroupName = null;
        public readonly IEnumerable<XmlDataElement> Elements = null;


        public XmlDataGroup(in DataGroup group)
        {
            this.GroupName = group.GroupName;
            this.Elements = group
                .GetData()
                .GetDataElements()
                .Select(e => new XmlDataElement(e));
        }

        public XmlDataGroup(in XmlDataGroupModel model)
        {
            this.GroupName = model.GroupName;
            this.Elements = model.Elements
                .Select(e => new XmlDataElement(e))
                .Where(e => e.ValueType != null);
        }

        public IEnumerator<XmlDataElement> GetEnumerator()
        {
            foreach (var element in this.Elements)
            {
                yield return element;
            }
        }
    }
}
