nwhoisTimerExact
===

License
---

Copyright (c) 2010-2011 まどがい  
Licensed under the MIT license  
<http://www.opensource.org/licenses/mit-license.php>

Description
---

ニコニコ生放送のテスト時間に対応したnwhois用タイマープラグインです。  
本家のnwhoisTimerと同じような感覚で使えるように作ってあります。

Usage
---
nwhoisのpluginフォルダにnwhoisTimerExact.dllを入れてnwhoisを起動してください。

メニューのプラグインからnwhoisタイマーExactを選択すると設定画面が起動します。  
残り時間を設定し、コメントをするにチェックを入れてコメントしたいメッセージを記入してください。  
『監視スタート』ボタンを押すか、『常に監視』にチェックを入れることで監視を開始します。

Notice
---

自分の放送でのみコメントしたい場合はコミュニティーフィルターをご利用ください。  
コミュニティーフィルターに記述がある場合、そのコミュニティでのみコメントを行うようになります。

nwhoisTimerExactは延長した場合も終了時間が近づくたびにコメントを投げます。  
そのため、延長後も適用チェックボックスはありません。  

Advertisement
---

作者のニコニコ生放送のコミュニティは以下になります。
プログラミング垂れ流し
<http://com.nicovideo.jp/community/co317507>

作者は以下のサイトを運営しています。よければご覧ください。

*ToNaMeT*  
<http://www.tonamet.com>

Version
---
2011/04/29 非同期時のエラー処理に対応しました。
2011/03/31 上手くセーブできない人がいたため、データの保存方法をXMLSerializerからXmlDocumentに変更しました。