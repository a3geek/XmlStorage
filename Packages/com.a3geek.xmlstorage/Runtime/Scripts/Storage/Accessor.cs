using System;
using System.Collections.Generic;

namespace XmlStorage
{
    public static partial class Storage
    {
        // public static IEnumerable<Type> GetTypes()
        // {
        //     var group = CurrentDataGroup;
        //     return group.GetData().data.Keys;
        // }
        //
        // public static IEnumerable<string> GetKeys(in Type type)
        // {
        //     var group = CurrentDataGroup;
        //     return group.GetData().TryGet(type, out var dictionary)
        //         ? dictionary.Keys
        //         : Array.Empty<string>();
        // }
        //
        // public static bool HasKey(in Type type, in string key)
        // {
        //     var group = CurrentDataGroup;
        //     return group.GetData().TryGet(type, out var dictionary) && dictionary.ContainsKey(key);
        // }
        //
        // public static bool Delete(in Type type, in string key)
        // {
        //     var group = CurrentDataGroup;
        //     return group.GetData().TryGet(type, out var dictionary) && dictionary.Remove(key);
        // }
        //
        // public static void DeleteAll()
        // {
        //     var group = CurrentDataGroup;
        //     group.GetData().data.Clear();
        // }
        //
        #region "Setter"
        public static void Set<T>(in string key, in T value)
        {
            SetValue(key, value, typeof(T));
        }
        
        public static void SetInt(in string key, int value)
        {
            SetValue(key, value, typeof(int));
        }
        
        public static void SetFloat(in string key, float value)
        {
            SetValue(key, value, typeof(float));
        }
        
        public static void SetBool(in string key, bool value)
        {
            SetValue(key, value, typeof(bool));
        }
        
        public static void SetString(in string key, in string value)
        {
            SetValue(key, value, typeof(string));
        }
        
        private static void SetValue<T>(in string key, in T value, in Type valueType)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }
            if (valueType == null)
            {
                throw new ArgumentNullException(nameof(valueType), "ValueType cannot be null.");
            }
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Value cannot be null.");
            }
            
            var group = CurrentDataGroup;
            group.GetData().Update(key, value, valueType);
        }
        #endregion
        
        #region "Getter"
        public static T Get<T>(in string key, in T defaultValue = default)
        {
            return GetValue(key, defaultValue);
        }
        
        public static int GetInt(in string key, int defaultValue = 0)
        {
            return GetValue(key, defaultValue);
        }
        
        public static float GetFloat(in string key, float defaultValue = 0f)
        {
            return GetValue(key, defaultValue);
        }
        
        public static bool GetBool(in string key, bool defaultValue = false)
        {
            return GetValue(key, defaultValue);
        }
        
        public static string GetString(in string key, in string defaultValue = "")
        {
            return GetValue(key, defaultValue);
        }
        
        private static T GetValue<T>(in string key, in T defaultValue, in Type valueType)
        {
            var group = CurrentDataGroup;
            if (group.GetData().TryGet(key, valueType, out var data))
            {
                return (T)data;
            }
            return  ? value : defaultValue;
            
            // return group.GetData().TryGet(key, out T value) ? value : defaultValue;
        }
        #endregion
    }
}
