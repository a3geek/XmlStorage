using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using XmlStorage.Utils;

namespace XmlStorage.XmlData
{
    [Serializable]
    [XmlRoot(XmlRootName)]
    internal sealed class XmlDataGroups
    {
        public const string XmlRootName = "ArrayOfDataGroup";

        [XmlElement("DataGroup")]
        public List<XmlDataGroup> Groups = new();


        public XmlDataGroups() { }

        public XmlDataGroups(in IEnumerable<XmlDataGroup> dataGroups)
        {
            this.Groups = dataGroups.ToList();
        }

        public IEnumerator<XmlDataGroup> GetEnumerator()
        {
            return ((IEnumerable<XmlDataGroup>)this.Groups).GetEnumerator();
        }

        public static explicit operator List<XmlDataGroup>(in XmlDataGroups dataGroups)
        {
            return new List<XmlDataGroup>(dataGroups.Groups);
        }

        public static List<(string filePath, XmlDataGroups dataGroups)> Load(in string directoryPath)
        {
            var dataGroups = new List<(string filePath, XmlDataGroups dataGroups)>();
            if (!Directory.Exists(directoryPath))
            {
                return dataGroups;
            }

            var filePaths = Directory.GetFiles(
                directoryPath, Const.FileSearchPattern, SearchOption.AllDirectories
            );
            foreach (var filePath in filePaths)
            {
                dataGroups.Add((filePath, Serializer.Deserialize(filePath)));
            }

            return dataGroups;
        }
    }
}
