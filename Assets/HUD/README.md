# HUD Shader

テクスチャを自分の視界の固定領域に表示するためのシェーダです。

自分のみに見えるような設定をすることができます。

# 安全に使用するために
他人からこのシェーダが見えなくなるようにする必要があります。

背景色のRの値が0 **以外** の色(例:白)のカメラを作成し、Nothingを映します。  
1px四方のレンダーテクスチャにそれを貼り付けると、アバターの持ち主の時はその色、それ以外では黒が移ります。

`SwitchTex` にそのテクスチャを指定することで、自分以外の時は視界を塗り替える面積を0にします。

なお、正確な判定条件は、「`SwitchTex` の 座標(0, 0) のR値が0かどうか」です。
