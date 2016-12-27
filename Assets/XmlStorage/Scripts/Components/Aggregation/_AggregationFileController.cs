using System.IO;
using System.Text;

namespace XmlStorage.Components {
    public sealed partial class Aggregation {
        private class StringWriterEncode : StringWriter {
            public override Encoding Encoding {
                get {
                    return this.encode;
                }
            }

            private Encoding encode = Encoding.UTF8;


            public StringWriterEncode() : base() {; }
            
            public StringWriterEncode(Encoding encode) : this() {
                this.encode = (encode == null ? this.encode : encode);
            }
        }

        private string Adjust4FileName(string fileName, string defaultValue = null) {
            if(string.IsNullOrEmpty(fileName)) {
                return string.IsNullOrEmpty(defaultValue) ? this.FileName : defaultValue;
            }
            
            return fileName.EndsWith(this.Extension) ? fileName : fileName + this.Extension;
        }

        private string Adjust4Extension(string extension, string defaultValue = null) {
            if(string.IsNullOrEmpty(extension)) {
                return string.IsNullOrEmpty(defaultValue) ? this.Extension : defaultValue;
            }
            
            return extension.StartsWith(".") ? extension : "." + extension;
        }

        private string Adjust4DirectoryPath(string directoryPath, string defaultValue = null) {
            if(string.IsNullOrEmpty(directoryPath)) {
                return string.IsNullOrEmpty(defaultValue) ? this.DirectoryPath : this.ChangeSeparatorChar(defaultValue);
            }
            directoryPath = this.ChangeSeparatorChar(directoryPath);

            if(!Directory.Exists(directoryPath)) { Directory.CreateDirectory(directoryPath); }
            directoryPath = directoryPath.TrimEnd(Path.DirectorySeparatorChar);
            
            return directoryPath;
        }

        private string ChangeSeparatorChar(string path) {
            if(path == null) { return null; }

            path = path.Replace('\\', '/');
            return path.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
