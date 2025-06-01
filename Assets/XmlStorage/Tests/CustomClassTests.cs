using System;
using NUnit.Framework;
using UnityEngine;
using System.Xml.Serialization;

namespace XmlStorage.Tests
{
    public class CustomClassTests
    {
        [SetUp]
        public void Setup()
        {
            Storage.CurrentDataGroupName = "CustomClass";
        }

        [Test]
        public void CustomObjectTest()
        {
            var obj = new CustomData { Id = 5, Name = "Data", Pos = Vector2.one };
            Storage.Set("obj", obj);
            Storage.Save();
            Storage.Load();

            var result = Storage.Get("obj", new CustomData());
            Assert.AreEqual(obj.Id, result.Id);
            Assert.AreEqual(obj.Name, result.Name);
            Assert.AreEqual(obj.Pos, result.Pos);
        }

        [Test]
        public void NestedCustomObjectTest()
        {
            var obj = new Parent
            {
                Title = "P",
                Child = new CustomData { Id = 1, Name = "C", Pos = new Vector2(3, 4) }
            };
            Storage.Set("nest", obj);
            Storage.Save();
            Storage.Load();

            var result = Storage.Get("nest", new Parent());
            Assert.AreEqual(obj.Title, result.Title);
            Assert.AreEqual(obj.Child.Id, result.Child.Id);
        }

        [Test]
        public void XmlIgnoreTest()
        {
            var obj = new WithIgnore { Value = "Yes", Ignore = "No" };
            Storage.Set("ign", obj);
            Storage.Save();
            Storage.Load();

            var res = Storage.Get("ign", new WithIgnore());
            Assert.AreEqual("Yes", res.Value);
            Assert.IsNull(res.Ignore);
        }


        [Serializable]
        public class CustomData
        {
            public int Id;
            public string Name;
            public Vector2 Pos;
        }

        [Serializable]
        public class WithIgnore
        {
            public string Value;

            [XmlIgnore]
            public string Ignore;
        }

        [Serializable]
        public class Parent
        {
            public string Title;
            public CustomData Child;
        }
    }
}
