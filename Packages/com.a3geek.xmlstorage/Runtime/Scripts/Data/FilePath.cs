using System.IO;

namespace XmlStorage.Data
{
    public readonly ref struct FilePath
    {
        private const char Dot = '.';
        private static readonly char Separator = Path.DirectorySeparatorChar;

        public readonly string DirectoryPath;
        public readonly string FileName;
        public readonly string Extension;


        public FilePath(string directoryPath, string fileName, string extension)
        {
            this.DirectoryPath = directoryPath;
            this.FileName = fileName;
            this.Extension = extension;
        }

        public string GetFullPath()
        {
            var directory = this.DirectoryPath.TrimEnd(Separator);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            return this.DirectoryPath + Separator + this.FileName + Dot + this.Extension.TrimStart(Dot);
        }
    }
}
