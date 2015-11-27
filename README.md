# XmlSaver
XML形式でデータを保存するライブラリ。

## 使い方
XmlSaverのSet関数で値を設定し、Save関数でXMLファイルに保存する。  
Get関数で保存したデータを取得できる。  

クラスのインスタンスでもシリアライズ出来るものならそのまま保存できる。  
Keyに指定した文字列が同一でも、保存するデータの型(System.Type情報)が異なる場合は別データとして保存させる。

詳しい使い方は[Example](http://github.team-lab.local/SketchSeries/XmlSaver/blob/master/Assets/XmlSaver/Example/Example.cs)参照

## 保存先
Windows： c:\Users\[username]\AppData\LocalLow\[CompanyName]\[ProductName]\[FileName].xml

## リリース
http://github.team-lab.local/SketchSeries/XmlSaver/releases
