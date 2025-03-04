using System.Collections.Generic;
using XmlStorage.Data;
using XmlStorage.Utils;
using XmlStorage.Utils.Extensions;
using XmlStorage.XmlData;

namespace XmlStorage
{
    public static partial class Storage
    {
        public static DataGroup CurrentDataGroup => GetDataGroups().Get(CurrentDataGroupName);
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
        public static string CurrentDataGroupName { get; private set; } = Const.DataGroupName;

        private static DataGroups DataGroupsInternal = null;
        private static string[] DirectoryPathsInternal = Const.SaveDirectoryPaths;


        public static void Save()
        {
            var dic = new Dictionary<string, List<DataGroup>>();
            foreach (var (_, dataGroup) in GetDataGroups())
            {
                var list = dic.GetOrAdd(dataGroup.FullPath);
                list.Add(dataGroup);
            }

            foreach (var (fullPath, dataGroups) in dic)
            {
                Serializer.Serialize(
                    fullPath,
                    XmlDataCoordinator.ToXmlDataGroups(dataGroups)
                );
            }
        }

        public static void Load()
        {
            var dataGroups = new DataGroups();
            foreach (var path in DirectoryPaths)
            {
                foreach (var (filePath, xmlDataGroups) in XmlDataGroups.Load(path))
                {
                    dataGroups.Merge(filePath, xmlDataGroups);
                }
            }

            DataGroupsInternal = dataGroups;
        }

        internal static DataGroups GetDataGroups()
        {
            if (DataGroupsInternal == null)
            {
                Load();
            }

            return DataGroupsInternal;
        }
    }
}
