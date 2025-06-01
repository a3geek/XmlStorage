using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using XmlStorage.Utilities.CustomExtensions;

namespace XmlStorage.XmlData
{
    internal static class Serializer
    {
        private static readonly UTF8Encoding Encode = new(false);
        private static readonly Dictionary<Type, XmlSerializer> XmlSerializers = new();


        public static void Serialize(string filePath, XmlDataGroup xmlDataGroup)
        {
            using var sw = new StreamWriter(filePath, false, Encode);
            var serializer = GetXmlSerializer(typeof(XmlDataGroup));
            serializer.Serialize(sw, xmlDataGroup);
        }

        public static object Serialize(object value, Type type)
        {
            if (!type.IsNeedSerialize())
            {
                return value;
            }

            using var sw = new StringWriter();
            var serializer = GetXmlSerializer(type);
            serializer.Serialize(sw, value);

            return sw.ToString();
        }

        public static XmlDataGroup Deserialize(string filePath)
        {
            try
            {
                using var sr = new StreamReader(filePath, Encode);
                var serializer = GetXmlSerializer(typeof(XmlDataGroup));
                return serializer.Deserialize(sr) as XmlDataGroup;
            }
            catch
            {
                return null;
            }
        }

        public static object Deserialize(object value, Type type)
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
