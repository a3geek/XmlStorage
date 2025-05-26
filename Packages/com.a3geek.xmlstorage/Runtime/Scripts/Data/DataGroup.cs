using XmlStorage.XmlData;

namespace XmlStorage.Data
{
    /// <remarks>DataGroupはXmlStorage外からのアクセスを許可する</remarks>
    public sealed class DataGroup
    {
        public string GroupName { get; }

        private readonly Data data = new();


        internal DataGroup(string groupName)
        {
            this.GroupName = groupName;
        }

        internal DataGroup(in XmlDataGroup xmlDataGroup)
        {
            this.GroupName = xmlDataGroup.GroupName;

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
