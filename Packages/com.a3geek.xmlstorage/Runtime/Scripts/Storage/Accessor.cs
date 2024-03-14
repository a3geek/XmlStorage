using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace XmlStorage
{
    using Systems;
    using Systems.Aggregations;
    using Systems.Utilities;
    using Data;
    using Utils;
    using Utils.Extensions;
    using XmlData;

    public static partial class Storage
    {
        private static void SetValue<T>(string key, T value)
        {
            var group = CurrentDataGroup;
            group.Update(key, value);
        }
    }
}
