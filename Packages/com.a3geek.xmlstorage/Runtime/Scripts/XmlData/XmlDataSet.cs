using System;
using System.Collections.Generic;

namespace XmlStorage.XmlData
{
    [Serializable]
    public sealed class XmlDataSet
    {
        public string GroupName { get; set; } = "";
        public List<XmlDataElement> Elements { get; set; } = new();


        public XmlDataSet() { }

        public XmlDataSet(string groupName, List<XmlDataElement> elements)
        {
            this.GroupName = groupName;
            this.Elements = elements ?? new();
        }
    }
}
