using System.IO;
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
            this.SaveFilePath = new FilePath();
        }

        internal DataGroup(in XmlDataGroup xmlDataGroup)
        {
            this.GroupName = xmlDataGroup.GroupName;
            this.SaveFilePath = xmlDataGroup.SaveFilePath;
            
            foreach (var e in xmlDataGroup.Elements)
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
