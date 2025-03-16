using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace XmlStorage.Tests
{
    public class SaveTest
    {
        [Test]
        public void Save()
        {
            try
            {
                Storage.CurrentDataGroupName = "TestGroup";
                Storage.CurrentDataGroup.SaveFilePath.FileName = "TestGroup";
                Storage.SetInt("Int", 10);
                Storage.SetFloat("Float", 12.345f);
                Storage.SetBool("Bool", true);
                Storage.SetString("String", "Example");
                Storage.Set("Vector3", new Vector3(1.1f, 2.2f, 3.3f));
                Storage.Set("TestModel", new TestModel(
                        1,
                        new[] { 1.1f, -2.2f, 3.3f, -4.4f },
                        new List<string> { "a", "b", "c" },
                        Mode.One,
                        2
                    )
                );

                Storage.CurrentDataGroupName = "NestedTestGroup";
                Storage.CurrentDataGroup.SaveFilePath.FileName = "NestedTestGroup";
                Storage.Set("NestedTestModel", new NestedTestModel()
                    {
                        Bool = true,
                        Str = "test",
                        Test = new TestModel(
                            -1,
                            new[] { 1.1235f, 0.0001f },
                            new List<string> { null, "", "." },
                            Mode.Two,
                            2
                        )
                    }
                );
                
                Storage.Save();
            }
            catch (Exception e)
            {
                Assert.Fail(e.Message);
            }
        }
    }
}
