using System;
using System.IO;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    using Utils;
    using Utils.Extensions;

    internal static class Serializer
    {
        private static readonly XmlSerializer XmlDataSetSerializer = new(
            typeof(XmlDataSets), new XmlRootAttribute("ArrayOfDataSet")
        );


        public static void Serialize(string filePath, XmlDataSets xmlDataSets)
        {
            using var sw = new StreamWriter(filePath, false, Consts.Encode);
            XmlDataSetSerializer.Serialize(sw, xmlDataSets);
        }

        public static object Serialize(Type type, object value)
        {
            if(!type.IsNeedSerialize())
            {
                return value;
            }

            using var sw = new EncodedStringWriter(Consts.Encode);
            var serializer = new XmlSerializer(type);
            serializer.Serialize(sw, value);

            return sw.ToString();
        }

        public static XmlDataSets Deserialize(string filePath)
        {
            using var sr = new StreamReader(filePath, Consts.Encode);
            return (XmlDataSets)XmlDataSetSerializer.Deserialize(sr);
        }

        public static object Deserialize(Type type, object value)
        {
            if(!type.IsNeedSerialize())
            {
                return value;
            }

            using var sr = new StringReader(value.ToString());
            var serializer = new XmlSerializer(type);
            return serializer.Deserialize(sr);
        }
    }
}
