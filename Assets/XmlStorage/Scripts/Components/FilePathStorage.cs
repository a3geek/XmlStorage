using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace XmlStorage.Components {
    /// <summary>
    /// データを保存している全ファイルのパスを管理する
    /// </summary>
    public sealed class FilePathStorage  {
        /// <summary>パスを保存するファイルの絶対パス</summary>
        public string FullPath { get { return this.DirectoryPath + Path.DirectorySeparatorChar + this.FileName + this.Extension; } }
        /// <summary>パスを保存するファイルを格納するフォルダ</summary>
        public string DirectoryPath { get; private set; }
        /// <summary>パスを保存するファイル名</summary>
        public string FileName { get; private set; }
        /// <summary>パスを保存するファイルの拡張子</summary>
        public string Extension { get; private set; }

        /// <summary>データを保存している全ファイルのパス</summary>
        private List<string> filePaths = new List<string>();
        /// <summary>ファイルに保存する際のエンコード情報</summary>
        private UTF8Encoding encode = new UTF8Encoding(false);
        

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FilePathStorage() : this(Application.persistentDataPath, "FilePaths", ".txt") {; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="directoryPath">パスを保存するファイルを格納するフォルダ</param>
        /// <param name="fileName">パスを保存するファイル名</param>
        /// <param name="extension">パスを保存するファイルの拡張子</param>
        public FilePathStorage(string directoryPath, string fileName, string extension) {
            this.DirectoryPath = directoryPath.TrimEnd(Path.DirectorySeparatorChar);
            this.FileName = fileName.TrimEnd('.');
            this.Extension = (extension.StartsWith(".") ? extension : "." + extension);
        }

        /// <summary>
        /// 保存するファイルパスを追加する
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        public void AddFilePath(string filePath) {
            this.filePaths.Add(filePath);
        }

        /// <summary>
        /// 保存設定をしたファイルパスを消去する
        /// </summary>
        /// <param name="filePath">ファイルパス</param>
        /// <returns>消去に成功したかどうか</returns>
        public bool RemoveFilePath(string filePath) {
            return this.filePaths.Remove(filePath);
        }
        
        /// <summary>
        /// 保存設定した全てのファイルパスを消去する
        /// </summary>
        public void ClearFilePaths() {
            this.filePaths.Clear();
        }

        /// <summary>
        /// ファイルパスを保存する
        /// </summary>
        public void Save() {
            this.Save(null);
        }

        /// <summary>
        /// ファイルパスを保存する
        /// </summary>
        /// <param name="filePaths">追加で保存するファイルパス</param>
        public void Save(List<string> filePaths) {
            if(!Directory.Exists(this.DirectoryPath)) { Directory.CreateDirectory(this.DirectoryPath); }
            if(filePaths != null) { this.filePaths.AddRange(filePaths); }

            File.WriteAllLines(this.FullPath, this.filePaths.ToArray(), this.encode);
        }
        
        /// <summary>
        /// ファイルパスを読み込む
        /// </summary>
        /// <returns>ファイルパス一覧</returns>
        public List<string> Load() {
            if(!Directory.Exists(this.DirectoryPath)) { return new List<string>(); }
            if(!File.Exists(this.FullPath)) { return new List<string>(); }

            return File.ReadAllLines(this.FullPath, this.encode).ToList();
        }
    }
}
