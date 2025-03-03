using System.IO;
using System.Text;

namespace XmlStorage.Utils
{
    using Extensions;

    internal static class Const
    {
        public static readonly char Separator = Path.DirectorySeparatorChar;
        public const string Extension = ".xml";
        public const string FileSearchPattern = "*" + Extension;
        public static readonly UTF8Encoding Encode = new(false);
        public const string DataGroupName = nameof(XmlStorage);

        public static readonly string SaveDirectoryName
            = "Saves".AdjustAsDirectoryPath(creatable: false);
        public const string SaveFileName = nameof(XmlStorage) + Extension;
        public static readonly string SaveDirectoryOriginPath
            =
        #if UNITY_EDITOR
            Directory.GetCurrentDirectory()
        #else
            System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
        #endif
            + Separator;
        public static readonly string[] SaveDirectoryPaths = new[]
        {
        #if !UNITY_EDITOR
            (SaveDirectoryOriginPath + ".." + Separator + SaveDirectoryName).AdjustAsDirectoryPath(creatable: false),
        #endif
            (SaveDirectoryOriginPath + SaveDirectoryName).AdjustAsDirectoryPath(creatable: false)
        };
    }
}
