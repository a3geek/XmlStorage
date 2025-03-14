using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using XmlStorage;

namespace XmlStorage.Tests
{
    public class UnitTests
    {
        [Test]
        public void Save()
        {
            try
            {
                Storage.SetInt("int", 10);
                Storage.SetFloat("float", 12.345f);
                Storage.SetBool("bool", true);
                Storage.SetString("string", "Example");
                Storage.Set("vector3", new Vector3(1.1f, 2.2f, 3.3f));
                Storage.Set("SerializableTest", new SerializableTestModel());
                Storage.Set("NonSerializableTest", new NonSerializableTestModel());
                Storage.Set("struct", new SerializableTestStruct());
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }

        [Test]
        public void Load()
        {
            
        }
    }
}
