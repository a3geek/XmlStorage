using System.IO;
using System.Text;

namespace XmlStorage.Utils
{
    using Extensions;

    internal static class Consts
    {
        public static readonly char Separator = Path.DirectorySeparatorChar;
        public static readonly string Extension = ".xml";
        public static readonly string FileSearchPattern = "*" + Extension;
        public static readonly UTF8Encoding Encode = new(false);
        public static readonly string DataGroupName = nameof(XmlStorage);

        public static readonly string SaveDirectoryName
            = "Saves".AdjustAsDirectoryPath(creatable: false);
        public static readonly string SaveFileName = nameof(XmlStorage) + Extension;
        public static readonly string[] SaveDirectoryPaths = new[]
        {
#if UNITY_EDITOR

#else
            (SaveDirectoryOriginPath + ".." + Separator + SaveDirectoryName).AdjustAsDirectoryPath(creatable: false),
            
#endif
            (SaveDirectoryOriginPath + SaveDirectoryName).AdjustAsDirectoryPath(creatable: false)
        };

        private static readonly string SaveDirectoryOriginPath
            =
#if UNITY_EDITOR
                Directory.GetCurrentDirectory()
#else
                System.AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\')
#endif
                + Separator;
    }
}
