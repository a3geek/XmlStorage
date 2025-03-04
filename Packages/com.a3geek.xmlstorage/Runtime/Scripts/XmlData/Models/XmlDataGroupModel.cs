using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    [XmlRoot("DataGroup")]
    internal sealed class XmlDataGroupModel
    {
        [XmlElement("GroupName")]
        public string GroupName = "";
        [XmlArray("Elements"), XmlArrayItem("DataElement")]
        public List<XmlDataElementModel> Elements = new();


        public XmlDataGroupModel() { }

        public XmlDataGroupModel(in string groupName, in List<XmlDataElementModel> elements)
        {
            this.GroupName = groupName;
            this.Elements = elements;
        }
    }
}
