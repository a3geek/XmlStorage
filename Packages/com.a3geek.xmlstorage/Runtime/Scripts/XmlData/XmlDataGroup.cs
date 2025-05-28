using System;
using XmlStorage.Data;

namespace XmlStorage.XmlData
{
    [Serializable]
    public class XmlDataGroup
    {
        public string GroupName;
        public XmlDataElement[] Elements;


        public XmlDataGroup() { }

        internal XmlDataGroup(DataGroup group)
        {
            this.GroupName = group.GroupName;

            var dataElements = group.GetData().GetElements();

            var cnt = 0;
            var elements = new XmlDataElement[dataElements.Count];
            foreach (var dataElement in dataElements)
            {
                elements[cnt++] = new XmlDataElement(dataElement);
            }

            this.Elements = elements;
        }

        internal DataGroup ToDataGroup()
        {
            var group = new DataGroup(this.GroupName);

            var data = group.GetData();
            foreach (var e in this.Elements)
            {
                if (e.TryGetDataElementTuple(out var tuple))
                {
                    data.Update(tuple.key, tuple.value, tuple.valueType);
                }
            }

            return group;
        }
    }
}
