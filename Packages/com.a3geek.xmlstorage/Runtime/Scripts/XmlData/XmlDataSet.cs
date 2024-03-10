using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    [Serializable]
    public sealed class XmlDataSet
    {
        [XmlElement("AggregationName")]
        public string GroupName { get; set; } = "";
        [XmlArray("Elements"), XmlArrayItem("DataElement")]
        public List<XmlDataElement> Elements { get; set; } = new();


        public XmlDataSet() { }

        public XmlDataSet(string groupName, List<XmlDataElement> elements)
        {
            this.GroupName = groupName;
            this.Elements = elements ?? new();
        }
    }
}
