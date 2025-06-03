using System;

namespace XmlStorage
{
    public static partial class Storage
    {
        public static bool HasKey(string key, Type type, string groupName = null)
        {
            var group = GetCurrentDataGroup(groupName ?? CurrentDataGroupName);
            return group.GetData().TryGet(key, type, out _);
        }

        public static bool Delete(string key, Type type, string groupName = null)
        {
            var group = GetCurrentDataGroup(groupName ?? CurrentDataGroupName);
            return group.GetData().Delete(key, type);
        }

        public static void DeleteAll(string groupName = null)
        {
            var group = GetCurrentDataGroup(groupName ?? CurrentDataGroupName);
            group.GetData().DeleteAll();
        }

    #region "Setter"
        public static void Set<T>(string key, T value)
        {
            SetValue(key, value, typeof(T));
        }

        public static void SetInt(string key, int value)
        {
            SetValue(key, value, typeof(int));
        }

        public static void SetFloat(string key, float value)
        {
            SetValue(key, value, typeof(float));
        }

        public static void SetBool(string key, bool value)
        {
            SetValue(key, value, typeof(bool));
        }

        public static void SetString(string key, string value)
        {
            SetValue(key, value, typeof(string));
        }

        private static void SetValue<T>(string key, T value, Type valueType, string groupName = null)
        {
            var group = GetCurrentDataGroup(groupName ?? CurrentDataGroupName);
            group.GetData().Update(key, value, valueType);
        }
    #endregion

    #region "Getter"
        public static T Get<T>(string key, T defaultValue = default)
        {
            return GetValue(key, defaultValue, typeof(T));
        }

        public static int GetInt(string key, int defaultValue = 0)
        {
            return GetValue(key, defaultValue, typeof(int));
        }

        public static float GetFloat(string key, float defaultValue = 0f)
        {
            return GetValue(key, defaultValue, typeof(float));
        }

        public static bool GetBool(string key, bool defaultValue = false)
        {
            return GetValue(key, defaultValue, typeof(bool));
        }

        public static string GetString(string key, string defaultValue = "")
        {
            return GetValue(key, defaultValue, typeof(string));
        }

        private static T GetValue<T>(string key, T defaultValue,  Type valueType, string groupName = null)
        {
            var group = GetCurrentDataGroup(groupName ?? CurrentDataGroupName);
            if (!group.GetData().TryGet(key, valueType, out var data))
            {
                return defaultValue;
            }

            return data.Value is T v ? v : defaultValue;
        }
    #endregion
    }
}
