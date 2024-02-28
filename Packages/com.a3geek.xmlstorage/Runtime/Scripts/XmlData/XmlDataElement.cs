using System;

namespace XmlStorage.XmlData
{
    using Utils.Extensions;

    [Serializable]
    public sealed class XmlDataElement
    {
        public string Key { get; set; } = "";
        public object Value { get; set; } = null;
        public string TypeName { get; set; } = "";
        public Type Type => this.type ??= this.TypeName.GetTypeAsTypeName();

        private Type type = null;


        public XmlDataElement() { }

        public XmlDataElement(string key, object value, Type type)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Key cannot be null or empty.");
            }
            if(type == null)
            {
                throw new ArgumentNullException("type", "Type cannnot be null.");
            }

            this.Key = key;
            this.Value = value ?? throw new ArgumentNullException("value", "Value cannot be null.");
            this.TypeName = type.FullName;
        }
    }
}
