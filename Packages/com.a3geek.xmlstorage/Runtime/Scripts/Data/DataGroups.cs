using System.Collections.Generic;

namespace XmlStorage.Data
{
    using System.Xml.Linq;
    using Utils.Extensions;

    public class DataGroups
    {
        private Dictionary<string, DataGroup> dataGroups = null;


        public void Set(Dictionary<string, DataGroup> dataGroups)
        {
            this.dataGroups = dataGroups;

            UnityEngine.Debug.Log(dataGroups.Count);
            foreach (var (name, group) in dataGroups)
            {
                UnityEngine.Debug.Log(name);
                UnityEngine.Debug.Log(group.FullPath);
                foreach(var (type, key, value) in group)
                {
                    UnityEngine.Debug.Log($"{type} {key} {value}");
                }
                UnityEngine.Debug.Log("");
            }
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
