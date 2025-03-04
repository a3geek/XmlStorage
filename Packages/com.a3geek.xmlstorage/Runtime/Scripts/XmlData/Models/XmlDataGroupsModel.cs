using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    [XmlRoot(XmlRootName)]
    internal sealed class XmlDataGroupsModel
    {
        public const string XmlRootName = "ArrayOfDataGroup";

        [XmlElement("DataGroup")]
        public List<XmlDataGroupModel> Groups = new();
        

        public XmlDataGroupsModel() { }
        
        public XmlDataGroupsModel(in IEnumerable<XmlDataGroup> dataGroups)
        {
            foreach (var dataGroup in dataGroups)
            {
                this.Groups.Add(new XmlDataGroupModel(
                    dataGroup.GroupName, dataGroup.Elements
                ));
            }
        }
    }
}
