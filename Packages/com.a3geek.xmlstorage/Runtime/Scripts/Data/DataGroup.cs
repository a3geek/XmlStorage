using System;
using System.Collections.Generic;

namespace XmlStorage.Data
{
    using System.IO;
    using Utils;
    using Utils.Extensions;
    using Data = Dictionary<Type, Dictionary<string, object>>;

    public partial class DataGroup
    {
        public string FullPath => this.directoryPath + this.FileName;
        public string GroupName { get; } = "";
        public string FileName
        {
            get
            {
                return string.IsNullOrEmpty(this.fileName)
                    ? nameof(XmlStorage) + Consts.Extension
                    : this.fileName;
            }
            set
            {
                var name = value.AdjustAsFileName();
                this.fileName = string.IsNullOrEmpty(name) ? this.fileName : name;
            }
        }

        private string fileName = "";
        private readonly string directoryPath = "";
        private readonly Data data = new();


        public DataGroup(string groupName, string filePath, Data data)
        {
            this.GroupName = groupName;
            this.directoryPath = Path.GetDirectoryName(filePath).AdjustAsDirectoryPath(creatable: false);
            this.fileName = Path.GetFileName(filePath).AdjustAsFileName();
            this.data = data;
        }

        public void Merge(DataGroup other)
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

        public IEnumerator<(Type type, string key, object value)> GetEnumerator()
        {
            foreach(var (type, data) in this.data)
            {
                foreach(var (key, value) in data)
                {
                    yield return (type, key, value);
                }
            }
        }
    }
}
