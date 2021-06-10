# QuiltWindowViewer
フルスクリーンで画面中央に表示された正方形の5x9 Quilt形式の画像や映像をLooking Glassに映し出すツールです。

[English](https://github.com/sunasaji/QuiltWindowViewer/blob/master/README.md) | Japanese

![qwv.gif](images/qwv.gif)

# 使い方
1. リリース [release](https://github.com/sunasaji/QuiltWindowViewer/releases) からZIPアーカイブをダウンロードして解凍します。

2. 任意のアプリで、Quilt画像や映像をフルスクリーンで表示します。

3. QuiltWindowViewerを起動してQuilt画像を表示したアプリをクリックします。

4. Looking Glassに奥行きのある映像が表示されます。

5. 終了する場合は本アプリのウィンドウをクリックしてAlt+F4を押してください。

# VRChatと組み合わせて使う
VRChatでQuilt画像を表示させて、本アプリを使うと、VRChatをLooking Glassで見ることができます。  
下記のワールドにQuilt画像を表示できるアバターを置いたので使ってください。  
https://vrchat.com/home/world/wrld_6c584a2b-307f-4254-bf18-e8d4afd6fff2

- Ctrl+Hを押すと左下のマイクアイコンを隠せます。

- 他のアバターに変更したい時は、Ctrl+\ (Ctrl+＼)を押して一旦デフォルトアバターに戻してください。

- Ctrl + F12を押すと高画質のスクリーンショットを撮ることができます。

# 5x9以外のQuilt画像を表示する
下記の自分でビルドする場合を参考に、Unityエディタで手動でQuiltの値を設定して使ってください。

1. Projectのヒエラルキーから、Assets/uWindowCapture/Prefabs/uWC Window Object.prefabを選択

2. InspectorでHoloplay(Script)->Quilt Settings->Quilt Presetを「Automatic」から「Custom」にする

3. 「View Columns」に行数(縦の枚数)を入力し、「View Rows」に列数(横の枚数)を入力する

4. Gameビュー上部右上の「Maximize On Play」をOnにして、上部の再生ボタンを押す

# 自分でビルドする場合
1. Unity2019.4.13f1をインストールする
2. GitHubの右上のCodeメニューからコードを取得してUnityで開く
3. FileメニューからBuild And Runを実行する

# Todo
- 5x9以外のQuiltの対応
- 物理ボタン対応

# ライセンス

## QuiltWindowViewer
Copyright (c) 2021 sunasaji  
Released under the MIT License  
https://github.com/sunasaji/QuiltWindowViewer/blob/master/LICENSE.txt

## [uWindowCapture](https://github.com/hecomi/uWindowCapture)
Copyright (c) 2018 hecomi  
Released under the MIT License  
https://github.com/hecomi/uWindowCapture/blob/master/README.md

## [HoloPlay SDK](https://docs.lookingglassfactory.com/developer-tools/unity)
Copyright 2019-20 Looking Glass Factory Inc. All rights reserved.  
See [LICENSE.txt](LICENSE.txt) for datails.

## [HUD Shader](https://github.com/yukatayu-vrc/HUD_shader)
Copyright (c) 2019 yukatayu-vrc  
Released under the MIT License  
https://github.com/yukatayu-vrc/HUD_shader/blob/master/LICENSE
