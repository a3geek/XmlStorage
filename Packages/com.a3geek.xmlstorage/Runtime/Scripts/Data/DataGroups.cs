using System.Collections.Generic;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Data
{
    internal class DataGroups
    {
        private Dictionary<string, DataGroup> dataGroups = new();


        public void Set(in Dictionary<string, DataGroup> dataGroups)
        {
            this.dataGroups = dataGroups;
        }

        public DataGroup Get(in string groupName)
        {
            if(this.dataGroups.TryGetValue(groupName, out var group))
            {
                return group;
            }
            
            group = new DataGroup(groupName);
            this.dataGroups[groupName] = group;
            
            return group;
        }

        public Dictionary<string, DataGroup> Get()
        {
            if(this.dataGroups == null)
            {
                Storage.Load();
            }

            return this.dataGroups;
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
            var dataGroups = this.Get();
            foreach(var (groupName, dataGroup) in dataGroups)
            {
                yield return (groupName, dataGroup);
            }
        }
    }
}
