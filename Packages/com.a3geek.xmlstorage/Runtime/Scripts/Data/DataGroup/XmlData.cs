using System.Collections.Generic;
using System;

namespace XmlStorage.Data
{
    using XmlData;
    using XmlStorage.Utils.Extensions;
    using Data = Dictionary<Type, Dictionary<string, object>>;

    public partial class DataGroup
    {
        private List<XmlDataElement> GetXmlDataElements()
        {
            var elements = new List<XmlDataElement>();

            foreach(var (type, data) in this.data)
            {
                foreach(var (key, value) in data)
                {
                    elements.Add(new XmlDataElement(
                        key, Serializer.Serialize(type, value), type
                    ));
                }
            }

            return elements;
        }

        public static XmlDataSet ToXmlDataSet(DataGroup group)
        {
            return new XmlDataSet(group.GroupName, group.GetXmlDataElements());
        }

        public static DataGroup FromXmlDataSet(in XmlDataSet dataset, string fileName)
        {
            var data = new Data();
            foreach(var e in dataset.Elements)
            {
                if(e.Type == null)
                {
                    continue;
                }

                var d = data.GetOrAdd(e.Type);
                d[e.Key] = Serializer.Deserialize(e.Type, e.Value);
            }

            return new DataGroup(dataset.GroupName, data, fileName);
        }
    }
}
