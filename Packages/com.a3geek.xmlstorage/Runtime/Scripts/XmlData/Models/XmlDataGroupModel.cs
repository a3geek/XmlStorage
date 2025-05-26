using System;

namespace XmlStorage.XmlData.Models
{
    [Serializable]
    public class XmlDataGroupModel
    {
        public string GroupName;
        public XmlDataElementModel[] Elements;

        
        public XmlDataGroupModel() : this(string.Empty, null) { }

        internal XmlDataGroupModel(string groupName, XmlDataElementModel[] elements)
        {
            this.GroupName = groupName;
            this.Elements = elements ?? Array.Empty<XmlDataElementModel>();
        }
    }
}
