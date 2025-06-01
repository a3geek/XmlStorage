namespace XmlStorage.Data
{
    /// <remarks>DataGroupはXmlStorage外からのアクセスを許可する</remarks>
    public sealed class DataGroup
    {
        public string GroupName { get; }
        public FilePath FilePath => new(Storage.DirectoryPath, this.GroupName, Storage.Extension);

        private readonly Data data = new();


        internal DataGroup(string groupName)
        {
            this.GroupName = groupName;
        }

        internal Data GetData()
        {
            return this.data;
        }
    }
}
