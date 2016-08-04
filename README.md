# XmlStorage
XML形式でデータを保存するライブラリ。

## 使い方
XmlStorageのSet関数で値を設定し、Save関数でXMLファイルに保存する。  
Get関数で保存したデータを取得できる。  

クラスのインスタンスでもシリアライズ出来るものならそのまま保存できる。  
Keyに指定した文字列が同一でも、保存するデータの型(System.Type情報)が異なる場合は別データとして保存させる。

ChangeAggregationによってデータを保存する集合を変更することが出来る。
また、各Aggregation毎に保存する際の『ファイル名』『ディレクトリパス』『拡張子』を変更することが出来る。

詳しい使い方は[Example](http://github.team-lab.local/SketchSeries/XmlStorage/tree/master/Assets/XmlStorage/Example)参照

## デフォルトの保存先
Windows： c:\Users\[username]\AppData\LocalLow\[CompanyName]\[ProductName]\[SceneName].xml

## リリース
http://github.team-lab.local/SketchSeries/XmlSaver/releases
