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

        public IEnumerable<DataGroup> Get()
        {
            return this.groups.Values;
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
        
        public void Merge(in XmlDataGroups xmlDataGroups)
        {
            foreach (var xmlDataGroup in xmlDataGroups)
            {
                if (this.groups.TryGetValue(xmlDataGroup.GroupName, out var group))
                {
                    group.GetData().Merge(xmlDataGroup);
                }
                else
                {
                    this.groups[xmlDataGroup.GroupName] = new DataGroup(xmlDataGroup);
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
