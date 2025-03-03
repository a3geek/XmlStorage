using System;
using System.IO;
using System.Xml.Serialization;
using XmlStorage.Utils;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.XmlData
{
    internal static class Serializer
    {
        private static readonly XmlSerializer XmlDataGroupsSerializer = new(
            typeof(XmlDataGroups), new XmlRootAttribute(XmlDataGroups.XmlRootName)
        );


        public static void Serialize(in string filePath, in XmlDataGroups xmlDataGroups)
        {
            using var sw = new StreamWriter(filePath, false, Const.Encode);
            XmlDataGroupsSerializer.Serialize(sw, xmlDataGroups);
        }

        public static object Serialize(in Type type, in object value)
        {
            if(!type.IsNeedSerialize())
            {
                return value;
            }

            using var sw = new EncodedStringWriter(Const.Encode);
            var serializer = new XmlSerializer(type);
            serializer.Serialize(sw, value);

            return sw.ToString();
        }

        public static XmlDataGroups Deserialize(in string filePath)
        {
            using var sr = new StreamReader(filePath, Const.Encode);
            return (XmlDataGroups)XmlDataGroupsSerializer.Deserialize(sr);
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
