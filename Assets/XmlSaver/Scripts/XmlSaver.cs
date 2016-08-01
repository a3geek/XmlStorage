using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;


namespace XmlSaver {
    using ExDictionary = Dictionary<Type, Dictionary<string, object>>;

    /// <summary>SerializeしてXMLデータとしてファイル保存する時に利用する形式</summary>
    [Serializable]
    public sealed class SaveDataElement {
        /// <summary>データを取り出す時に使うキー</summary>
        public string Key { get; private set; }
        /// <summary>保存するデータ</summary>
        public object Value { get; private set; }
        /// <summary>データの型のフルネーム</summary>
        public string TypeName { get; private set; }
        /// <summary>データの型(RO)</summary>
        public Type ValueType { get { return this.GetType(this.TypeName); } }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SaveDataElement() : this(Guid.NewGuid().ToString(), new object(), typeof(object).FullName) {; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型</param>
        public SaveDataElement(string key, object value, Type type) : this(key, value, type.FullName) {; }
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型のフルネーム</param>
        public SaveDataElement(string key, object value, string type) { this.Set(key, value, type); }

        /// <summary>
        /// メンバ変数の値を更新する
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型</param>
        public void Set(string key, object value, Type type) { this.Set(key, value, type.FullName); }

        /// <summary>
        /// メンバ変数の値を更新する
        /// </summary>
        /// <param name="key">データを取り出す時に使うキー</param>
        /// <param name="value">保存するデータ</param>
        /// <param name="type">データの型のフルネーム</param>
        public void Set(string key, object value, string type) {
            if(key == null) { throw new ArgumentNullException("key", "Key cannot be null."); }
            if(key == "") { throw new ArgumentException("key", "Key cannot be empty."); }

            if(value == null) { throw new ArgumentNullException("value", "Value cannot be null."); }

            if(type == null) { throw new ArgumentNullException("type", "Type cannnot be null."); }
            if(type == "") { throw new ArgumentException("type", "Type cannot be empty."); }

            this.Key = key;
            this.Value = value;
            this.TypeName = type;
        }

        /// <summary>
        /// 文字列から型情報を取得する(UnityEngine内部の型はType.GetTypeではnullになるのでこの関数を利用する)
        /// </summary>
        /// <param name="typeName">型のフルネーム</param>
        /// <returns>検索結果</returns>
        private Type GetType(string typeName) {
            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType(typeName);

            // If it worked, then we're done here
            if(type != null) {
                return type;
            }

            // If the TypeName is a full name, then we can try loading the defining assembly directly
            if(typeName.Contains(".")) {
                // Get the name of the assembly (Assumption is that we are using 
                // fully-qualified type names)
                var assemblyName = typeName.Substring(0, typeName.IndexOf('.'));

                // Attempt to load the indicated Assembly
                var assembly = Assembly.Load(assemblyName);
                if(assembly == null) {
                    return null;
                }

                // Ask that assembly to return the proper Type
                type = assembly.GetType(typeName);
                if(type != null) {
                    return type;
                }
            }

            // If we still haven't found the proper type, we can enumerate all of the 
            // loaded assemblies and see if any of them define the type
            var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();

            foreach(var assemblyName in referencedAssemblies) {
                // Load the referenced assembly
                var assembly = Assembly.Load(assemblyName);

                if(assembly != null) {
                    // See if that assembly defines the named type
                    type = assembly.GetType(typeName);

                    if(type != null) {
                        return type;
                    }
                }
            }

            // The type just couldn't be found...
            return null;
        }
    }

    /// <summary>セットしたデータ群をXML形式で保存する</summary>
    public static class XmlSaver {
        /// <summary>保存する時のファイル名</summary>
        public static string FileName {
            get { return fileName; }
            set { if(value != null && value != "") { fileName = value.EndsWith(extension) ? value : value + extension; } }
        }
        /// <summary>保存する時のファイル拡張子</summary>
        public static string Extension {
            get { return extension; }
            set { if(value != null && value != "") { extension = value.StartsWith(".") ? value : "." + value; } }
        }
        /// <summary>保存する時のファイル名(拡張子なし)</summary>
        public static string FileNameWithoutExtension { get { return FileName.Substring(0, FileName.Length - Extension.Length); } }
        /// <summary>保存する時のフルファイルパス</summary>
        public static string FullPath { get { return ChangePath4Platform(DirectoryPath + Path.DirectorySeparatorChar + FileName); } }
        /// <summary>保存するファイルを置くフォルダ</summary>
        public static string DirectoryPath { get { return ChangePath4Platform(Application.persistentDataPath); } }

        /// <summary>保存する時のファイル名を保持</summary>
        private static string fileName = "XmlSaver.xml";
        /// <summary>保存する時のファイル拡張子を保持</summary>
        private static string extension = ".xml";
        /// <summary>セットされたデータ群を保持</summary>
        private static ExDictionary dictionary = new ExDictionary();
        /// <summary>データをXMLにシリアライズするためのシリアライザーインスタンス</summary>
        private readonly static XmlSerializer serializer = new XmlSerializer(typeof(List<SaveDataElement>));
        /// <summary>ファイルに保存する際のエンコード情報</summary>
        private readonly static UTF8Encoding encode = new UTF8Encoding(false);


        /// <summary>静的コンストラクタ</summary>
        static XmlSaver() {
            dictionary = Load();
        }

        /// <summary>セットした全てのデータを消去する</summary>
        public static void DeleteAll() {
            dictionary.Clear();
            Save();
        }

        /// <summary>
        /// 引数に渡したキーと一致するデータを全て消去する
        /// </summary>
        /// <param name="key">消去するデータ郡のキー</param>
        public static void DeleteKey(string key) {
            foreach(var pair in dictionary) { pair.Value.Remove(key); }

            Save();
        }

        /// <summary>
        /// 引数に渡したキーと型に一致するデータを消去する
        /// </summary>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="type">消去するデータの型</param>
        public static void DeleteKey(string key, Type type) {
            dictionary[type].Remove(key);
            Save();
        }

        /// <summary>セットしたデータ群をXML形式でファイルに保存する</summary>
        public static void Save() {
            if(dictionary.Count <= 0) {
                File.Delete(FullPath);
                return;
            }

            using(var sw = new StreamWriter(FullPath, false, encode)) {
                serializer.Serialize(sw, ConvertExDictionary2List(dictionary));
            }
        }

        /// <summary>
        /// 引数に渡したキーと一致するデータが一つでも存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータ群のキー</param>
        /// <returns>検索結果</returns>
        public static bool HasKey(string key) {
            foreach(var pair in dictionary) {
                if(HasKey(key, pair.Key)) { return true; }
            }

            return false;
        }

        /// <summary>
        /// 引数に渡したキーと型に一致するデータが存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="type">検索するデータの型</param>
        /// <returns>検索結果</returns>
        public static bool HasKey(string key, Type type) {
            return dictionary.ContainsKey(type) && dictionary[type].ContainsKey(key);
        }

        #region "Setters"
        /// <summary>
        /// 任意の型のデータとキーをセットする
        /// </summary>
        /// <typeparam name="T">セットするデータの型(Serializable)</typeparam>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void Set<T>(string key, T value) {
            Set<T>(key, value, typeof(T));
        }

        public static void Set<T>(string key, T value, Type type) {
            var serializer = new XmlSerializer(type);

            using(var sw = new StringWriter()) {
                serializer.Serialize(sw, value);
                SetValue(key, sw.ToString(), type);
            }
        }

        /// <summary>
        /// float型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetFloat(string key, float value) {
            SetValue<float>(key, value);
        }

        /// <summary>
        /// int型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetInt(string key, int value) {
            SetValue<int>(key, value);
        }

        /// <summary>
        /// string型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetString(string key, string value) {
            SetValue<string>(key, value);
        }

        /// <summary>
        /// bool型のデータとキーをセットする
        /// </summary>
        /// <param name="key">セットするデータのキー</param>
        /// <param name="value">セットするデータ</param>
        public static void SetBool(string key, bool value) {
            SetValue<bool>(key, value);
        }

        /// <summary>
        /// データとキー情報を内部的に保持する
        /// </summary>
        /// <typeparam name="T">保持するデータの型(Serializable)</typeparam>
        /// <param name="key">保持するデータのキー</param>
        /// <param name="value">保持するデータ</param>
        /// <param name="type">保持するデータの型情報</param>
        private static void SetValue<T>(string key, T value, Type type = null) {
            type = (type == null ? typeof(T) : type);
            if(!dictionary.ContainsKey(type)) { dictionary[type] = new Dictionary<string, object>(); }

            dictionary[type][key] = value;
        }
        #endregion

        #region "Getters"
        /// /// <summary>
        /// 引数に渡したキーと対応する任意の型のデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static T Get<T>(string key, T defaultValue = default(T)) {
            return Get<T>(key, typeof(T), defaultValue);
        }

        public static T Get<T>(string key, Type type, T defaultValue) {
            return GetValue<T>(key, defaultValue, obj => {
                var serializer = new XmlSerializer(type);

                using(var sr = new StringReader((string)obj)) {
                    return (T)serializer.Deserialize(sr);
                }
            }, type);
        }

        /// <summary>
        /// 引数に渡したキーと対応するfloat型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static float GetFloat(string key, float defaultValue = default(float)) {
            return GetValue<float>(key, defaultValue, null);
        }

        /// <summary>
        /// 引数に渡したキーと対応するint型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static int GetInt(string key, int defaultValue = default(int)) {
            return GetValue<int>(key, defaultValue, null);
        }

        /// <summary>
        /// 引数に渡したキーと対応するstring型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static string GetString(string key, string defaultValue = "") {
            return GetValue<string>(key, defaultValue, null);
        }

        /// <summary>
        /// 引数に渡したキーと対応するbool型のデータを取得する
        /// </summary>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <returns>キーに対応するデータ</returns>
        public static bool GetBool(string key, bool defaultValue = default(bool)) {
            return GetValue<bool>(key, defaultValue, null);
        }

        /// <summary>
        /// キー情報を元に内部保持しているデータ郡から対応するデータを取得する
        /// </summary>
        /// <typeparam name="T">取得するデータの型</typeparam>
        /// <param name="key">取得するデータのキー</param>
        /// <param name="defaultValue">キーに対応するデータが存在しなかった時の返り値</param>
        /// <param name="converter">見つかったデータをobject型から本来のデータの型に変換する変換器</param>
        /// <returns>キーに対応するデータ</returns>
        private static T GetValue<T>(string key, T defaultValue, Func<object, T> converter = null, Type type = null) {
            type = (type == null ? typeof(T) : type);
            return HasKey(key, type) ? (converter == null ? (T)dictionary[type][key] : converter(dictionary[type][key])) : defaultValue;
        }
        #endregion

        /// <summary>
        /// 保存したファイルから情報を読み込む
        /// </summary>
        /// <returns>読み込んだ情報</returns>
        private static ExDictionary Load() {
            if(!File.Exists(FullPath)) { return new ExDictionary(); }

            using(var sr = new StreamReader(FullPath, encode)) {
                return ConvertList2ExDictionary((List<SaveDataElement>)serializer.Deserialize(sr));
            }
        }

        /// <summary>
        /// ファイルに保存するための形式から内部的に保持するための形式に変換する
        /// </summary>
        /// <param name="list">変換するデータ群</param>
        /// <returns>形式を変換した情報</returns>
        private static ExDictionary ConvertList2ExDictionary(List<SaveDataElement> list) {
            dictionary = new Dictionary<Type, Dictionary<string, object>>();
            list.ForEach(e => SetValue(e.Key, e.Value, e.ValueType));

            return dictionary;
        }

        /// <summary>
        /// 内部的に保持するための形式からファイルに保存するための形式に変換する
        /// </summary>
        /// <param name="dic">変換するデータ群</param>
        /// <returns>形式を変換した情報</returns>
        private static List<SaveDataElement> ConvertExDictionary2List(ExDictionary dic) {
            var list = new List<SaveDataElement>();
            foreach(var pair in dic) {
                foreach(var e in pair.Value) { list.Add(new SaveDataElement(e.Key, e.Value, pair.Key)); }
            }

            return list;
        }

        private static string ChangePath4Platform(string path) {
            var pf = Application.platform;
            var target = ((pf == RuntimePlatform.WindowsEditor || pf == RuntimePlatform.WindowsPlayer) ? '/' : '\\');

            return path.Replace(target, Path.DirectorySeparatorChar);
        }
    }
}
