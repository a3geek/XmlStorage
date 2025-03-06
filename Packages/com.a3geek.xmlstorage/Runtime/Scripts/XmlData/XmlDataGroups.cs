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


        public XmlDataGroups(in DataGroups dataGroups)
        {
            this.Groups = dataGroups.Get().Select(e => new XmlDataGroup(e));
        }

        public XmlDataGroups(string filePath, in XmlDataGroupsModel model)
        {
            this.Groups = model.Groups.Select(e => new XmlDataGroup(filePath, e));
        }

        public IEnumerator<XmlDataGroup> GetEnumerator()
        {
            foreach (var group in this.Groups)
            {
                yield return group;
            }
        }

        public static void Save(in DataGroups dataGroups)
        {
            var xmlDataGroups = new XmlDataGroups(dataGroups);
            var dic = new Dictionary<FilePath, XmlDataGroupsModel>();

            foreach (var group in xmlDataGroups)
            {
                XmlDataGroupsModel model = null;
                foreach (var (filePath, xmlDataGroupsModel) in dic)
                {
                    if (!filePath.IsEquals(group.SaveFilePath))
                    {
                        continue;
                    }
                    
                    model = xmlDataGroupsModel;
                }

                if (model == null)
                {
                    model = new XmlDataGroupsModel();
                    dic[group.SaveFilePath] = model;
                }

                model.Groups.Add(new XmlDataGroupModel(group));
            }

            foreach (var (filePath, xmlDataGroupsModel) in dic)
            {
                if (!Directory.Exists(filePath.DirectoryPath))
                {
                    Directory.CreateDirectory(filePath.DirectoryPath);
                }
                
                Serializer.Serialize(
                    filePath.FullPath,
                    xmlDataGroupsModel
                );
            }
        }

        public static IEnumerable<XmlDataGroups> Load(in string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return Enumerable.Empty<XmlDataGroups>();
            }

            var dataGroups = new List<XmlDataGroups>();
            var filePaths = Directory.GetFiles(
                directoryPath, Const.FileSearchPattern, SearchOption.AllDirectories
            );
            foreach (var filePath in filePaths)
            {
                var model = Serializer.Deserialize(filePath);
                dataGroups.Add(new XmlDataGroups(filePath, model));
            }

            return dataGroups;
        }
    }
}
