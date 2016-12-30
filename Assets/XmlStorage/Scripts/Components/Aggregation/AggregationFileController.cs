using System.IO;
using System.Text;

namespace XmlStorage.Components
{
    public sealed partial class Aggregation
    {
        /// <summary>
        /// <see cref="StringWriter"/>を指定したエンコードで保存する
        /// </summary>
        private class StringWriterEncode : StringWriter
        {
            /// <summary>
            /// 保存する時のエンコード
            /// </summary>
            public override Encoding Encoding
            {
                get
                {
                    return this.encode;
                }
            }

            /// <summary>
            /// 保存する時のエンコード
            /// </summary>
            private Encoding encode = Encoding.UTF8;


            /// <summary>
            /// コンストラクタ
            /// </summary>
            public StringWriterEncode() : base() {; }

            /// <summary>
            /// コンストラクタ
            /// </summary>
            /// <param name="encode">保存する時のエンコード</param>
            public StringWriterEncode(Encoding encode) : this()
            {
                this.encode = (encode == null ? this.encode : encode);
            }
        }

        /// <summary>
        /// ファイル名として調節する
        /// </summary>
        /// <param name="fileName">調整するファイル名</param>
        /// <param name="defaultValue">ファイル名として問題がある時に返すデフォルト値</param>
        /// <returns>ファイル名</returns>
        private string Adjust4FileName(string fileName, string defaultValue = null)
        {
            if(string.IsNullOrEmpty(fileName))
            {
                return string.IsNullOrEmpty(defaultValue) ? this.FileName : defaultValue;
            }

            return fileName.EndsWith(this.Extension) ? fileName : fileName + this.Extension;
        }

        /// <summary>
        /// 拡張子として調節する
        /// </summary>
        /// <param name="extension">調節する拡張子</param>
        /// <param name="defaultValue">拡張子として問題がある時に返すデフォルト値</param>
        /// <returns>拡張子</returns>
        private string Adjust4Extension(string extension, string defaultValue = null)
        {
            if(string.IsNullOrEmpty(extension))
            {
                return string.IsNullOrEmpty(defaultValue) ? this.Extension : defaultValue;
            }

            return extension.StartsWith(".") ? extension : "." + extension;
        }

        /// <summary>
        /// フォルダパスとして調節する
        /// </summary>
        /// <param name="directoryPath">調節するフォルダパス</param>
        /// <param name="defaultValue">フォルダパスとして問題がある時に返すデフォルト値</param>
        /// <returns>フォルダパス</returns>
        private string Adjust4DirectoryPath(string directoryPath, string defaultValue = null)
        {
            if(string.IsNullOrEmpty(directoryPath))
            {
                return string.IsNullOrEmpty(defaultValue) ? this.DirectoryPath : this.ChangeSeparatorChar(defaultValue);
            }
            directoryPath = this.ChangeSeparatorChar(directoryPath);

            if(!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            directoryPath = directoryPath.TrimEnd(Path.DirectorySeparatorChar);

            return directoryPath;
        }

        /// <summary>
        /// パスの区切り文字を環境に合わせて調節する
        /// </summary>
        /// <param name="path">パス</param>
        /// <returns>調節したパス</returns>
        private string ChangeSeparatorChar(string path)
        {
            if(path == null)
            {
                return null;
            }

            path = path.Replace('\\', '/');
            return path.Replace('/', Path.DirectorySeparatorChar);
        }
    }
}
