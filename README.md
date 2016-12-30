XmlStorage
===


## Description
XML形式でデータを保存するライブラリです。  

Unityの[PlayerPrefs](https://docs.unity3d.com/jp/current/ScriptReference/PlayerPrefs.html)は、Windows上ではアプリケーション間でキー情報がコンフリクトしたり、レジストリに保存するために値の確認がし辛い等の問題があります。  
それらの問題を解決し置き換えるのを目的として開発されたため、PlayerPrefsをXmlStorageに全置換すればそのまま動作します。

## Usage
サンプルコード
```` csharp
void Start()
{
    XmlStorage.Set("int1", 1);
    XmlStorage.SetInt("int2", 2);
    XmlStorage.Save(); // ファイルに保存

    Debug.Log(XmlStorage.Get("int1", 0)); // 1
    Debug.Log(XmlStorage.GetInt("int2", 0)); // 2
    Debug.Log(XmlStorage.GetInt("int3", 0)); // 3
}
````
より詳しい使い方は[Example](XmlStorage/Assets/XmlStorage/Example)を参照してください。

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

    XmlStorage.Set("test", test);
    XmlStorage.Save();

    Debug.Log(XmlStorage.Get<Test>("test").int1); // 100
    Debug.Log(XmlStorage.Get("test", new Test()).str); // "STR"
}
````
<br />

- Keyに指定した文字列が同一でも、保存するデータの型(System.Type情報)が異なれば別データとして保存されます。
```` csharp
void Start()
{
    XmlStorage.Set("value", "str");
    XmlStorage.Set("value", 10);
    XmlStorage.Set("value", 11);
    XmlStorage.Save();

    Debug.Log(XmlStorage.Get("value", "")); // str
    Debug.Log(XmlStorage.Get("value", 0)); // 11
}
````
<br />

- 各データはAggregation(集団)に所属しており、所属する集団を指定する事が出来ます。
  - 各集団毎に保存する『ファイル名』『フォルダパス』『拡張子』等を変更する事が出来ます。
  - 各集団同士は独立しているので、キー情報はコンフリクトしません。
```` csharp
void Start()
{
    Debug.Log(XmlStorage.CurrentAggregationName); // Default
    XmlStorage.Set("str", "str0");
    XmlStorage.Set("int", 10);

    Debug.Log(XmlStorage.Get("str", "")); // "str0"
    Debug.Log(XmlStorage.Get("int", 0)); // 10
    
    XmlStorage.ChangeAggregation("Test");
    Debug.Log(XmlStorage.CurrentAggregationName); // Test

    XmlStorage.Set("str", "str1");
    XmlStorage.Set("int", 100);

    Debug.Log(XmlStorage.Get("str", "")); // "str1"
    Debug.Log(XmlStorage.Get("int", 0)); // 100

    XmlStorage.ChangeAggregation(XmlStorage.DefaultAggregationName);
    Debug.Log(XmlStorage.CurrentAggregationName); // Default
    Debug.Log(XmlStorage.Get("str", "")); // "str0"
    Debug.Log(XmlStorage.Get("int", 0)); // 10
}
````
<br />

## API
### XmlStorageクラス
XmlStorageクラスはstaticクラスであり、全てのプロパティ・メソッド等はstatic宣言されています。  
全てのプロパティ・メソッドは現在選択されている集団(`CurrentAggregationName`)に対して処理が行われます。  

`<T>`は保存するデータの型を表しています。

### プロパティ
#### `const string DefaultAggregationName = "Default"`
デフォルトの集団名。  
初期状態ではこの集団名が選択されています。

#### `string FileNmae`
データを保存する時のファイル名

#### `string Extension`
データを保存する時のファイル拡張子

#### `string DirectoryPath`
データを保存する時のファイルを置くフォルダ

#### `string FileNameWithoutExtension`
データを保存する時のファイル名(拡張子なし)

#### `string FullPath`
データを保存する時の絶対パス

#### `string CurrentAggregationName`
現在選択されている集団名

### メソッド


## デフォルトの保存先
Windows： c:\Users\[username]\AppData\LocalLow\[CompanyName]\[ProductName]\[SceneName].xml
