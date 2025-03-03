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
        internal static XmlDataGroups ToXmlDataGroups(in List<DataGroup> groups)
        {
            return new XmlDataGroups(groups.Select(group => ToXmlDataGroup(group)));
        }

        internal static XmlDataGroup ToXmlDataGroup(in DataGroup group)
        {
            return new XmlDataGroup(group.GroupName, GetXmlDataElements(group));
        }

        internal static List<DataGroup> FromXmlDataSets(in string filePath, in XmlDataGroups datasets)
        {
            var groups = new List<DataGroup>();
            foreach(var dataset in datasets)
            {
                groups.Add(FromXmlDataGroup(filePath, dataset));
            }

            return groups;
        }

        internal static DataGroup FromXmlDataGroup(in string filePath, in XmlDataGroup dataGroup)
        {
            var data = new Dictionary<Type, Dictionary<string, object>>();
            foreach(var e in dataGroup.Elements)
            {
                if(e.ValueType == null)
                {
                    continue;
                }

                var d = data.GetOrAdd(e.ValueType);
                d[e.Key] = Serializer.Deserialize(e.ValueType, e.Value);
            }

            return new DataGroup(dataGroup.GroupName, filePath, data);
        }

        private static List<XmlDataElement> GetXmlDataElements(in DataGroup group)
        {
            var elements = new List<XmlDataElement>();

            foreach(var (type, key, value) in group.GetData())
            {
                elements.Add(new XmlDataElement(
                        key, Serializer.Serialize(type, value), type
                    )
                );
            }

            return elements;
        }
    }
}
