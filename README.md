XmlStorage
===


## Description
XML形式でデータを保存するライブラリです。  

Unityの[PlayerPrefs](https://docs.unity3d.com/jp/current/ScriptReference/PlayerPrefs.html)は、Windows上ではアプリケーション間でキー情報がコンフリクトしたり、レジストリに保存するために値の確認がし辛い等の問題があります。  
それらの問題を解決し置き換えるのを目的として開発されたため、PlayerPrefsをStorageに全置換すればそのまま動作します。

## Usage
サンプルコード
```` csharp
void Start()
{
    Storage.Set("int1", 1);
    Storage.SetInt("int2", 2);
    Storage.Save(); // ファイルに保存

    Debug.Log(Storage.Get("int1", 0)); // 1
    Debug.Log(Storage.GetInt("int2", 0)); // 2
    Debug.Log(Storage.GetInt("int3", 0)); // 0
}
````
より詳しい使い方は[Example](Assets/XmlStorage/Example/)を参照してください。

## Behaviour
- クラスのインスタンスでもシリアライズ可能であればそのまま保存できます。
```` csharp
[System.Serializable]
public class Test
{
    public int int1 = 10;
    public string str = "str";
}

void Start()
{
    var test = new Test();
    test.int1 = 100;
    test.str = "STR";

    Storage.Set("test", test);
    Storage.Save();

    Debug.Log(Storage.Get<Test>("test").int1); // 100
    Debug.Log(Storage.Get("test", new Test()).str); // "STR"
}
````
<br />

- Keyに指定した文字列が同一でも、保存するデータの型(System.Type情報)が異なれば別データとして保存されます。
    - 保存するデータの型情報を明示する事もできます。
```` csharp
void Start()
{
    Storage.Set("value", "str");
    Storage.Set("value", 10);
    Storage.Set("value", 11);
    Storage.Set(typeof(object), "obj", 100);
    Storage.Save();

    Debug.Log(Storage.Get("value", "")); // str
    Debug.Log(Storage.Get("value", 0)); // 11

    Debug.Log(Storage.Get(typeof(object), "obj", 0)); // 100
    Debug.Log(Storage.Get<object>("obj")); // 100
    Debug.Log(Storage.Get(typeof(int), "obj", 0)); // 0
    Debug.Log(Storage.Get<int>("obj")); // 0
}
````
<br />

- 各データはAggregation(集団)に所属しており、所属する集団を指定する事ができます。
  - 各集団毎に保存する『ファイル名』を変更する事ができます。
  - 各集団同士は独立しているので、キー情報はコンフリクトしません。
```` csharp
void Start()
{
    Debug.Log(Storage.CurrentAggregationName); // Default
    Storage.Set("str", "str0");
    Storage.Set("int", 10);

    Debug.Log(Storage.Get("str", "")); // "str0"
    Debug.Log(Storage.Get("int", 0)); // 10
    
    Storage.ChangeAggregation("Test");
    Debug.Log(Storage.CurrentAggregationName); // Test

    Storage.Set("str", "str1");
    Storage.Set("int", 100);

    Debug.Log(Storage.Get("str", "")); // "str1"
    Debug.Log(Storage.Get("int", 0)); // 100

    Storage.ChangeAggregation(Storage.DefaultAggregationName);
    Debug.Log(Storage.CurrentAggregationName); // Default
    Debug.Log(Storage.Get("str", "")); // "str0"
    Debug.Log(Storage.Get("int", 0)); // 10
}
````
<br />

- データの保存先のフォルダは、任意のタイミングで変更することができます。
    - フォルダパスを変更した場合は`Storage.Load()`をよぶようにしてください。
    - `Storage.Load()`をよばなかった場合は内部的なデータの初期化が行われないため、今までSetしてきたデータや集団情報もそのディレクトリに保存されます。
```` csharp
void Start()
{
    Storage.Set("test1", Vector2.zero); // Defaultの保存先に保存.
    Storage.Save();

    Storage.DirectoryPath = Application.dataPath + "/../Saves2/";   
    Storage.Load();

    Storage.Set("test2", Vector2.one); // 変更した保存先に保存.
    Storage.Save(); // Sotrage.Loadをよばなかった場合、変更した保存先に"test1"も保存される。
}
````
<br />
    

## Default save folder
[Application.dataPath](https://docs.unity3d.com/ja/current/ScriptReference/Application-dataPath.html) + "/../Saves/"
