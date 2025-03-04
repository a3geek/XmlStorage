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
            return null;
            // return new XmlDataGroups(groups.Select(group => ToXmlDataGroup(group)));
        }
    }
}
