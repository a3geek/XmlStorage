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
                // Storage.CurrentDataGroup.SaveFilePath.FileName = "TestGroup";
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
                // Storage.CurrentDataGroup.SaveFilePath.FileName = "NestedTestGroup";
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

//
// using NUnit.Framework;
// using UnityEngine;
// using System.IO;
//
// public class XmlStorageTests
// {
//     private string testDirectory;
//
//     [SetUp]
//     public void SetUp()
//     {
//         // テスト用のディレクトリを設定
//         testDirectory = Path.Combine(Application.dataPath, "../TestSaves/");
//         if (!Directory.Exists(testDirectory))
//         {
//             Directory.CreateDirectory(testDirectory);
//         }
//
//         // XmlStorageの保存先ディレクトリを変更
//         Storage.DirectoryPath = testDirectory;
//         Storage.ChangeAggregation("TestAggregation");
//     }
//
//     [TearDown]
//     public void TearDown()
//     {
//         // テストで作成したファイルを削除
//         if (Directory.Exists(testDirectory))
//         {
//             Directory.Delete(testDirectory, true);
//         }
//
//         // Aggregationをデフォルトに戻す
//         Storage.ChangeAggregation(Storage.DefaultAggregationName);
//     }
//
//     [Test]
//     public void SaveAndLoad_IntValue()
//     {
//         // 値を設定して保存
//         Storage.Set("testInt", 42);
//         Storage.Save();
//
//         // 値を変更して再度保存
//         Storage.Set("testInt", 100);
//         Storage.Save();
//
//         // データを読み込み
//         Storage.Load();
//
//         // 値が正しく保存されているか確認
//         int loadedValue = Storage.Get("testInt", 0);
//         Assert.AreEqual(100, loadedValue);
//     }
//
//     [Test]
//     public void SaveAndLoad_CustomClass()
//     {
//         // カスタムクラスのインスタンスを作成
//         var testData = new TestData { intValue = 10, stringValue = "Hello" };
//
//         // データを保存
//         Storage.Set("testData", testData);
//         Storage.Save();
//
//         // データを読み込み
//         Storage.Load();
//         var loadedData = Storage.Get<TestData>("testData");
//
//         // データが正しく保存されているか確認
//         Assert.AreEqual(testData.intValue, loadedData.intValue);
//         Assert.AreEqual(testData.stringValue, loadedData.stringValue);
//     }
//
//     [System.Serializable]
//     public class TestData
//     {
//         public int intValue;
//         public string stringValue;
//     }
// }