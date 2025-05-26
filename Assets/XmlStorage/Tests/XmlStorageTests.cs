using NUnit.Framework;
using UnityEngine;
using System.IO;

namespace XmlStorage.Tests
{
    public class XmlStorageTests
    {
        private string testDirectory;


        // [SetUp]
        // public void SetUp()
        // {
        //     this.testDirectory = Path.Combine(Application.dataPath, "../TestSaves/");
        //     if (!Directory.Exists(this.testDirectory))
        //     {
        //         Directory.CreateDirectory(this.testDirectory);
        //     }
        //
        //     // XmlStorageの保存先ディレクトリを変更
        //     Storage.DirectoryPath = testDirectory;
        //     Storage.ChangeAggregation("TestAggregation");
        //     
        //     Storage.CurrentDataGroupName = "TestGroup";
        // }
        //
        // [TearDown]
        // public void TearDown()
        // {
        //     // テストで作成したファイルを削除
        //     if (Directory.Exists(testDirectory))
        //     {
        //         Directory.Delete(testDirectory, true);
        //     }
        //
        //     // Aggregationをデフォルトに戻す
        //     Storage.ChangeAggregation(Storage.DefaultAggregationName);
        // }
        //
        // [Test]
        // public void SaveAndLoad_IntValue()
        // {
        //     // 値を設定して保存
        //     Storage.Set("testInt", 42);
        //     Storage.Save();
        //
        //     // 値を変更して再度保存
        //     Storage.Set("testInt", 100);
        //     Storage.Save();
        //
        //     // データを読み込み
        //     Storage.Load();
        //
        //     // 値が正しく保存されているか確認
        //     int loadedValue = Storage.Get("testInt", 0);
        //     Assert.AreEqual(100, loadedValue);
        // }
        //
        // [Test]
        // public void SaveAndLoad_CustomClass()
        // {
        //     // カスタムクラスのインスタンスを作成
        //     var testData = new TestData { intValue = 10, stringValue = "Hello" };
        //
        //     // データを保存
        //     Storage.Set("testData", testData);
        //     Storage.Save();
        //
        //     // データを読み込み
        //     Storage.Load();
        //     var loadedData = Storage.Get<TestData>("testData");
        //
        //     // データが正しく保存されているか確認
        //     Assert.AreEqual(testData.intValue, loadedData.intValue);
        //     Assert.AreEqual(testData.stringValue, loadedData.stringValue);
        // }
        //
        // [System.Serializable]
        // public class TestData
        // {
        //     public int intValue;
        //     public string stringValue;
        // }
    }
}
