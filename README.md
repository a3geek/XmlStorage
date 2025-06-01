XmlStorage
===
XML形式でデータを保存するライブラリです。  


## 概要
Unityの[PlayerPrefs](https://docs.unity3d.com/jp/current/ScriptReference/PlayerPrefs.html)は、対応する型が非常に限定的であり、レジストリに保存するため値の確認に手間がかかるなどの問題があります。  
それらの問題を解決し置き換えることを目的として開発されたのがXmlStoraです。
既存コードのPlayerPrefsをStorageに全置換すれば、そのまま動作させることができます。

## 導入
UPMで「Add package from git URL」選択して以下のURLを入力してください。
```
https://github.com/a3geek/XmlStorage.git?path=Packages/com.a3geek.xmlstorage
```

## Usage
サンプルコード
```` csharp
private void Start()
{
    Storage.Set("int1", 1);
    Storage.SetInt("int2", 2);
    Storage.Save(); // ファイルに保存

    Debug.Log(Storage.Get("int1", 0)); // 1
    Debug.Log(Storage.GetInt("int2", 0)); // 2
    Debug.Log(Storage.GetInt("int3", 0)); // 0
}
````
より詳しい使い方は[Example](Assets/XmlStorage/Example/)や[Tests](Assets/XmlStorage/Tests)を参照してください。

## Behaviour
- シリアライズ可能なクラスであれば保存可能
```` csharp
[System.Serializable]
public class Test
{
    public int int1 = 10;
    public string str = "str";

    public Test() { }
}

void Start()
{
    var test = new Test();
    test.int1 = 100;
    test.str = "STR";

    Storage.Set("test", test);
    Storage.Save();

    Debug.Log(Storage.Get<Test>("test").int1); // 100
    Debug.Log(Storage.Get<Test>("test").str); // "STR"
}
````
<br />

- Keyに指定した文字列が同一でも、保存するデータの型(System.Type情報)が異なれば別データとして保存
```` csharp
void Start()
{
    Storage.Set("value", "str");
    Storage.Set("value", 10);
    Storage.Set("value", 11);
    Storage.Save();

    Debug.Log(Storage.Get("value", "")); // str
    Debug.Log(Storage.Get("value", 0)); // 11
}
````
<br />

- `Storage.CurrentDataGroupName`によってデータグループを切り替えることが可能
  - デフォルトでは`Storage.DefaultDataGroupName`が設定されている
  - 各グループは独立しており、同じキー情報を持つデータがあってもコンフリクトしない
  - データグループ名が保存するファイル名に反映される
```` csharp
void Start()
{
    Debug.Log(Storage.CurrentDataGroupName); // Prefs
    Storage.Set("float", 1f);
    Storage.Set("int", 10);

    Debug.Log(Storage.Get("str", 0f)); // 1.0
    Debug.Log(Storage.Get("int", 0)); // 10
    
    // データグループを変更
    Storage.CurrentDataGroupName = "Test";
    Debug.Log(Storage.CurrentDataGroupName); // Test

    Storage.Set("float", 10f);
    Storage.Set("int", 100);

    Debug.Log(Storage.Get("str", 0f)); // 10.0
    Debug.Log(Storage.Get("int", 0)); // 10
}
````
<br />
