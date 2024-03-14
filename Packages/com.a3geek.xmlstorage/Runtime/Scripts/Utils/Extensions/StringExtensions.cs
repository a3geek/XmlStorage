using System;
using System.IO;
using System.Reflection;

namespace XmlStorage.Utils.Extensions
{
    internal static class StringExtensions
    {
        public static string AdjustAsFileName(this string fileName)
        {
            return string.IsNullOrEmpty(fileName)
                ? ""
                : fileName.EndsWith(Consts.Extension) ? fileName : (fileName + Consts.Extension);
        }

        public static string AdjustAsDirectoryPath(this string directoryPath, bool creatable = true)
        {
            if(string.IsNullOrEmpty(directoryPath))
            {
                return null;
            }

            var path = directoryPath.AdjustSeparateCharAsPath();
            if(Directory.Exists(path) == false && creatable == true)
            {
                Directory.CreateDirectory(path);
            }

            return path.EndsWith(Consts.Separator)
                ? path
                : (path + Consts.Separator);
        }

        public static string AdjustSeparateCharAsPath(this string path)
        {
            return string.IsNullOrEmpty(path)
                ? ""
                : path.Replace('\\', '/').Replace('/', Consts.Separator);
        }

        // https://answers.unity.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        public static Type GetTypeAsTypeName(this string typeName)
        {
            var type = Type.GetType(typeName);
            if(type != null)
            {
                return type;
            }

            if(typeName.Contains("."))
            {
                var assemblyName = typeName[..typeName.IndexOf('.')];
                try
                {
                    var assembly = Assembly.Load(assemblyName);

                    type = assembly.GetType(typeName);
                    if(type != null)
                    {
                        return type;
                    }
                }
                catch
                {
                    return null;
                }
            }

            var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            foreach(var assemblyName in referencedAssemblies)
            {
                try
                {
                    var assembly = Assembly.Load(assemblyName);

                    type = assembly.GetType(typeName);
                    if(type != null)
                    {
                        return type;
                    }
                }
                catch
                {
                    continue;
                }
            }

            return null;
        }
    }
}
