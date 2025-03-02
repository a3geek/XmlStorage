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
            typeof(XmlDataGroups), new XmlRootAttribute("ArrayOfDataSet")
        );


        public static void Serialize(in string filePath, in XmlDataGroups xmlDataSets)
        {
            using var sw = new StreamWriter(filePath, false, Consts.Encode);
            XmlDataSetSerializer.Serialize(sw, xmlDataSets);
        }

        public static object Serialize(in Type type, in object value)
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

        public static XmlDataGroups Deserialize(in string filePath)
        {
            using var sr = new StreamReader(filePath, Consts.Encode);
            return (XmlDataGroups)XmlDataSetSerializer.Deserialize(sr);
        }

        public static object Deserialize(in Type type, in object value)
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
