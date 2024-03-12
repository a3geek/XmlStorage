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
        public string GroupName { get; } = "";
        public string FullPath => this.directoryPath + this.FileName;

        private string fileName = "";
        private readonly string directoryPath = "";
        public readonly Data data = new();


        public DataGroup(string groupName, string filePath, Data data)
        {
            this.GroupName = groupName;
            this.directoryPath = Path.GetDirectoryName(filePath).AdjustAsDirectoryPath(creatable: true);
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
