using System.IO;
using XmlStorage.Utilities.Extensions;

namespace XmlStorage.Data
{
    public readonly ref partial struct FilePath
    {
        private const char Dot = '.';
        private static readonly char Separator = Path.DirectorySeparatorChar;

        public readonly string DirectoryPath;
        public readonly string FileName;
        public readonly string Extension;


        public FilePath(string directoryPath, string fileName, string extension)
        {
            this.DirectoryPath = directoryPath.AdjustAsDirectoryPath();
            this.FileName = fileName.AdjustAsFileName();
            this.Extension = extension;
        }

        public string GetFullPath()
        {
            var directory = this.DirectoryPath.TrimEnd(Separator);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var extension = Dot + this.Extension.TrimStart(Dot);
            return this.DirectoryPath + Separator + this.FileName + Separator + extension;
        }
    }
}
