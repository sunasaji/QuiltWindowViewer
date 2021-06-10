# QuiltWindowViewer
A tool to capture 5x9 Square Quilt image/video in full screen mode and display it to Looking Glass.

English | [Japanese](https://github.com/sunasaji/QuiltWindowViewer/blob/master/README-ja.md)

![qwv.gif](images/qwv.gif)

# How to use
1. Download ZIP from [release](https://github.com/sunasaji/QuiltWindowViewer/releases) and extract it.

2. Display 5x9 Square Quilt image/video in full screen mode with any application.

3. Execute QuiltWindowViewer and click the application name.

4. 3D image is displayed on the Looking Glass.

5. To finish viewing, click QuiltWindowViewer window and press Alt+F4.

# Use with VRChat
If you display Quilt image on VRChat and use this application, VRChat can be played with a Looking Glass.  
An avatar pedestal to display quilt image is placed at the following world.  
https://vrchat.com/home/world/wrld_6c584a2b-307f-4254-bf18-e8d4afd6fff2

- To hide mic icon displayed at the bottom-left side, press Ctrl+H Key.

- If you want to change another avater, press Ctrl+\ (Ctrl+＼) and use the default avatar.

- Press Ctrl + F12 to acquire high resolution screenshots.

# If your Quilt image is not 5x9 style...
See "How to build" section and use Unity editor to set Quilt number manually.

1. Open Project Hierarchy, Select Assets/uWindowCapture/Prefabs/uWC Window Object.prefab

2. See Inspector and Change "Holoplay(Script)->Quilt Settings->Quilt Preset" from "Automatic" to "Custom"

3. Set column number to "View Columns", Set row number to "View Rows"

4. Enable "Maximize On Play" at the top-right side in Game View, then press Play button on the top

# How to build
1. Install Unity2019.4.13f1
2. Get code from "Code" menu at upper-right side on GitHub and open it with Unity
3. Execute "Build And Run" from File menu

# Todo
- Support Quilt other than 5x9 style
- Support Physical buttons on the Looking Glass

# License

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
