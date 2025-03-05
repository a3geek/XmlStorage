using System.Collections.Generic;
using System.IO;
using System.Linq;
using XmlStorage.Data;
using XmlStorage.Utils;
using XmlStorage.XmlData.Models;

namespace XmlStorage.XmlData
{
    internal sealed class XmlDataGroups
    {
        private readonly IEnumerable<XmlDataGroup> Groups = null;


        public XmlDataGroups(in IEnumerable<DataGroup> dataGroups)
        {
            this.Groups = dataGroups.Select(e => new XmlDataGroup(e));
        }

        public XmlDataGroups(in XmlDataGroupsModel model)
        {
            this.Groups = model.Groups.Select(e => new XmlDataGroup(e));
        }

        public IEnumerator<XmlDataGroup> GetEnumerator()
        {
            foreach (var group in this.Groups)
            {
                yield return group;
            }
        }

        public static void Save(in string fullPath, in List<DataGroup> dataGroups)
        {
            var group = new XmlDataGroups(dataGroups);
            Serializer.Serialize(
                fullPath,
                new XmlDataGroupsModel(group.Groups)
            );
        }

        public static List<(string filePath, XmlDataGroups dataGroups)> Load(in string directoryPath)
        {
            var dataGroups = new List<(string filePath, XmlDataGroups dataGroups)>();
            if (!Directory.Exists(directoryPath))
            {
                return dataGroups;
            }

            var filePaths = Directory.GetFiles(
                directoryPath, Const.FileSearchPattern, SearchOption.AllDirectories
            );
            foreach (var filePath in filePaths)
            {
                var model = Serializer.Deserialize(filePath);
                dataGroups.Add((filePath, new XmlDataGroups(model)));
            }

            return dataGroups;
        }
    }
}
