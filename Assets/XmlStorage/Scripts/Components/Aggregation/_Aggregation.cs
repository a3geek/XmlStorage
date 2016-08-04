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


        public Aggregation(List<DataElement> elements, string aggregationName) {
            if(elements != null) {
                this.Set2DictionaryByList(elements);
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

        public void Set2DictionaryByList(List<DataElement> list) {
            this.dictionary.Clear();
            this.Add2DictionaryByList(list);
        }

        public void Add2DictionaryByList(List<DataElement> list) {
            list.ForEach(e => {
                if(!this.dictionary.ContainsKey(e.ValueType)) { this.dictionary[e.ValueType] = new Dictionary<string, object>(); }

                this.dictionary[e.ValueType][e.Key] = e.Value;
            });
        }

        public List<DataElement> GetDataAsList() {
            var list = new List<DataElement>();

            foreach(var pair in this.dictionary) {
                foreach(var e in pair.Value) {
                    list.Add(new DataElement(e.Key, e.Value, pair.Key));
                }
            }

            return list;
        }
    }
}
