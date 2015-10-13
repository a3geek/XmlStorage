using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;


namespace XmlSaver {
    [Serializable]
    public sealed class KeyValue {
        public string key { get; private set; }
        public object value { get; private set; }


        public KeyValue() : this(Guid.NewGuid().ToString(), new object()) { ; }
        public KeyValue(string key, object value) { this.Set(key, value); }
        public KeyValue(KeyValuePair<string, object> pair) : this(pair.Key, pair.Value) { ; }


        public void Set(string key, object value) {
            if(key == null) { throw new ArgumentNullException("key", "Key cannot be null."); }
            else if(key == "") { throw new ArgumentException("key", "Key cannot be empty."); }
            else if(value == null) { throw new ArgumentNullException("value", "Value cannot be null."); }

            this.key = key;
            this.value = value;
        }
    }

    public sealed class XmlSaver {
        public static string FileName {
            get { return fileName; }
            set { if(value != null && value != "") { fileName = value.EndsWith(extension) ? value : value + extension; } }
        }
        public static string Extension {
            get { return extension; }
            set { if(value != null && value != "") { extension = value.StartsWith(".") ? value : "." + value; } }
        }
        public static string FileNameWithoutExtension { get { return FileName.Substring(0, FileName.Length - extension.Length); } }
        public static string FullPath { get { return Application.persistentDataPath + Path.DirectorySeparatorChar + FileName; } }

        private static string fileName = "XmlSaver.xml";
        private static string extension = ".xml";
        private static Dictionary<string, object> dictionary = new Dictionary<string, object>();
        private readonly static XmlSerializer serializer = new XmlSerializer(typeof(List<KeyValue>));
        private readonly static UTF8Encoding encode = new UTF8Encoding(false);


        static XmlSaver() {
            dictionary = Load();
        }

        public static void DeleteAll() {
            dictionary.Clear();
            Save();
        }

        public static void DeleteKey(string key) {
            dictionary.Remove(key);
            Save();
        }
        
        public static void Save() {
            if(dictionary.Count <= 0) {
                File.Delete(FullPath);
                return;
            }

            using(var sw = new StreamWriter(FullPath, false, encode)) {
                serializer.Serialize(sw, ConvertDictionary2List(dictionary));
            }
        }

        public static bool HasKey(string key) {
            return dictionary.ContainsKey(key);
        }

        #region "Setters"
        public static void Set<T>(string key, T value) {
            var serializer = new XmlSerializer(typeof(T));
            
            using(var sw = new StringWriter()) {
                serializer.Serialize(sw, value);
                dictionary[key] = sw.ToString();
            }
        }

        public static void SetFloat(string key, float value) {
            dictionary[key] = value;
        }

        public static void SetInt(string key, int value) {
            dictionary[key] = value;
        }

        public static void SetString(string key, string value) {
            dictionary[key] = value;
        }

        public static void SetBool(string key, bool value) {
            dictionary[key] = value;
        }
        #endregion

        #region "Getters"
        public static T Get<T>(string key) {
            return Get<T>(key, default(T));
        }

        public static T Get<T>(string key, T defaultValue) {
            return Get<T>(key, defaultValue, obj => {
                var serializer = new XmlSerializer(typeof(T));

                using(var sr = new StringReader((string)obj)) {
                    return (T)serializer.Deserialize(sr);
                }
            });
        }

        public static float GetFloat(string key, float defaultValue = 0.0f) {
            return Get<float>(key, defaultValue, null);
        }

        public static float GetInt(string key, int defaultValue = 0) {
            return Get<int>(key, defaultValue, null);
        }

        public static string GetString(string key, string defaultValue = "") {
            return Get<string>(key, defaultValue, null);
        }

        public static bool GetBool(string key, bool defaultValue = false) {
            return Get<bool>(key, defaultValue, null);
        }

        private static T Get<T>(string key, T defaultValue, Func<object, T> getter) {
            return HasKey(key) ? (getter == null ? (T)dictionary[key] : getter(dictionary[key])) : defaultValue;
        }
        #endregion
        
        private static Dictionary<string, object> Load() {
            if(!File.Exists(FullPath)) { return new Dictionary<string, object>(); }
            
            using(var sr = new StreamReader(FullPath, encode)) {
                return ConvertList2Dictionary((List<KeyValue>)serializer.Deserialize(sr));
            }
        }

        private static Dictionary<string, object> ConvertList2Dictionary(List<KeyValue> list) {
            dictionary = new Dictionary<string, object>();
            list.ForEach(e => dictionary.Add(e.key, e.value));

            return dictionary;
        }

        private static List<KeyValue> ConvertDictionary2List(Dictionary<string, object> dic) {
            var list = new List<KeyValue>();
            foreach(var pair in dic) { list.Add(new KeyValue(pair)); }

            return list;
        }
    }
}
