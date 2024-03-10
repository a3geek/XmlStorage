using System.IO;
using System.Text;
using XmlStorage.Utils.Extensions;

namespace XmlStorage.Utils
{
    public static class Consts
    {
        public static readonly char Separator = Path.DirectorySeparatorChar;
        public static readonly string Extension = ".xml";
        public static readonly string FileSearchPattern = "*" + Extension;
        public static readonly UTF8Encoding Encode = new(false);
        public static readonly string DataGroupName = nameof(XmlStorage);

        public static readonly string SaveDirectoryName
            = "Saves".AdjustAsDirectoryPath(creatable: false);
        public static readonly string[] SaveDirectoryPaths = new []
        {
            (SaveDirectoryOriginPath + ".." + Separator + SaveDirectoryName).AdjustAsDirectoryPath(creatable: false),
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
