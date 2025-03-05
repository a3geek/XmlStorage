using System.IO;
using System.Linq;
using XmlStorage.Utils;
using XmlStorage.Utils.Extensions;
using XmlStorage.XmlData;

namespace XmlStorage.Data
{
    /// <remarks>DataGroupはXmlStorage外からのアクセスを許可する</remarks>
    public sealed class DataGroup
    {
        public string GroupName { get; }
        public FilePath SaveFilePath { get; }

        private readonly Data data = new();


        internal DataGroup(in string groupName)
        {
            this.GroupName = groupName;
            this.SaveFilePath = new FilePath(Const.SaveFileName, Storage.DirectoryPaths[0]);
        }

        internal DataGroup(in XmlDataGroup xmlDataGroup)
        {
            this.GroupName = xmlDataGroup.GroupName;
            this.SaveFilePath = xmlDataGroup.SaveFilePath;
            
            foreach (var e in xmlDataGroup.Elements.Where(e => e.ValueType != null))
            {
                this.data.Update(e.Key, e.Value, e.ValueType);
            }
        }

        internal Data GetData()
        {
            return this.data;
        }


        public sealed class FilePath
        {
            public string FileName
            {
                get => this.fileName;
                set
                {
                    var name = value.AdjustAsFileName();
                    this.fileName = string.IsNullOrEmpty(name) ? this.fileName : name;
                    this.FullPath = this.directoryPath + this.fileName;
                }
            }
            public string DirectoryPath
            {
                get => this.directoryPath;
                set
                {
                    var path = value.AdjustAsDirectoryPath(creatable: false);
                    this.directoryPath = string.IsNullOrEmpty(path) ? this.directoryPath : path;
                    this.FullPath = this.directoryPath + this.fileName;
                }
            }
            public string FullPath { get; private set; } = null;

            private string fileName = null;
            private string directoryPath = null;


            public FilePath(string filePath) : this(Path.GetFileName(filePath), Path.GetDirectoryName(filePath)) { }

            public FilePath(in string fileName, in string directoryPath)
            {
                this.FileName = fileName;
                this.DirectoryPath = directoryPath;
                this.FullPath = fileName + directoryPath;
            }
        }
    }
}
