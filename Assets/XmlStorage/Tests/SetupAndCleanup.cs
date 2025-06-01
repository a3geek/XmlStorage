using System.IO;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace XmlStorage.Tests
{
    [PrebuildSetup(typeof(SetupAndCleanup))]
    [PostBuildCleanup(typeof(SetupAndCleanup))]
    public class SetupAndCleanup : IPrebuildSetup, IPostBuildCleanup
    {
        public void Setup()
        {
            Storage.DirectoryPath = Path.Combine(Storage.GetDefaultDirectoryPath(), "XmlTests");
        }

        public void Cleanup()
        {
            var path = Path.Combine(Storage.GetDefaultDirectoryPath(), "XmlTests");
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }

            Storage.DirectoryPath = Storage.GetDefaultDirectoryPath();
        }

        [Test]
        public void Build()
        {
            Assert.That(true, Is.True);
        }
    }
}
