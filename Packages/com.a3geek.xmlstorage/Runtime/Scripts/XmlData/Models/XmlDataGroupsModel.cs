using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    [XmlRoot(XmlRootName)]
    public sealed class XmlDataGroupsModel
    {
        public const string XmlRootName = "ArrayOfDataGroup";

        [XmlElement("DataGroup")]
        public List<XmlDataGroupModel> Groups = new();
    }
}
