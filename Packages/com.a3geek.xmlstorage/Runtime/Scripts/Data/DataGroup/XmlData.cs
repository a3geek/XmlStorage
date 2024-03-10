using System;
using System.Collections.Generic;

namespace XmlStorage.Data
{
    using XmlData;
    using XmlStorage.Utils.Extensions;

    public partial class DataGroup
    {
        private List<XmlDataElement> GetXmlDataElements()
        {
            var elements = new List<XmlDataElement>();

            foreach(var (type, key, value) in this)
            {
                elements.Add(new XmlDataElement(
                    key, Serializer.Serialize(type, value), type
                ));
            }
            
            return elements;
        }

        public static XmlDataSet ToXmlDataSet(DataGroup group)
        {
            return new XmlDataSet(group.GroupName, group.GetXmlDataElements());
        }

        public static DataGroup FromXmlDataSet(string filePath, in XmlDataSet dataset)
        {
            var data = new Dictionary<Type, Dictionary<string, object>>();
            foreach(var e in dataset.Elements)
            {
                if(e.Type == null)
                {
                    continue;
                }

                var d = data.GetOrAdd(e.Type);
                d[e.Key] = Serializer.Deserialize(e.Type, e.Value);
            }

            return new DataGroup(dataset.GroupName, filePath, data);
        }
    }
}
