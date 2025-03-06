using System.IO;

namespace XmlStorage.Data
{
    public sealed partial class FilePath
    {
        public string DirectoryPath
        {
            get => this.directory.Path;
            set
            {
                this.directory.Path = value;
                this.Set(this.directory, this.file);
            }
        }
        public string FileName
        {
            get => this.file.Name;
            set
            {
                this.file.Name = value;
                this.Set(this.directory, this.file);
            }
        }
        public string FullPath { get; private set; } = null;

        private DirectoryPathEntry directory = null;
        private FileNameEntry file = null;


        public FilePath() : this(null, null) { }

        public FilePath(string filePath) : this(Path.GetDirectoryName(filePath), Path.GetFileName(filePath)) { }

        public FilePath(in string directoryPath, in string fileName)
        {
            this.Set(new DirectoryPathEntry(directoryPath), new FileNameEntry(fileName));
        }

        public bool IsEquals(in FilePath other)
        {
            return this.FullPath == other.FullPath;
        }

        private void Set(in DirectoryPathEntry directoryPath, in FileNameEntry fileName)
        {
            this.directory = directoryPath;
            this.file = fileName;
            this.FullPath = this.DirectoryPath + this.FileName;
        }
    }
}
