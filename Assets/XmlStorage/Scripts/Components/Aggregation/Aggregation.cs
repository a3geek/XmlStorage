using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace XmlStorage.Components
{
    using ExDictionary = Dictionary<Type, Dictionary<string, object>>;

    /// <summary>
    /// データ群を集団としてまとめて管理する
    /// </summary>
    [Serializable]
    public sealed partial class Aggregation
    {
        /// <summary>データ群を保存する時のファイル名</summary>
        public string FileName
        {
            get { return this.fileName; }
            set { this.fileName = this.Adjust4FileName(value, this.fileName); }
        }
        /// <summary>データ群を保存する時のファイルの拡張子</summary>
        public string Extension
        {
            get { return this.extension; }
            set { this.extension = this.Adjust4Extension(value, this.extension); }
        }
        /// <summary>データ群を保存するファイルを格納するフォルダ</summary>
        public string DirectoryPath
        {
            get { return this.directoryPath; }
            set { this.directoryPath = this.Adjust4DirectoryPath(value, this.directoryPath); }
        }
        /// <summary>データ群を保存する時のファイル名(拡張子なし)</summary>
        public string FileNameWithoutExtension
        {
            get { return this.FileName.TrimEnd(this.Extension.ToCharArray()); }
        }
        /// <summary>データ群を保存する時のフルパス</summary>
        public string FullPath
        {
            get { return this.DirectoryPath + Path.DirectorySeparatorChar + this.FileName; }
        }
        /// <summary>集団名</summary>
        public string AggregationName { get; private set; }
        /// <summary>全ての型をシリアライズして保存するかどうか</summary>
        /// <remarks>falseの時は<see cref="Type.IsSerializable"/>によってシリアライズするかどうかを判断する</remarks>
        public bool IsAllTypesSerialize { get; private set; }

        /// <summary>保存する時のファイル名</summary>
        private string fileName = "XmlStorage.xml";
        /// <summary>保存する時のファイルの拡張子</summary>
        private string extension = ".xml";
        /// <summary>保存するファイルを格納するフォルダ</summary>
        private string directoryPath = Application.persistentDataPath;
        /// <summary>データ群</summary>
        private ExDictionary dictionary = new ExDictionary();


        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="elements">初期データ群</param>
        /// <param name="aggregationName">集団名</param>
        /// <param name="isAllTypesSerialize">全ての型をシリアライズして保存するかどうか</param>
        public Aggregation(List<DataElement> elements, string aggregationName, bool isAllTypesSerialize = false)
        {
            this.IsAllTypesSerialize = isAllTypesSerialize;

            if(elements != null)
            {
                this.SetData(elements);
            }
            this.AggregationName = (string.IsNullOrEmpty(aggregationName) ? Guid.NewGuid().ToString() : aggregationName);

            this.Extension = ".xml";
            this.FileName = SceneManager.GetActiveScene().name + this.Extension;
            this.DirectoryPath = Application.persistentDataPath;
        }

        /// <summary>
        /// セットした全てのデータを消去する
        /// </summary>
        public void DeleteAll()
        {
            this.dictionary.Clear();
        }

        /// <summary>
        /// キーと一致するデータを全て消去する
        /// </summary>
        /// <param name="key">消去するデータのキー</param>
        /// <returns>消去に成功したかどうか</returns>
        public bool DeleteKey(string key)
        {
            var flag = false;

            foreach(var pair in this.dictionary)
            {
                if(pair.Value.Remove(key))
                {
                    flag = true;
                }
            }

            return flag;
        }

        /// <summary>
        /// キーと型に一致するデータを消去する
        /// </summary>
        /// <param name="key">消去するデータのキー</param>
        /// <param name="type">消去するデータの型</param>
        /// <returns>消去に成功したかどうか</returns>
        public bool DeleteKey(string key, Type type)
        {
            return this.dictionary[type].Remove(key);
        }

        /// <summary>
        /// キーと一致するデータが一つでも存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータのキー</param>
        /// <returns>存在するかどうか</returns>
        public bool HasKey(string key)
        {
            foreach(var pair in this.dictionary)
            {
                if(this.HasKey(key, pair.Key))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// キーと型に一致するデータが存在するかどうかを検索する
        /// </summary>
        /// <param name="key">検索するデータのキー</param>
        /// <param name="type">検索するデータの型</param>
        /// <returns>存在するかどうか</returns>
        public bool HasKey(string key, Type type)
        {
            return this.dictionary.ContainsKey(type) && this.dictionary[type].ContainsKey(key);
        }

        /// <summary>
        /// 現在所属する全てのデータを消去して新規データ群を所属させる
        /// </summary>
        /// <param name="list">データ群</param>
        public void SetData(List<DataElement> list)
        {
            this.DeleteAll();
            this.AddData(list);
        }

        /// <summary>
        /// 新しいデータ群を所属させる
        /// </summary>
        /// <param name="list">データ群</param>
        public void AddData(List<DataElement> list)
        {
            list.ForEach(e =>
            {
                var vt = e.ValueType;

                if(!this.dictionary.ContainsKey(vt))
                { this.dictionary[vt] = new Dictionary<string, object>(); }

                if(this.IsAllTypesSerialize || !vt.IsSerializable)
                {
                    this.dictionary[vt][e.Key] = this.Deserialize(e.Value, vt);
                }
                else
                {
                    this.dictionary[vt][e.Key] = e.Value;
                }
            });
        }

        /// <summary>
        /// 所属するデータ群を<see cref="DataElement"/>型の情報群に変換する
        /// </summary>
        /// <remarks><paramref name="encode"/>がnullの時は<see cref="Encoding.UTF8"/>を使う</remarks>
        /// <param name="encode">シリアライズ時のエンコード</param>
        /// <returns>情報群</returns>
        public List<DataElement> GetDataAsDataElements(Encoding encode = null)
        {
            var list = new List<DataElement>();
            encode = (encode == null ? Encoding.UTF8 : encode);

            foreach(var pair in this.dictionary)
            {
                foreach(var e in pair.Value)
                {
                    if(this.IsAllTypesSerialize)
                    {
                        list.Add(this.Object2DataElement(e.Value, e.Key, pair.Key, true, encode));
                    }
                    else
                    {
                        list.Add(this.Object2DataElement(e.Value, e.Key, pair.Key, !e.Value.GetType().IsSerializable, encode));
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// オブジェクトを<see cref="DataElement"/>型に変換する
        /// </summary>
        /// <param name="value">オブジェクト</param>
        /// <param name="key">データのキー</param>
        /// <param name="type">データの型</param>
        /// <param name="serialize">シリアライズするかどうか</param>
        /// <param name="encode">シリアライズ時のエンコード</param>
        /// <returns>保存時用の情報</returns>
        private DataElement Object2DataElement(object value, string key, Type type, bool serialize = false, Encoding encode = null)
        {
            if(serialize)
            {
                var serializer = new XmlSerializer(type);

                using(var sw = new StringWriterEncode(encode))
                {
                    serializer.Serialize(sw, value);

                    return new DataElement(key, sw.ToString(), type);
                }
            }

            return new DataElement(key, value, type);
        }

        /// <summary>
        /// オブジェクトをデシリアライズする
        /// </summary>
        /// <param name="value">デシリアライズするオブジェクト</param>
        /// <param name="type">オブジェクトの本来の型</param>
        /// <returns>デシリアライズした値</returns>
        private object Deserialize(object value, Type type)
        {
            return this.Deserialize(value.ToString(), type);
        }

        /// <summary>
        /// オブジェクトをデシリアライズする
        /// </summary>
        /// <param name="value">デシリアライズするオブジェクト</param>
        /// <param name="type">オブジェクトの本来の型</param>
        /// <returns>デシリアライズした値</returns>
        private object Deserialize(string value, Type type)
        {
            var serializer = new XmlSerializer(type);

            using(var sr = new StringReader(value))
            {
                return serializer.Deserialize(sr);
            }
        }
    }
}
