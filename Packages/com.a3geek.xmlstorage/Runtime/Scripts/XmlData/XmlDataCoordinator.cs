using System;
using System.Collections.Generic;
using System.Linq;
using XmlStorage.Data;
using XmlStorage.Utils.Extensions;
using XmlStorage.XmlData;

namespace XmlStorage
{
    internal static class XmlDataCoordinator
    {
        internal static XmlDataGroups ToXmlDataSets(in List<DataGroup> groups)
        {
            return new XmlDataGroups(groups.Select(group => ToXmlDataSet(group)));
        }

        internal static XmlDataGroup ToXmlDataSet(in DataGroup group)
        {
            return new XmlDataGroup(group.GroupName, GetXmlDataElements(group));
        }

        internal static List<DataGroup> FromXmlDataSets(in string filePath, in XmlDataGroups datasets)
        {
            var groups = new List<DataGroup>();
            foreach(var dataset in datasets)
            {
                groups.Add(FromXmlDataSet(filePath, dataset));
            }

            return groups;
        }

        internal static DataGroup FromXmlDataSet(in string filePath, in XmlDataGroup dataset)
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
        
        private static List<XmlDataElement> GetXmlDataElements(in DataGroup group)
        {
            var elements = new List<XmlDataElement>();

            foreach(var (type, key, value) in group.GetData())
            {
                elements.Add(new XmlDataElement(
                    key, Serializer.Serialize(type, value), type
                ));
            }
            
            return elements;
        }
    }
}
