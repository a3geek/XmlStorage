﻿using System.IO;
using XmlStorage.Data;
using XmlStorage.XmlData;

namespace XmlStorage
{
    public static partial class Storage
    {
        internal const string Extension = "xml";
        private const string LoadPattern = "*." + Extension;
        private const string DefaultDataGroupName = "Prefs";

        public static string CurrentDataGroupName { get; set; } = DefaultDataGroupName;
        public static DataGroup CurrentDataGroup => GetCurrentDataGroup(CurrentDataGroupName);
        public static string DirectoryPath { get; set; } = GetDefaultDirectoryPath();

        private static DataGroups DataGroups
        {
            get
            {
                if (DataGroupsInternal == null)
                {
                    Load();
                }

                return DataGroupsInternal;
            }
        }
        private static DataGroups DataGroupsInternal = null;


        public static void Save()
        {
            foreach (var group in DataGroups)
            {
                var xml = new XmlDataGroup(group);
                Serializer.Serialize(group.FilePath.GetFullPath(), xml);
            }
        }

        public static void Load()
        {
            var groups = new DataGroups();

            if (Directory.Exists(DirectoryPath))
            {
                var files = Directory.GetFiles(DirectoryPath, LoadPattern, SearchOption.TopDirectoryOnly);
                foreach (var path in files)
                {
                    var xml = Serializer.Deserialize(path);
                    xml?.LoadToDataGroup(groups.GetGroup(xml.GroupName));
                }
            }

            DataGroupsInternal = groups;
        }

        public static string GetDefaultDirectoryPath()
        {
        #if UNITY_EDITOR
            return Directory.GetCurrentDirectory();
        #else
            return System.AppDomain.CurrentDomain.BaseDirectory;
        #endif
        }

        private static DataGroup GetCurrentDataGroup(string groupName)
        {
            return DataGroups.GetGroup(string.IsNullOrEmpty(groupName) ? DefaultDataGroupName : groupName);
        }
    }
}
