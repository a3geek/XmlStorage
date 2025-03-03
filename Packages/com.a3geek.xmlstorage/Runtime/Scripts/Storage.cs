using System.Collections.Generic;
using XmlStorage.Data;
using XmlStorage.Utils;
using XmlStorage.XmlData;

namespace XmlStorage
{
    public static partial class Storage
    {
        public static DataGroup CurrentDataGroup => DataGroups.Get(CurrentDataGroupName);
        public static string[] DirectoryPaths
        {
            get => DirectoryPathsInternal;
            set
            {
                if((value?.Length ?? -1) <= 0)
                {
                    return;
                }

                DirectoryPathsInternal = value;
            }
        }
        public static string CurrentDataGroupName { get; private set; } = Const.DataGroupName;

        private static readonly DataGroups DataGroups = new();
        private static string[] DirectoryPathsInternal = Const.SaveDirectoryPaths;


        public static void Save()
        {
            var fileGroups = DataGroups.GetFileGroups();
            foreach(var (filePath, dataGroups) in fileGroups)
            {
                Serializer.Serialize(
                    filePath,
                    XmlDataCoordinator.ToXmlDataGroups(dataGroups)
                );
            }
        }

        public static void Load()
        {
            var dataGroups = new Dictionary<string, DataGroup>();
            foreach(var path in DirectoryPaths)
            {
                foreach(var (filePath, datasets) in XmlDataGroups.Load(path))
                {
                    var groups = XmlDataCoordinator.FromXmlDataSets(filePath, datasets);
                    Merge(dataGroups, groups);
                }
            }

            DataGroups.Set(dataGroups);
        }

        private static void Merge(Dictionary<string, DataGroup> dataGroups, in List<DataGroup> groups)
        {
            foreach(var group in groups)
            {
                if(dataGroups.TryGetValue(group.GroupName, out var dataGroup))
                {
                    dataGroup.GetData().Merge(group.GetData());
                }
                else
                {
                    dataGroups[group.GroupName] = group;
                }
            }
        }
    }
}

