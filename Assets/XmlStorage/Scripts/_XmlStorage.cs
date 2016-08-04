using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;


namespace XmlStorage {
    using Components;
    using SerializeType = List<Components.DataSet>;

    /// <summary>セットしたデータ群をXML形式で保存する</summary>
    public static partial class XmlStorage {
        public const string DefaultAggregationName = "Default";

        /// <summary>保存する時のファイル名</summary>
        public static string FileName {
            get { return CurrentAggregation.FileName; }
            set { CurrentAggregation.FileName = value; }
        }
        /// <summary>保存する時のファイル拡張子</summary>
        public static string Extension {
            get { return CurrentAggregation.Extension; }
            set { CurrentAggregation.Extension = value; }
        }
        /// <summary>保存するファイルを置くフォルダ</summary>
        public static string DirectoryPath {
            get { return CurrentAggregation.DirectoryPath; }
            set { CurrentAggregation.DirectoryPath = value; }
        }
        /// <summary>保存する時のファイル名(拡張子なし)</summary>
        public static string FileNameWithoutExtension { get { return CurrentAggregation.FileNameWithoutExtension; } }
        /// <summary>保存する時のフルファイルパス</summary>
        public static string FullPath { get { return CurrentAggregation.FullPath; } }
        
        public static string CurrentAggregationName { get; private set; }
        private static Dictionary<string, Aggregation> aggregations = new Dictionary<string, Aggregation>();
        private static Aggregation CurrentAggregation { get { return aggregations[CurrentAggregationName]; } }

        private static FilePathStorage filePathStorage = new FilePathStorage();

        /// <summary>データをXMLにシリアライズするためのシリアライザーインスタンス</summary>
        private readonly static XmlSerializer serializer = new XmlSerializer(typeof(SerializeType));
        /// <summary>ファイルに保存する際のエンコード情報</summary>
        private readonly static UTF8Encoding encode = new UTF8Encoding(false);


        /// <summary>静的コンストラクタ</summary>
        static XmlStorage() {
            aggregations = Load();
            CurrentAggregationName = DefaultAggregationName;
        }

        public static void ChangeAggregation(string aggregationName) {
            if(!aggregations.ContainsKey(aggregationName)) { aggregations.Add(aggregationName, new Aggregation(null, aggregationName)); }
            CurrentAggregationName = aggregationName;
        }

        private static void Action4ChosenAggregation(string aggregationName, Action<Aggregation> action) {
            Action4ChosenAggregation(aggregationName, agg => {
                action(agg);
                return false;
            });
        }

        private static T Action4ChosenAggregation<T>(string aggregationName, Func<Aggregation, T> func) {
            if(HasAggregation(aggregationName)) {
                return func(aggregations[aggregationName]);
            }
            else {
                return func(CurrentAggregation);
            }
        }

        public static bool DeleteAggregation(string aggregationName) {
            if(aggregationName == DefaultAggregationName) { return false; }
            if(HasAggregation(aggregationName)) { return aggregations.Remove(aggregationName); }

            return false;
        }

        public static bool HasAggregation(string aggregationName) {
            return (string.IsNullOrEmpty(aggregationName) ? false : aggregations.ContainsKey(aggregationName));
        }
        
        private static Dictionary<string, SerializeType> Aggregations2Dictionary4FileControl(Dictionary<string, Aggregation> aggregations) {
            var dic = new Dictionary<string, SerializeType>();

            foreach(var pair in aggregations) {
                var agg = pair.Value;
                var dataset = new DataSet(agg.AggregationName, agg.FileName, agg.Extension, agg.DirectoryPath, agg.GetDataAsList());
                
                if(!dic.ContainsKey(agg.FullPath)) { dic.Add(agg.FullPath, new SerializeType()); }

                dic[agg.FullPath].Add(dataset);
            }

            return dic;
        }

        private static Dictionary<string, Aggregation> DataSetsList2Aggregations(SerializeType sets) {
            var dic = new Dictionary<string, Aggregation>();

            foreach(var set in sets) {
                if(!dic.ContainsKey(set.AggregationName)) { dic.Add(set.AggregationName, new Aggregation(set.Elements, set.AggregationName)); }
            }

            return dic;
        }

        /// <summary>セットした全てのデータを消去する</summary>
        public static void DeleteAll(string aggregationName = null) {
            Action4ChosenAggregation(aggregationName, agg => agg.DeleteAll());
        }
        
        /// <summary>
        /// 引数に渡したキーと一致するデータを全て消去する
        /// </summary>
        /// <param name="key">消去するデータ郡のキー</param>
        public static bool DeleteKey(string key, string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.DeleteKey(key));
        }

        /// <summary>
        /// 引数に渡したキーと型に一致するデータを消去する
        /// </summary>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="type">消去するデータの型</param>
        public static bool DeleteKey(string key, Type type, string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.DeleteKey(key, type));
        }
        
        /// <summary>
        /// 引数に渡したキーと一致するデータが一つでも存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータ群のキー</param>
        /// <returns>検索結果</returns>
        public static bool HasKey(string key, string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.HasKey(key));
        }

        /// <summary>
        /// 引数に渡したキーと型に一致するデータが存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="type">検索するデータの型</param>
        /// <returns>検索結果</returns>
        public static bool HasKey(string key, Type type, string aggregationName = null) {
            return Action4ChosenAggregation(aggregationName, agg => agg.HasKey(key, type));
        }

        /// <summary>セットしたデータ群をXML形式でファイルに保存する</summary>
        public static void Save() {
            var dic = Aggregations2Dictionary4FileControl(aggregations);
            filePathStorage.ClearFilePaths();

            foreach(var pair in dic) {
                using(var sw = new StreamWriter(pair.Key, false, encode)) {
                    var serializer = new XmlSerializer(typeof(SerializeType));
                    serializer.Serialize(sw, pair.Value);
                }
                
                filePathStorage.AddFilePath(pair.Key);
            }

            filePathStorage.Save();
        }

        /// <summary>
        /// 保存したファイルから情報を読み込む
        /// </summary>
        /// <returns>読み込んだ情報</returns>
        private static Dictionary<string, Aggregation> Load() {
            var aggs = new Dictionary<string, Aggregation>();
            var filePaths = filePathStorage.Load();
            
            if(filePaths.Count <= 0) {
                aggs.Add(DefaultAggregationName, new Aggregation(null, DefaultAggregationName));

                return aggs;
            }

            foreach(var filePath in filePaths) {
                if(!File.Exists(filePath)) { continue; }

                using(var sr = new StreamReader(filePath, encode)) {
                    foreach(var pair in DataSetsList2Aggregations((SerializeType)serializer.Deserialize(sr))) {
                        aggs.Add(pair.Key, pair.Value);
                    }
                }
            }

            if(aggs.Count <= 0) { aggs.Add(DefaultAggregationName, new Aggregation(null, DefaultAggregationName)); }

            return aggs;
        }
    }
}
