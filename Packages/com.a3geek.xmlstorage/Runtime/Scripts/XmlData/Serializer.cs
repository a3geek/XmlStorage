using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace XmlStorage.XmlData
{
    using Utils;
    using Utils.Extensions;

    public static class Serializer
    {
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
