using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    using Utils;

    [Serializable]
    [XmlRoot("ArrayOfDataGroup")]
    public sealed class XmlDataGroups
    {
        [XmlElement("DataGroup")]
        public List<XmlDataGroup> DataSets = new();


        public XmlDataGroups()
        {
        }

        public XmlDataGroups(in IEnumerable<XmlDataGroup> datasets)
        {
            this.DataSets = datasets.ToList();
        }

        public IEnumerator<XmlDataGroup> GetEnumerator()
        {
            foreach(var xmlDataSet in this.DataSets)
            {
                yield return xmlDataSet;
            }
        }

        public static explicit operator List<XmlDataGroup>(in XmlDataGroups xmlDataSets)
        {
            return new(xmlDataSets.DataSets);
        }

        public static List<(string filePath, XmlDataGroups datasets)> Load(in string directoryPath)
        {
            if(!Directory.Exists(directoryPath))
            {
                return new();
            }

            var datasets = new List<(string filePath, XmlDataGroups dataset)>();
            var filePaths = Directory.GetFiles(
                directoryPath, Consts.FileSearchPattern, SearchOption.AllDirectories
            );

            foreach(var filePath in filePaths)
            {
                var xmlDataSets = Serializer.Deserialize(filePath);
                datasets.Add((filePath, xmlDataSets));
            }

            return datasets;
        }
    }
}
