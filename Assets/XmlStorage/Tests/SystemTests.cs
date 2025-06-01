using NUnit.Framework;

namespace XmlStorage.Tests
{
    public class SystemTests
    {
        [SetUp]
        public void Setup()
        {
            Storage.CurrentDataGroupName = "SystemTests";
        }

        [Test]
        public void HasKeyAndDeleteTest()
        {
            Storage.Set("temp", 42);
            Assert.IsTrue(Storage.HasKey("temp", typeof(int)));
            Storage.Delete("temp", typeof(int));
            Assert.IsFalse(Storage.HasKey("temp", typeof(int)));
        }

        [Test]
        public void KeyTypeTest()
        {
            Storage.Set("key", 10);
            Storage.Set("key", 0.1f);
            Storage.Set("key", "value");
            Storage.Save();
            Storage.Load();

            Assert.AreEqual(10, Storage.Get("key", 0));
            Assert.AreEqual(0.1f, Storage.Get("key", 0f));
            Assert.AreEqual("value", Storage.Get("key", ""));
        }

        [Test]
        public void WrongTypeTest()
        {
            Storage.Set("wrong", "123");
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(-1, Storage.Get("wrong", -1));
        }

        [Test]
        public void MultipleGroupsTest()
        {
            var group = Storage.CurrentDataGroupName;

            Storage.CurrentDataGroupName = "GroupA";
            Storage.Set("k", 1);

            Storage.CurrentDataGroupName = "GroupB";
            Storage.Set("k", 2);

            Storage.Save();
            Storage.Load();

            Storage.CurrentDataGroupName = "GroupA";
            Assert.AreEqual(1, Storage.Get("k", 0));

            Storage.CurrentDataGroupName = "GroupB";
            Assert.AreEqual(2, Storage.Get("k", 0));

            Storage.CurrentDataGroupName = group;
        }
    }
}
