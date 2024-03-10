using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    using Utils;

    [XmlRoot("ArrayOfDataSet")]
    public sealed class XmlDataSets
    {
        [XmlArray("DataSet")]
        private readonly List<XmlDataSet> xmlDataSets = new();


        public XmlDataSets()
        {
        }

        public XmlDataSets(List<XmlDataSet> datasets)
        {
            this.xmlDataSets = datasets ?? new();
        }

        public IEnumerator<XmlDataSet> GetEnumerator()
        {
            foreach(var xmlDataSet in this.xmlDataSets)
            {
                yield return xmlDataSet;
            }
        }

        public static implicit operator List<XmlDataSet>(XmlDataSets xmlDataSets)
        {
            return xmlDataSets.xmlDataSets;
        }

        public static List<(string filePath, XmlDataSets datasets)> Load(string directoryPath)
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
