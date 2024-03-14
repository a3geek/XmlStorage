using System;
using System.Collections.Generic;

namespace XmlStorage.Data
{
    using System.IO;
    using Utils;
    using Utils.Extensions;
    using Data = Dictionary<Type, Dictionary<string, object>>;

    /// <remarks>DataGroupはXmlStorage外からのアクセスを許可する</remarks>
    public partial class DataGroup
    {
        public string FileName
        {
            get => this.fileName;
            set
            {
                var name = value.AdjustAsFileName();
                this.fileName = string.IsNullOrEmpty(name) ? this.FileName : name;
            }
        }
        public string GroupName { get; } = "";
        public string FullPath => this.directoryPath + this.FileName;

        internal string fileName = Consts.SaveFileName;
        internal readonly string directoryPath = Storage.DirectoryPaths[0];
        internal readonly Data data = new();


        internal DataGroup(string groupName)
        {
            this.GroupName = groupName;
        }

        internal DataGroup(string groupName, string filePath, Data data)
        {
            this.GroupName = groupName;
            this.directoryPath = Path.GetDirectoryName(filePath).AdjustAsDirectoryPath(creatable: true);
            this.fileName = Path.GetFileName(filePath).AdjustAsFileName();
            this.data = data;
        }

        internal void Update<T>(string key, T value)
        {
            var type = typeof(T);
            if(!this.data.ContainsKey(type))
            {
                this.data[type] = new Dictionary<string, object>();
            }

            this.data[type][key] = value;
        }

        internal void Merge(DataGroup other)
        {
            foreach(var (type, key, value) in other)
            {
                var dic = this.data.GetOrAdd(type);
                if(!dic.ContainsKey(key))
                {
                    dic[key] = value;
                }
            }
        }
    }
}
