using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace XmlStorage.Tests
{
    public class CollectionTests
    {
        [SetUp]
        public void Setup()
        {
            Storage.CurrentDataGroupName = "Collections";
        }

        [Test]
        public void ListTest()
        {
            var list = new List<Vector3> { Vector3.one, Vector3.zero };
            Storage.Set("list", list);
            Storage.Save();
            Storage.Load();
            CollectionAssert.AreEqual(list, Storage.Get("list", new List<Vector3>()));
        }

        [Test]
        public void ArrayTest()
        {
            var array = new[] { Vector2.one, Vector2.zero };
            Storage.Set("array", array);
            Storage.Save();
            Storage.Load();
            CollectionAssert.AreEqual(array, Storage.Get("array", Array.Empty<Vector2>()));
        }
    }
}
