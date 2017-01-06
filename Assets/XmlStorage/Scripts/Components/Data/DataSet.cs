using System;
using System.Collections.Generic;
using System.IO;

namespace XmlStorage.Components
{
    /// <summary>
    /// データ群をファイルに保存する時のデータ形式
    /// </summary>
    [Serializable]
    public sealed class DataSet
    {
        /// <summary>集団名</summary>
        public string AggregationName { get; private set; }
        /// <summary>ファイル名</summary>
        public string FileName { get; private set; }
        /// <summary>拡張子</summary>
        public string Extension { get; private set; }
        /// <summary>フォルダ名</summary>
        public string DirectoryPath { get; private set; }
        /// <summary>保存するデータ群</summary>
        public List<DataElement> Elements { get; private set; }
        /// <summary>フルパス</summary>
        public string FullPath
        {
            get { return this.DirectoryPath + Path.DirectorySeparatorChar + this.FileName; }
        }


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DataSet() : this("", "", "", "", new List<DataElement>()) {; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="aggregationName">集団名</param>
        /// <param name="fileName">ファイル名</param>
        /// <param name="extension">拡張子</param>
        /// <param name="directoryPath">フォルダ名</param>
        /// <param name="elements">保存するデータ群</param>
        public DataSet(string aggregationName, string fileName, string extension, string directoryPath, List<DataElement> elements)
        {
            this.AggregationName = aggregationName;
            this.FileName = fileName;
            this.Extension = extension;
            this.DirectoryPath = directoryPath;
            this.Elements = (elements == null ? new List<DataElement>() : elements);
        }
    }
}
