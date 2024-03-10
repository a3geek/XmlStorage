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
    using XmlStorage.XmlData;
    using System.Runtime.CompilerServices;
    using System.Linq;

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
                    dataGroups.Select(dataGroup => DataGroup.ToXmlDataSet(dataGroup)).ToList()
                );
            }
        }

        public static void Load()
        {
            var groups = new Dictionary<string, DataGroup>();

            foreach(var path in DirectoryPaths)
            {
                foreach(var (filePath, datasets) in XmlDataSets.Load(path))
                {
                    foreach(var dataset in datasets)
                    {
                        var group = DataGroup.FromXmlDataSet(filePath, dataset);

                        if(groups.TryGetValue(group.GroupName, out var dataGroup))
                        {
                            dataGroup.Merge(group);
                        }
                        else
                        {
                            groups[group.GroupName] = group;
                        }
                    }
                }
            }

            DataGroups.Set(groups);
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
