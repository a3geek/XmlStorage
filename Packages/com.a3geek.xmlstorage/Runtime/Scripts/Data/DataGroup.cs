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
        public string FileName
        {
            get => this.fileName;
            set
            {
                var name = value.AdjustAsFileName();
                this.fileName = string.IsNullOrEmpty(name) ? this.fileName : name;
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

        internal DataGroup(in string groupName, in string filePath) : this(groupName)
        {
            this.directoryPath = Path.GetDirectoryName(filePath).AdjustAsDirectoryPath(creatable: true);
            this.fileName = Path.GetFileName(filePath).AdjustAsFileName();
        }

        internal DataGroup(in XmlDataGroup xmlDataGroup, in string filePath) : this(xmlDataGroup.GroupName, filePath)
        {
            foreach (var e in xmlDataGroup.Elements.Where(e => e.ValueType != null))
            {
                this.data.Update(e.Key, e.Value, e.ValueType);
            }
        }

        internal Data GetData()
        {
            return this.data;
        }
    }
}
