using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    using Utils;

    [Serializable]
    [XmlRoot("ArrayOfDataSet")]
    public sealed class XmlDataSets
    {
        [XmlElement("DataSet")]
        public List<XmlDataSet> DataSets = new();


        public XmlDataSets()
        {
        }

        public XmlDataSets(in IEnumerable<XmlDataSet> datasets)
        {
            this.DataSets = datasets.ToList();
        }

        public IEnumerator<XmlDataSet> GetEnumerator()
        {
            foreach(var xmlDataSet in this.DataSets)
            {
                yield return xmlDataSet;
            }
        }

        public static explicit operator List<XmlDataSet>(in XmlDataSets xmlDataSets)
        {
            return new(xmlDataSets.DataSets);
        }

        public static List<(string filePath, XmlDataSets datasets)> Load(in string directoryPath)
        {
            if(!Directory.Exists(directoryPath))
            {
                return new();
            }

            var datasets = new List<(string filePath, XmlDataSets dataset)>();
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
