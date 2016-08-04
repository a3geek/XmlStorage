using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Linq;
using System.Reflection;


namespace XmlStorage.Components {
    [Serializable]
    public sealed class DataSet {
        public string AggregationName { get; private set; }
        public string FileName { get; private set; }
        public string Extension { get; private set; }
        public string DirectoryPath { get; private set; }
        public List<DataElement> Elements { get; private set; }
        
        
        public DataSet() : this("", "", "", "", new List<DataElement>()) {
        }
        
        public DataSet(string aggregationName, string fileName, string extension, string directoryPath, List<DataElement> elements) {
            this.AggregationName = aggregationName;
            this.FileName = fileName;
            this.Extension = extension;
            this.DirectoryPath = directoryPath;
            this.Elements = (elements == null ? new List<DataElement>() : elements);
        }
    }
}
