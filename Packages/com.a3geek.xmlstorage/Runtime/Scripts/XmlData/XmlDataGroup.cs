using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    [Serializable]
    [XmlRoot("DataGroup")]
    internal sealed class XmlDataGroup
    {
        [XmlElement("GroupName")]
        public string GroupName = "";
        [XmlArray("Elements"), XmlArrayItem("DataElement")]
        public List<XmlDataElement> Elements = new();


        public XmlDataGroup() { }

        public XmlDataGroup(in string groupName, in List<XmlDataElement> elements)
        {
            this.GroupName = groupName;
            this.Elements = elements ?? new List<XmlDataElement>();
        }
    }
}
