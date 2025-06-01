using NUnit.Framework;

namespace XmlStorage.Tests
{
    public class PrimitiveTypeTests
    {
        [SetUp]
        public void Setup()
        {
            Storage.CurrentDataGroupName = "PrimitiveTypes";
        }

        [TestCase(42)]
        [TestCase(-1)]
        public void IntTest(int value)
        {
            Storage.SetInt("intKey", value);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(value, Storage.GetInt("intKey"));
        }

        [TestCase(3.14f)]
        public void FloatTest(float value)
        {
            Storage.SetFloat("floatKey", value);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(value, Storage.GetFloat("floatKey"));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void BoolTest(bool value)
        {
            Storage.SetBool("boolKey", value);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(value, Storage.GetBool("boolKey"));
        }

        [TestCase("ABC_abc")]
        public void StringTest(string value)
        {
            Storage.SetString("stringKey", value);
            Storage.Save();
            Storage.Load();
            Assert.AreEqual(value, Storage.GetString("stringKey"));
        }
    }
}
