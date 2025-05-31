using XmlStorage.Utilities;
using XmlStorage.Utilities.Extensions;

namespace XmlStorage.Data
{
    public readonly ref partial struct FilePath
    {
        public sealed class DirectoryPathEntry : FilePathEntry
        {
            public string Path
            {
                get => this.Value;
                set => this.Value = value;
            }


            public DirectoryPathEntry(string directoryPath) : base(directoryPath, Const.SaveDirectoryPaths[0]) { }

            protected override string Validate(string input) => input.AdjustAsDirectoryPath(creatable: false);
        }

        public sealed class FileNameEntry : FilePathEntry
        {
            public string Name
            {
                get => this.Value;
                set => this.Value = value;
            }


            public FileNameEntry(string fileName) : base(fileName, Const.SaveFileName) { }

            protected override string Validate(string input) => input.AdjustAsFileName();
        }

        public abstract class FilePathEntry
        {
            protected string Value
            {
                get => this.entry;
                set
                {
                    var v = this.Validate(value);
                    this.entry = string.IsNullOrEmpty(v) ? this.Value : v;
                }
            }

            private string entry = null;


            protected FilePathEntry(string value, string defaultValue)
            {
                this.Value = string.IsNullOrEmpty(value) ? defaultValue : value;
            }

            protected abstract string Validate(string input);
        }
    }
}
