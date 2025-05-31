using System.IO;
using XmlStorage.Data;
using XmlStorage.Utilities;
using XmlStorage.XmlData;

namespace XmlStorage
{
    public static partial class Storage
    {
        private const string Extension = "xml";
        private const string LoadPattern = "*." + Extension;
        
        public static DataGroup CurrentDataGroup => GetDataGroups().GetGroup(CurrentDataGroupName);
        public static string[] DirectoryPaths
        {
            get => DirectoryPathsInternal;
            set
            {
                if ((value?.Length ?? -1) <= 0)
                {
                    return;
                }

                DirectoryPathsInternal = value;
            }
        }
        public static string CurrentDataGroupName { get; set; } = Const.DataGroupName;

        private static DataGroups DataGroupsInternal = null;
        private static string[] DirectoryPathsInternal = Const.SaveDirectoryPaths;


        public static void Save()
        {
            var directoryPath = DirectoryPaths[0];
            foreach (var group in GetDataGroups())
            {
                var path = new FilePath(directoryPath, group.GroupName, Extension);
                var xml = new XmlDataGroup(group);

                Serializer.Serialize(path.GetFullPath(), xml);
            }
        }

        public static void Load()
        {
            var groups = new DataGroups();
            var directoryPath = DirectoryPaths[0];

            var files = Directory.GetFiles(directoryPath, LoadPattern, SearchOption.TopDirectoryOnly);
            foreach (var path in files)
            {
                var xml = Serializer.Deserialize(path);
                xml?.LoadToDataGroup(groups.GetGroup(xml.GroupName));
            }
        }

        private static DataGroups GetDataGroups()
        {
            if (DataGroupsInternal == null)
            {
                Load();
            }

            return DataGroupsInternal;
        }
    }
}
