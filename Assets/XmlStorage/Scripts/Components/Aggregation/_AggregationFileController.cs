using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;


namespace XmlStorage.Components {
    public sealed partial class Aggregation {
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
