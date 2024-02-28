using System;
using System.Collections.Generic;

namespace XmlStorage.Data
{
    using Utils;
    using Utils.Extensions;
    using Data = Dictionary<Type, Dictionary<string, object>>;

    public partial class DataGroup
    {
        public string FullPath => Storage.DirectoryPath + this.FileName;
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
        private readonly Data data = new();


        public DataGroup(string groupName, Data data, string fileName)
        {
            this.GroupName = groupName;
            this.data = data;
            this.fileName = fileName;
        }
    }
}
