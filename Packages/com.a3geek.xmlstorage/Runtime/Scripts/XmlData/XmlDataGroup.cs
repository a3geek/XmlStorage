using System.Collections.Generic;
using System.Linq;
using XmlStorage.Data;
using XmlStorage.XmlData.Models;

namespace XmlStorage.XmlData
{
    internal sealed class XmlDataGroup
    {
        public readonly string GroupName = null;
        public readonly FilePath SaveFilePath = null;
        public readonly IEnumerable<XmlDataElement> Elements = null;


        public XmlDataGroup(in DataGroup group)
        {
            this.GroupName = group.GroupName;
            this.SaveFilePath = group.SaveFilePath;
            this.Elements = group
                .GetData()
                .GetElements()
                .Select(e => new XmlDataElement(e));
        }

        public XmlDataGroup(in string filePath, in XmlDataGroupModel model)
        {
            this.GroupName = model.GroupName;
            this.SaveFilePath = new FilePath(filePath);
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
