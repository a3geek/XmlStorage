using System;
using System.Collections.Generic;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    public sealed class XmlDataGroupModel
    {
        public string GroupName = "";
        public List<XmlDataElementModel> Elements = new();


        public XmlDataGroupModel() { }
        
        internal XmlDataGroupModel(XmlDataGroup group)
        {
            this.GroupName = group.GroupName;
            
            foreach (var e in group.Elements)
            {
                this.Elements.Add(new XmlDataElementModel(e));
            }
        }

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
