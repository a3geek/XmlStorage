using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;


namespace XmlStorage.Components {
    public sealed partial class Aggregation {
        #region "Setters"
        public void Set<T>(string key, T value) {
            var serializer = new XmlSerializer(typeof(T));

            using(var sw = new StringWriter()) {
                serializer.Serialize(sw, value);
                this.SetValue(key, sw.ToString(), typeof(T));
            }
        }
        
        public void SetFloat(string key, float value) { this.SetValue(key, value); }

        public void SetInt(string key, int value) { this.SetValue(key, value); }

        public void SetString(string key, string value) { this.SetValue(key, value); }

        public void SetBool(string key, bool value) { this.SetValue(key, value); }

        private void SetValue<T>(string key, T value, Type type = null) {
            type = (type == null ? typeof(T) : type);
            if(!this.dictionary.ContainsKey(type)) { dictionary[type] = new Dictionary<string, object>(); }

            this.dictionary[type][key] = value;
        }
        #endregion

        #region "Getters"
        public T Get<T>(string key, T defaultValue = default(T)) {
            Func<object, T> converter = obj => {
                var serializer = new XmlSerializer(typeof(T));

                using(var sr = new StringReader((string)obj)) {
                    return (T)serializer.Deserialize(sr);
                }
            };

            return this.GetValue<T>(key, defaultValue, converter, typeof(T));
        }
        
        public float GetFloat(string key, float defaultValue = default(float)) { return this.GetValue<float>(key, defaultValue); }

        public int GetInt(string key, int defaultValue = default(int)) { return this.GetValue<int>(key, defaultValue); }

        public string GetString(string key, string defaultValue = "") { return this.GetValue<string>(key, defaultValue); }

        public bool GetBool(string key, bool defaultValue = default(bool)) { return this.GetValue<bool>(key, defaultValue); }

        private T GetValue<T>(string key, T defaultValue, Func<object, T> converter = null, Type type = null) {
            type = (type == null ? typeof(T) : type);
            return this.HasKey(key, type) ?
                (converter == null ? (T)this.dictionary[type][key] : converter(dictionary[type][key])) :
                defaultValue;
        }
        #endregion
    }
}
