﻿using System;
using System.Xml.Serialization;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.XmlData
{
    [Serializable]
    internal sealed class XmlDataElement
    {
        public Type ValueType => this.type ??= this.TypeName.GetTypeAsTypeName();
        [XmlElement("Key")]
        public string Key = "";
        [XmlElement("Value")]
        public object Value = null;
        [XmlElement("TypeName")]
        public string TypeName = "";

        private Type type = null;


        public XmlDataElement() { }

        public XmlDataElement(in string key, in object value, in Type type)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key), "Key cannot be null or empty.");
            }
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type), "Type cannot be null.");
            }

            this.Key = key;
            this.Value = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            this.TypeName = type.FullName;
        }
    }
}
