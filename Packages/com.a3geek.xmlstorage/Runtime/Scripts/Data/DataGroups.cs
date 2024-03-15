using System.Collections.Generic;

namespace XmlStorage.Data
{
    using Utils.Extensions;

    internal class DataGroups
    {
        private Dictionary<string, DataGroup> dataGroups = null;


        public void Set(Dictionary<string, DataGroup> dataGroups)
        {
            this.dataGroups = dataGroups;
        }

        public DataGroup Get(in string groupName)
        {
            if(!this.dataGroups.TryGetValue(groupName, out var group))
            {
                group = new DataGroup(groupName);
                this.dataGroups[groupName] = group;
            }

            return group;
        }

        public Dictionary<string, DataGroup> Get()
        {
            if(dataGroups == null)
            {
                Storage.Load();
            }

            return dataGroups;
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
