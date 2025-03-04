using System.Collections.Generic;
using System.IO;
using System.Linq;
using XmlStorage.Utils;
using XmlStorage.XmlData;
using XmlStorage.XmlData.Models;

namespace XmlStorage
{
    internal sealed class XmlDataGroups
    {
        private readonly IEnumerable<XmlDataGroup> Groups = null;


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
                dataGroups.Add((filePath, new XmlDataGroups(Serializer.Deserialize(filePath))));
            }

            return dataGroups;
        }
    }
}
