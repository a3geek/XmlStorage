using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    [XmlRoot("DataGroup")]
    public sealed class XmlDataGroupModel
    {
        [XmlElement("GroupName")]
        public string GroupName = "";
        [XmlArray("Elements"), XmlArrayItem("DataElement")]
        public List<XmlDataElementModel> Elements = new();


        public XmlDataGroupModel() { }

        internal XmlDataGroupModel(in string groupName, in IEnumerable<XmlDataElement> elements)
        {
            this.GroupName = groupName;
            
            foreach (var e in elements)
            {
                this.Elements.Add(new XmlDataElementModel(e));
            }
        }
    }
}
