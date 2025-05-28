using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using XmlStorage.Utils;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.XmlData
{
    internal static class Serializer
    {
        private static readonly Dictionary<Type, XmlSerializer> XmlSerializers = new();


        public static void Serialize(string filePath, XmlDataGroup xmlDataGroup)
        {
            using var sw = new StreamWriter(filePath, false, Const.Encode);
            GetXmlSerializer(typeof(XmlDataGroup)).Serialize(sw, xmlDataGroup);
        }

        // public static object Serialize(in Type type, in object value)
        // {
        //     if (!type.IsNeedSerialize())
        //     {
        //         return value;
        //     }
        //
        //     using var sw = new EncodedStringWriter(Const.Encode);
        //     var serializer = GetXmlSerializer(type);
        //     serializer.Serialize(sw, value);
        //
        //     return sw.ToString();
        // }

        public static XmlDataGroup Deserialize(in string filePath)
        {
            using var sr = new StreamReader(filePath, Const.Encode);
            return (XmlDataGroup)GetXmlSerializer(typeof(XmlDataGroup)).Deserialize(sr);
        }

        public static object Deserialize(in Type type, in object value)
        {
            if (!type.IsNeedSerialize())
            {
                return value;
            }

            using var sr = new StringReader(value.ToString());
            var serializer = GetXmlSerializer(type);
            return serializer.Deserialize(sr);
        }

        private static XmlSerializer GetXmlSerializer(Type type)
        {
            if (XmlSerializers.TryGetValue(type, out var serializer))
            {
                return serializer;
            }

            serializer = new XmlSerializer(type);
            XmlSerializers.Add(type, serializer);
            return serializer;
        }
    }
}
