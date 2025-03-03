using System.Collections.Generic;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Data
{
    internal class DataGroups
    {
        private Dictionary<string, DataGroup> groups = new();


        public void Set(in Dictionary<string, DataGroup> groups)
        {
            this.groups = groups;
        }

        public DataGroup Get(in string groupName)
        {
            if(this.groups.TryGetValue(groupName, out var group))
            {
                return group;
            }
            
            group = new DataGroup(groupName);
            this.groups[groupName] = group;
            
            return group;
        }

        public Dictionary<string, DataGroup> Get()
        {
            if(this.groups == null)
            {
                Storage.Load();
            }

            return this.groups;
        }

        public Dictionary<string, List<DataGroup>> GetFileGroups()
        {
            var dataGroups = this.Get();
            var fileGroups = new Dictionary<string, List<DataGroup>>();

            foreach(var (_, dataGroup) in dataGroups)
            {
                var list = fileGroups.GetOrAdd(dataGroup.FullPath);
                list.Add(dataGroup);
            }

            return fileGroups;
        }

        public IEnumerator<(string groupName, DataGroup dataGroup)> GetEnumerator()
        {
            var groups = this.Get();
            foreach(var (groupName, dataGroup) in groups)
            {
                yield return (groupName, dataGroup);
            }
        }
    }
}
