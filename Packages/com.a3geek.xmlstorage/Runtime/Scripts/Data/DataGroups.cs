using System.Collections.Generic;
using XmlStorage.XmlData;

namespace XmlStorage.Data
{
    internal sealed class DataGroups
    {
        private readonly Dictionary<string, DataGroup> groups = null; // Key: GroupName


        public DataGroups() : this(new Dictionary<string, DataGroup>()) { }

        public DataGroups(in Dictionary<string, DataGroup> groups)
        {
            this.groups = groups;
        }

        public DataGroups(in string filePath, in XmlDataGroups xmlDataGroups) : this()
        {
            foreach (var xmlDataGroup in xmlDataGroups)
            {
                this.groups[xmlDataGroup.GroupName] = new DataGroup(xmlDataGroup, filePath);
            }
        }

        public DataGroup Get(in string groupName)
        {
            if (this.groups.TryGetValue(groupName, out var group))
            {
                return group;
            }

            group = new DataGroup(groupName);
            this.groups[groupName] = group;

            return group;
        }
        
        public void Merge(in string filePath, in XmlDataGroups xmlDataGroups)
        {
            foreach (var xmlDataGroup in xmlDataGroups)
            {
                var dataGroup = new DataGroup(xmlDataGroup, filePath);
                
                if (this.groups.TryGetValue(xmlDataGroup.GroupName, out var group))
                {
                    group.GetData().Merge(dataGroup);
                }
                else
                {
                    this.groups[xmlDataGroup.GroupName] = dataGroup;
                }
            }
            //
            // foreach (var (groupName, dataGroup) in other)
            // {
            //     if (this.groups.TryGetValue(groupName, out var group))
            //     {
            //         group.GetData().Merge(dataGroup);
            //     }
            //     else
            //     {
            //         this.groups[groupName] = dataGroup;
            //     }
            // }
        }

        public void Merge(in DataGroups other)
        {
            foreach (var (groupName, dataGroup) in other)
            {
                if (this.groups.TryGetValue(groupName, out var group))
                {
                    group.GetData().Merge(dataGroup);
                }
                else
                {
                    this.groups[groupName] = dataGroup;
                }
            }
        }
        
        public IEnumerator<(string groupName, DataGroup dataGroup)> GetEnumerator()
        {
            foreach (var (groupName, dataGroup) in this.groups)
            {
                yield return (groupName, dataGroup);
            }
        }
    }
}
