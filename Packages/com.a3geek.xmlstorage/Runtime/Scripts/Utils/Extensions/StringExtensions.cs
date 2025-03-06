using System;
using System.IO;
using System.Reflection;

namespace XmlStorage.Utils.Extensions
{
    internal static class StringExtensions
    {
        public static string AdjustAsFileName(this string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return string.Empty;
            }

            return fileName.EndsWith(Const.Extension) ? fileName : fileName + Const.Extension;
        }

        public static string AdjustAsDirectoryPath(this string directoryPath, bool creatable = true)
        {
            if (string.IsNullOrEmpty(directoryPath))
            {
                return null;
            }

            var path = directoryPath.AdjustSeparateCharAsPath();
            if (creatable && Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
            }

            return path.EndsWith(Const.Separator) ? path : path + Const.Separator;
        }

        public static string AdjustSeparateCharAsPath(this string path)
        {
            return string.IsNullOrEmpty(path) ? string.Empty : path.Replace('\\', '/').Replace('/', Const.Separator);
        }

        public static bool TryGetTypeAsTypeName(this string typeName, out Type type)
        {
            type = GetType(typeName, 0);
            return type != null;
        }

        // https://answers.unity.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        private static Type GetType(in string typeName, uint tryCount)
        {
            try
            {
                return tryCount switch
                {
                    0 => Type.GetType(typeName) ?? GetType(typeName, 1),
                    1 => FindFromAssemblyName(typeName) ?? GetType(typeName, 2),
                    2 => FindFromAssemblies(typeName) ?? GetType(typeName, 3),
                    _ => null
                };
            }
            catch
            {
                return GetType(typeName, tryCount + 1);
            }

            static Type FindFromAssemblyName(in string typeName)
            {
                if (!typeName.Contains("."))
                {
                    return null;
                }

                var assemblyName = typeName[..typeName.IndexOf('.')];
                var assembly = Assembly.Load(assemblyName);
                return assembly.GetType(typeName);
            }

            static Type FindFromAssemblies(in string typeName)
            {
                var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                foreach (var assemblyName in referencedAssemblies)
                {
                    var assembly = Assembly.Load(assemblyName);
                    var type = assembly.GetType(typeName);
                    if (type != null)
                    {
                        return type;
                    }
                }

                return null;
            }
        }
    }
}
