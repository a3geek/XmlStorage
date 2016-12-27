using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace XmlStorage.Components {
    public sealed class FilePathStorage  {
        public string FullPath { get { return this.DirectoryPath + Path.DirectorySeparatorChar + this.FileName + this.Extension; } }
        public string DirectoryPath { get; private set; }
        public string FileName { get; private set; }
        public string Extension { get; private set; }

        private List<string> filePaths = new List<string>();
        private UTF8Encoding encode = new UTF8Encoding(false);
        

        public FilePathStorage() : this(Application.persistentDataPath, "FilePaths", ".txt") {; }

        public FilePathStorage(string directoryPath, string fileName, string extension) {
            this.DirectoryPath = directoryPath.TrimEnd(Path.DirectorySeparatorChar);
            this.FileName = fileName.TrimEnd('.');
            this.Extension = (extension.StartsWith(".") ? extension : "." + extension);
        }

        public void AddFilePath(string filePath) { this.filePaths.Add(filePath); }

        public bool RemoveFilePath(string filePath) { return this.filePaths.Remove(filePath); }

        public void ClearFilePaths() { this.filePaths.Clear(); }

        public void Save() { this.Save(null); }

        public void Save(List<string> filePaths) {
            if(!Directory.Exists(this.DirectoryPath)) { Directory.CreateDirectory(this.DirectoryPath); }
            if(filePaths != null) { this.filePaths.AddRange(filePaths); }

            File.WriteAllLines(this.FullPath, this.filePaths.ToArray(), this.encode);
        }

        public List<string> Load() {
            if(!Directory.Exists(this.DirectoryPath)) { return new List<string>(); }
            if(!File.Exists(this.FullPath)) { return new List<string>(); }

            return File.ReadAllLines(this.FullPath, this.encode).ToList();
        }
    }
}
