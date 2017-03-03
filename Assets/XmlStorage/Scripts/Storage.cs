using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace XmlStorage
{
    using Components;
    using SerializeType = List<Components.DataSet>;

    /// <summary>
    /// セットしたデータ群をXML形式で保存する
    /// </summary>
    public static partial class Storage
    {
        /// <summary>デフォルトの集団名</summary>
        public const string DefaultAggregationName = "Default";

        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のファイル名</summary>
        public static string FileName
        {
            set { CurrentAggregation.FileName = value; }
            get { return CurrentAggregation.FileName; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のファイル拡張子</summary>
        public static string Extension
        {
            set { CurrentAggregation.Extension = value; }
            get { return CurrentAggregation.Extension; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存するファイルを置くフォルダ</summary>
        public static string DirectoryPath
        {
            set { CurrentAggregation.DirectoryPath = value; }
            get { return CurrentAggregation.DirectoryPath; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のファイル名(拡張子なし)</summary>
        public static string FileNameWithoutExtension
        {
            get { return CurrentAggregation.FileNameWithoutExtension; }
        }
        /// <summary><see cref="CurrentAggregation"/>がデータ群を保存する時のフルパス</summary>
        public static string FullPath
        {
            get { return CurrentAggregation.FullPath; }
        }
        /// <summary>現在選択されている集団名</summary>
        public static string CurrentAggregationName
        {
            get; private set;
        }

        /// <summary>データをXMLにシリアライズするためのシリアライザーインスタンス</summary>
        private readonly static XmlSerializer serializer = new XmlSerializer(typeof(SerializeType));
        /// <summary>ファイルに保存する時のエンコード情報</summary>
        private readonly static UTF8Encoding encode = new UTF8Encoding(false);

        /// <summary>現在選択されている集団</summary>
        private static Aggregation CurrentAggregation
        {
            get { return aggregations[CurrentAggregationName]; }
        }
        /// <summary>集団群</summary>
        private static Dictionary<string, Aggregation> aggregations = new Dictionary<string, Aggregation>();
        /// <summary>全保存ファイルの管理</summary>
        private static FilePathStorage filePathStorage = new FilePathStorage();


        /// <summary>静的コンストラクタ</summary>
        static Storage()
        {
            aggregations = Load();
            CurrentAggregationName = DefaultAggregationName;
        }

        /// <summary>
        /// 別の集団を選択する
        /// <paramref name="aggregationName"/>集団が存在しない場合は新しく生成する
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        public static void ChangeAggregation(string aggregationName)
        {
            if(!aggregations.ContainsKey(aggregationName))
            {
                aggregations.Add(aggregationName, new Aggregation(null, aggregationName));
            }

            CurrentAggregationName = aggregationName;
        }

        /// <summary>
        /// 集団を消去する
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <returns>消去に成功したかどうか</returns>
        public static bool DeleteAggregation(string aggregationName)
        {
            if(aggregationName == DefaultAggregationName)
            {
                return false;
            }
            else if(HasAggregation(aggregationName))
            {
                return aggregations.Remove(aggregationName);
            }

            return false;
        }

        /// <summary>
        /// 集団が存在するかどうか
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <returns>存在するかどうか</returns>
        public static bool HasAggregation(string aggregationName)
        {
            return (string.IsNullOrEmpty(aggregationName) ? false : aggregations.ContainsKey(aggregationName));
        }

        /// <summary>
        /// セットした全てのデータを消去する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="aggregationName">データが所属する集団名</param>
        public static void DeleteAll(string aggregationName = null)
        {
            Action4ChosenAggregation(aggregationName, agg => agg.DeleteAll());
        }

        /// <summary>
        /// キーと一致するデータを全て消去する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>消去に成功したかどうか</returns>
        public static bool DeleteKey(string key, string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.DeleteKey(key));
        }

        /// <summary>
        /// キーと型に一致するデータを消去する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="type">消去するデータの型</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>消去に成功したかどうか</returns>
        public static bool DeleteKey(string key, Type type, string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.DeleteKey(key, type));
        }

        /// <summary>
        /// キーと一致するデータが一つでも存在するかどうかを検索する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>存在するかどうか</returns>
        public static bool HasKey(string key, string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.HasKey(key));
        }

        /// <summary>
        /// キーと型に一致するデータが存在するかどうかを検索する
        /// </summary>
        /// <remarks><paramref name="aggregationName"/>がnullの時は、<see cref="CurrentAggregationName"/>が使われる</remarks>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="type">検索するデータの型</param>
        /// <param name="aggregationName">データが所属する集団名</param>
        /// <returns>存在するかどうか</returns>
        public static bool HasKey(string key, Type type, string aggregationName = null)
        {
            return Action4ChosenAggregation(aggregationName, agg => agg.HasKey(key, type));
        }

        /// <summary>
        /// セットしたデータ群をXML形式でファイルに保存する
        /// </summary>
        /// <remarks>全ての集団のデータ群を保存する</remarks>
        public static void Save()
        {
            var dic = Aggregations2Dictionary4FileControl(aggregations);
            filePathStorage.ClearFilePaths();

            foreach(var pair in dic)
            {
                using(var sw = new StreamWriter(pair.Key, false, encode))
                {
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
        private static Dictionary<string, Aggregation> Load()
        {
            var aggs = new Dictionary<string, Aggregation>();
            var filePaths = filePathStorage.Load();

            if(filePaths.Count <= 0)
            {
                aggs.Add(DefaultAggregationName, new Aggregation(null, DefaultAggregationName));

                return aggs;
            }

            foreach(var filePath in filePaths)
            {
                if(!File.Exists(filePath))
                {
                    continue;
                }

                using(var sr = new StreamReader(filePath, encode))
                {
                    foreach(var pair in DataSetsList2Aggregations((SerializeType)serializer.Deserialize(sr)))
                    {
                        aggs.Add(pair.Key, pair.Value);
                    }
                }
            }

            if(aggs.Count <= 0)
            {
                aggs.Add(DefaultAggregationName, new Aggregation(null, DefaultAggregationName));
            }

            return aggs;
        }

        /// <summary>
        /// <paramref name="aggregationName"/>で指定した集団に対して処理を行う
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <param name="action">処理内容</param>
        private static void Action4ChosenAggregation(string aggregationName, Action<Aggregation> action)
        {
            Action4ChosenAggregation(aggregationName, agg =>
            {
                action(agg);
                return false;
            });
        }

        /// <summary>
        /// <paramref name="aggregationName"/>で指定した集団に対して処理を行い値を返す
        /// </summary>
        /// <typeparam name="T">返す値の型</typeparam>
        /// <param name="aggregationName">集団名</param>
        /// <param name="func">処理内容</param>
        /// <returns>返却値</returns>
        private static T Action4ChosenAggregation<T>(string aggregationName, Func<Aggregation, T> func)
        {
            if(HasAggregation(aggregationName))
            {
                return func(aggregations[aggregationName]);
            }
            else
            {
                return func(CurrentAggregation);
            }
        }

        /// <summary>
        /// 集団群をファイル保存用の情報群に変換する
        /// </summary>
        /// <param name="aggregations">集団群</param>
        /// <returns>保存用に変換した情報群</returns>
        private static Dictionary<string, SerializeType> Aggregations2Dictionary4FileControl(Dictionary<string, Aggregation> aggregations)
        {
            var dic = new Dictionary<string, SerializeType>();

            foreach(var pair in aggregations)
            {
                var agg = pair.Value;
                var dataset = new DataSet(agg.AggregationName, agg.FileName, agg.Extension, agg.DirectoryPath, agg.GetDataAsDataElements(encode));

                if(!dic.ContainsKey(agg.FullPath))
                {
                    dic.Add(agg.FullPath, new SerializeType());
                }

                dic[agg.FullPath].Add(dataset);
            }

            return dic;
        }

        /// <summary>
        /// 保存用の情報群から集団群に変換する
        /// </summary>
        /// <param name="sets">情報群</param>
        /// <returns>集団群</returns>
        private static Dictionary<string, Aggregation> DataSetsList2Aggregations(SerializeType sets)
        {
            var dic = new Dictionary<string, Aggregation>();

            foreach(var set in sets)
            {
                if(!dic.ContainsKey(set.AggregationName))
                {
                    dic.Add(set.AggregationName, new Aggregation(set.Elements, set.AggregationName, set.FullPath));
                }
            }

            return dic;
        }
    }
}
