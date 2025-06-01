using NUnit.Framework;
using UnityEngine;

namespace XmlStorage.Tests
{
    public class UnityTypeTests
    {
        [SetUp]
        public void Setup()
        {
            Storage.CurrentDataGroupName = "UnityTypes";
        }

        [Test]
        public void Vector2Test()
        {
            var val = new Vector2(1f, 2f);
            Storage.Set("v2", val);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(val, Storage.Get("v2", Vector2.zero));
        }

        [Test]
        public void QuaternionTest()
        {
            var val = Quaternion.Euler(0f, 90f, 0f);
            Storage.Set("q", val);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(val, Storage.Get("q", Quaternion.identity));
        }

        [Test]
        public void ColorTest()
        {
            var val = new Color(0.1f, 0.2f, 0.3f);
            Storage.Set("color", val);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(val, Storage.Get("color", Color.black));
        }
    }
}
