using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlStorage
{
    using Systems;
    using Systems.Aggregations;
    using Systems.Utilities;
    using Data;
    using Utils;
    using Utils.Extensions;
    using XmlData;

    public static partial class Storage
    {
        public static string[] DirectoryPaths
        {
            get => DirectoryPathsInternal;
            set
            {
                if(value == null || value.Length <= 0)
                {
                    return;
                }

                DirectoryPathsInternal = value;
            }
        }
        public static string CurrentDataGroupName { get; private set; } = Consts.DataGroupName;

        private static readonly DataGroups DataGroups = new();
        private static string[] DirectoryPathsInternal = Consts.SaveDirectoryPaths;


        public static void Save()
        {
            var fileGroups = DataGroups.GetFileGroups();
            foreach(var (filePath, dataGroups) in fileGroups)
            {
                Serializer.Serialize(
                    filePath,
                    DataGroup.ToXmlDataSets(dataGroups)
                );
            }
        }

        public static void Load()
        {
            var dataGroups = new Dictionary<string, DataGroup>();
            foreach(var path in DirectoryPaths)
            {
                foreach(var (filePath, datasets) in XmlDataSets.Load(path))
                {
                    var groups = DataGroup.FromXmlDataSets(filePath, datasets);
                    Merge(dataGroups, groups);
                }
            }

            foreach(var (name, group) in dataGroups)
            {
                UnityEngine.Debug.Log($"{name} : {group.GroupName}, {group.FileName}, {group.FullPath}");
                foreach(var (type, key, value) in group)
                {
                    UnityEngine.Debug.Log($"{type}, {key}, {value}");
                }
                UnityEngine.Debug.Log("");
            }

            DataGroups.Set(dataGroups);
        }

        private static void Merge(Dictionary<string, DataGroup> dataGroups, List<DataGroup> groups)
        {
            foreach(var group in groups)
            {
                if(dataGroups.TryGetValue(group.GroupName, out var dataGroup))
                {
                    dataGroup.Merge(group);
                }
                else
                {
                    dataGroups[group.GroupName] = group;
                }
            }
        }

        //private static void Action(string aggregationName, Action<Aggregation> action)
        //{
        //    Func(aggregationName, agg =>
        //    {
        //        action(agg);
        //        return true;
        //    });
        //}

        //private static T Func<T>(string aggregationName, Func<Aggregation, T> func)
        //{
        //    return HasAggregation(aggregationName) == true ?
        //        func(Aggregations[aggregationName]) :
        //        func(CurrentAggregation);
        //}
    }
}
