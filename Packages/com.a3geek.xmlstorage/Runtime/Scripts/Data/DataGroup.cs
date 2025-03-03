using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using XmlStorage.Utils;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Data
{
    /// <remarks>DataGroupはXmlStorage外からのアクセスを許可する</remarks>
    public sealed class DataGroup
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
        public string GroupName { get; }
        public string FullPath => this.directoryPath + this.FileName;
        
        private string fileName = Const.SaveFileName;
        private readonly string directoryPath = Storage.DirectoryPaths[0];
        private readonly Data data = new();


        internal DataGroup(in string groupName)
        {
            this.GroupName = groupName;
        }

        internal DataGroup(in string groupName, in string filePath, Dictionary<Type, Dictionary<string, object>> data)
        {
            this.GroupName = groupName;
            this.directoryPath = Path.GetDirectoryName(filePath).AdjustAsDirectoryPath(creatable: true);
            this.fileName = Path.GetFileName(filePath).AdjustAsFileName();
            this.data = new Data(data);
        }

        internal Data GetData()
        {
            return this.data;
        }
    }
}
