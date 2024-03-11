using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    using System.Linq;
    using Utils;

    [XmlRoot("ArrayOfDataSet")]
    public sealed class XmlDataSets
    {
        [XmlArray("DataSet")]
        private readonly IEnumerable<XmlDataSet> xmlDataSets = Enumerable.Empty<XmlDataSet>();


        public XmlDataSets()
        {
        }

        public XmlDataSets(IEnumerable<XmlDataSet> datasets)
        {
            this.xmlDataSets = datasets ?? Enumerable.Empty<XmlDataSet>();
        }

        public IEnumerator<XmlDataSet> GetEnumerator()
        {
            foreach(var xmlDataSet in this.xmlDataSets)
            {
                yield return xmlDataSet;
            }
        }

        public static explicit operator List<XmlDataSet>(XmlDataSets xmlDataSets)
        {
            return new(xmlDataSets.xmlDataSets);
        }

        public static List<(string filePath, XmlDataSets datasets)> Load(string directoryPath)
        {
            if(!Directory.Exists(directoryPath))
            {
                return new();
            }
            UnityEngine.Debug.Log(directoryPath);

            var datasets = new List<(string filePath, XmlDataSets dataset)>();
            var filePaths = Directory.GetFiles(
                directoryPath, Consts.FileSearchPattern, SearchOption.AllDirectories
            );

            foreach(var filePath in filePaths)
            {
                var xmlDataSets = Serializer.Deserialize(filePath);

                var list = (List<XmlDataSet>)xmlDataSets;
                UnityEngine.Debug.Log(filePath + " : " + list.Count);
                foreach(var xmlDataSet in xmlDataSets)
                {
                    UnityEngine.Debug.Log(xmlDataSet.GroupName);
                }

                datasets.Add((filePath, xmlDataSets));
            }

            UnityEngine.Debug.Log(datasets.Count);
            return datasets;
        }
    }
}
