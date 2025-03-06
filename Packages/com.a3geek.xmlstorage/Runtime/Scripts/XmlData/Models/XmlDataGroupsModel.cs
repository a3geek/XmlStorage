using System;
using System.Collections.Generic;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    public sealed class XmlDataGroupsModel
    {
        public const string XmlRootName = "ArrayOfDataGroup";

        public List<XmlDataGroupModel> Groups = new();
    }
}
