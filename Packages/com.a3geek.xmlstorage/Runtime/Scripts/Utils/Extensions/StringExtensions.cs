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
                : fileName.EndsWith(Const.Extension)
                    ? fileName
                    : (fileName + Const.Extension);
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

            return path.EndsWith(Const.Separator)
                ? path
                : (path + Const.Separator);
        }

        public static string AdjustSeparateCharAsPath(this string path)
        {
            return string.IsNullOrEmpty(path)
                ? ""
                : path.Replace('\\', '/').Replace('/', Const.Separator);
        }

        public static Type GetTypeAsTypeName(this string typeName)
        {
            return GetType(typeName, 0);
        }

        // https://answers.unity.com/questions/206665/typegettypestring-does-not-work-in-unity.html
        private static Type GetType(in string typeName, int tryCount)
        {
            switch(tryCount)
            {
                case 0:
                {
                    var type = Type.GetType(typeName);
                    return type ?? GetType(typeName, 1);
                }
                case 1:
                {
                    var type = FindByAssemblyName(typeName);
                    return type ?? GetType(typeName, 2);
                }
                case 2:
                {
                    var type = FindFromAssemblies(typeName);
                    return type ?? GetType(typeName, 3);
                }
            }

            return null;

            static Type FindByAssemblyName(in string typeName)
            {
                if(!typeName.Contains("."))
                {
                    return null;
                }

                try
                {
                    var assemblyName = typeName[..typeName.IndexOf('.')];
                    var assembly = Assembly.Load(assemblyName);

                    return assembly.GetType(typeName);
                }
                catch
                {
                    return null;
                }
            }

            static Type FindFromAssemblies(in string typeName)
            {
                var referencedAssemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
                foreach(var assemblyName in referencedAssemblies)
                {
                    try
                    {
                        var assembly = Assembly.Load(assemblyName);

                        var type = assembly.GetType(typeName);
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
}
