using System.Collections.Generic;

namespace XmlStorage.Data
{
    internal sealed class DataGroups
    {
        private const string DefaultGroupName = nameof(XmlStorage);

        private readonly Dictionary<string, DataGroup> groups = new(); // Key: GroupName

        
        public DataGroup GetGroup(string groupName)
        {
            if (string.IsNullOrEmpty(groupName))
            {
                return this.GetGroup(DefaultGroupName);
            }

            if (this.groups.TryGetValue(groupName, out var group))
            {
                return group;
            }

            group = new DataGroup(groupName);
            this.groups[groupName] = group;

            return group;
        }

        // public void Merge(in XmlDataGroups xmlDataGroups)
        // {
        //     foreach (var xmlDataGroup in xmlDataGroups)
        //     {
        //         if (this.groups.TryGetValue(xmlDataGroup.GroupName, out var group))
        //         {
        //             group.GetData().Merge(xmlDataGroup);
        //         }
        //         else
        //         {
        //             this.groups[xmlDataGroup.GroupName] = new DataGroup(xmlDataGroup);
        //         }
        //     }
        // }

        public IEnumerator<DataGroup> GetEnumerator()
        {
            foreach (var (_, dataGroup) in this.groups)
            {
                yield return dataGroup;
            }
        }
    }
}
