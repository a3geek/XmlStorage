using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    [Serializable]
    [XmlRoot("DataSet")]
    public sealed class XmlDataSet
    {
        [XmlElement("AggregationName")]
        public string GroupName = "";
        [XmlArray("Elements"), XmlArrayItem("DataElement")]
        public List<XmlDataElement> Elements = new();


        public XmlDataSet() { }

        public XmlDataSet(in string groupName, in List<XmlDataElement> elements)
        {
            this.GroupName = groupName;
            this.Elements = elements ?? new();
        }
    }
}
