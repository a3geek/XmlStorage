using System.Collections.Generic;
using System.Linq;
using XmlStorage.Data;
using XmlStorage.XmlData;

namespace XmlStorage
{
    internal static class XmlDataCoordinator
    {
        public static XmlDataGroups ToXmlDataGroups(in List<DataGroup> groups)
        {
            return new XmlDataGroups(groups.Select(group => ToXmlDataGroup(group)));
        }

        public static XmlDataGroup ToXmlDataGroup(in DataGroup group)
        {
            return new XmlDataGroup(group.GroupName, GetXmlDataElements(group));
        }

        private static List<XmlDataElement> GetXmlDataElements(in DataGroup group)
        {
            var elements = new List<XmlDataElement>();

            foreach (var (type, key, value) in group.GetData())
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
