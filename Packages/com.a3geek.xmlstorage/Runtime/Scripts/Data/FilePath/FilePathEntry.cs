using XmlStorage.Utils;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Data
{
    public sealed partial class FilePath
    {
        public sealed class DirectoryPathEntry : FilePathEntry
        {
            public string Path
            {
                get => this.Value;
                set => this.Value = value;
            }


            public DirectoryPathEntry(in string directoryPath) : base(directoryPath, Const.SaveDirectoryPaths[0]) { }

            protected override string Validate(in string input) => input.AdjustAsDirectoryPath(creatable: false);
        }

        public sealed class FileNameEntry : FilePathEntry
        {
            public string Name
            {
                get => this.Value;
                set => this.Value = value;
            }


            public FileNameEntry(in string fileName) : base(fileName, Const.SaveFileName) { }

            protected override string Validate(in string input) => input.AdjustAsFileName();
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


            protected FilePathEntry(in string value, in string defaultValue)
            {
                this.Value = string.IsNullOrEmpty(value) ? defaultValue : value;
            }

            protected abstract string Validate(in string input);
        }
    }
}
