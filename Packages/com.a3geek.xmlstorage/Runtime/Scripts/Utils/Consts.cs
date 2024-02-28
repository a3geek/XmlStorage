using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace XmlStorage.Utils
{
    public static class Consts
    {
        public static readonly char Separator = Path.DirectorySeparatorChar;
        public static readonly string Extension = ".xml";
        public static readonly UTF8Encoding Encode = new(false);
    }
}
