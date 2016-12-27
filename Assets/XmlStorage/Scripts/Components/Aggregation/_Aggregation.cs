using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace XmlStorage.Components {
    using ExDictionary = Dictionary<Type, Dictionary<string, object>>;

    [Serializable]
    public sealed partial class Aggregation {
        public string FileName {
            get { return this.fileName; }
            set { this.fileName = this.Adjust4FileName(value, this.fileName); }
        }
        public string Extension {
            get { return this.extension; }
            set { this.extension = this.Adjust4Extension(value, this.extension); }
        }
        public string DirectoryPath {
            get { return this.directoryPath; }
            set { this.directoryPath = this.Adjust4DirectoryPath(value, this.directoryPath); }
        }
        public string FileNameWithoutExtension {
            get { return this.FileName.TrimEnd(this.Extension.ToCharArray()); }
        }
        public string FullPath {
            get { return this.DirectoryPath + Path.DirectorySeparatorChar + this.FileName; }
        }
        public string AggregationName { get; private set; }
        public bool IsAllTypesSerialize { get; private set; }

        private string fileName = "XmlStorage.xml";
        private string extension = ".xml";
        private string directoryPath = Application.persistentDataPath;
        private ExDictionary dictionary = new ExDictionary();


        public Aggregation(List<DataElement> elements, string aggregationName, bool isAllTypesSerialize = false) {
            this.IsAllTypesSerialize = isAllTypesSerialize;

            if(elements != null) { this.Set2DictionaryByList(elements); }
            this.AggregationName = (string.IsNullOrEmpty(aggregationName) ? Guid.NewGuid().ToString() : aggregationName);

            this.Extension = ".xml";
            this.FileName = SceneManager.GetActiveScene().name + this.Extension;
            this.DirectoryPath = Application.persistentDataPath;
        }

        public void DeleteAll() {
            this.dictionary.Clear();
        }

        public bool DeleteKey(string key) {
            var flag = false;

            foreach(var pair in this.dictionary) {
                if(pair.Value.Remove(key)) { flag = true; }
            }

            return flag;
        }

        public bool DeleteKey(string key, Type type) {
            return dictionary[type].Remove(key);
        }

        public bool HasKey(string key) {
            foreach(var pair in this.dictionary) {
                if(HasKey(key, pair.Key)) { return true; }
            }

            return false;
        }

        public bool HasKey(string key, Type type) {
            return dictionary.ContainsKey(type) && dictionary[type].ContainsKey(key);
        }

        public void Set2DictionaryByList(List<DataElement> list) {
            this.dictionary.Clear();
            this.Add2DictionaryByList(list);
        }

        public void Add2DictionaryByList(List<DataElement> list) {
            list.ForEach(e => {
                var vt = e.ValueType;

                if(!this.dictionary.ContainsKey(vt)) { this.dictionary[vt] = new Dictionary<string, object>(); }

                if(this.IsAllTypesSerialize || !vt.IsSerializable) {
                    this.dictionary[vt][e.Key] = this.Deserialize(e.Value, vt);
                }
                else {
                    this.dictionary[vt][e.Key] = e.Value;
                }
            });
        }

        public List<DataElement> GetDataAsList(Encoding encode = null) {
            var list = new List<DataElement>();
            
            foreach(var pair in this.dictionary) {
                foreach(var e in pair.Value) {
                    if(this.IsAllTypesSerialize) {
                        list.Add(this.Object2DataElement(e.Value, e.Key, pair.Key, true, encode));
                    }
                    else {
                        list.Add(this.Object2DataElement(e.Value, e.Key, pair.Key, !e.Value.GetType().IsSerializable, encode));
                    }
                }
            }

            return list;
        }

        private DataElement Object2DataElement(object value, string key, Type type, bool serialize = false, Encoding encode = null) {
            if(serialize) {
                var serializer = new XmlSerializer(type);

                using(var sw = new StringWriterEncode(encode)) {
                    serializer.Serialize(sw, value);

                    return new DataElement(key, sw.ToString(), type);
                }
            }

            return new DataElement(key, value, type);
        }

        private object Deserialize(object value, Type type) { return this.Deserialize(value.ToString(), type); }

        private object Deserialize(string value, Type type) {
            var serializer = new XmlSerializer(type);

            using(var sr = new StringReader(value)) {
                return serializer.Deserialize(sr);
            }
        }
    }
}
