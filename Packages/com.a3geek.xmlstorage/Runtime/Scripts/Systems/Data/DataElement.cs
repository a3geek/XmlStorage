using System;
using System.IO;
using System.Reflection;

namespace XmlStorage.Systems.Data
{
    /// <summary>
    /// データをファイルに保存する時のデータ形式
    /// </summary>
    [Serializable]
    public sealed class DataElement
    {
        public string Key { get; set; } = "";
        public object Value { get; set; } = null;
        public string TypeName { get; set; } = "";
        public Type Type => this.GetType(this.TypeName);


        public DataElement() { }

        public DataElement(string key, object value, Type type)
        {
            if(key == null)
            {
                throw new ArgumentNullException("key", "Key cannot be null.");
            }
            else if(key == "")
            {
                throw new ArgumentException("key", "Key cannot be empty.");
            }

            if(type == null)
            {
                throw new ArgumentNullException("type", "Type cannnot be null.");
            }

            this.Key = key;
            this.Value = value ?? throw new ArgumentNullException("value", "Value cannot be null.");
            this.TypeName = type.FullName;
        }

        // https://answers.unity.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        private Type GetType(string typeName)
        {
            var type = Type.GetType(typeName);
            if(type != null)
            {
                return type;
            }

            if(typeName.Contains("."))
            {
                var assemblyName = typeName[..typeName.IndexOf('.')];
                try
                {
                    var assembly = Assembly.Load(assemblyName);

                    type = assembly.GetType(typeName);
                    if(type != null)
                    {
                        return type;
                    }
                }
                catch
                {
                    return null;
                }
            }

            var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach(var assemblyName in referencedAssemblies)
            {
                try
                {
                    var assembly = Assembly.Load(assemblyName);

                    type = assembly.GetType(typeName);
                    if(type != null)
                    {
                        return type;
                    }
                }
                catch
                {
                    continue;
                }
            }

            return null;
        }
    }
}
