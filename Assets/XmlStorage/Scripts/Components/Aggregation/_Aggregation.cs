using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;


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

        private string fileName = "XmlStorage.xml";
        private string extension = ".xml";
        private string directoryPath = Application.persistentDataPath;
        private ExDictionary dictionary = new ExDictionary();


        public Aggregation(List<DataElement> elements, string aggregationName, bool serialize = true) {
            if(elements != null) {
                this.Set2DictionaryByList(elements, serialize);
            }

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

        public void Set2DictionaryByList(List<DataElement> list, bool serialize = true) {
            this.dictionary.Clear();
            this.Add2DictionaryByList(list, serialize);
        }

        public void Add2DictionaryByList(List<DataElement> list, bool serialize = true) {
            list.ForEach(e => {
                var vt = e.ValueType;

                if(!this.dictionary.ContainsKey(vt)) { this.dictionary[vt] = new Dictionary<string, object>(); }

                if(serialize) {
                    var serializer = new XmlSerializer(vt);

                    using(var sr = new StringReader(e.Value.ToString())) {
                        this.dictionary[vt][e.Key] = serializer.Deserialize(sr);
                    }
                }
                else {
                    this.dictionary[vt][e.Key] = e.Value;
                }
            });
        }

        public List<DataElement> GetDataAsList(Encoding encode = null, bool serialize = true) {
            var list = new List<DataElement>();

            foreach(var pair in this.dictionary) {
                var serializer = new XmlSerializer(pair.Key);

                foreach(var e in pair.Value) {
                    if(serialize) {
                        using(var sw = new StringWriterEncode(encode)) {
                            serializer.Serialize(sw, e.Value);
                            list.Add(new DataElement(e.Key, sw.ToString(), pair.Key));
                        }
                    }
                    else {
                        list.Add(new DataElement(e.Key, e.Value, pair.Key));
                    }
                }
            }

            return list;
        }
    }
}
