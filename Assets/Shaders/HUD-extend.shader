Shader "Unlit/HUD-extend" {
	Properties {
		_MainTex ("Texture", 2D) = "white" {}
		_SwitchTex ("Switch", 2D) = "white" {}
		_Zoom ("HUD Zoom", Range (0.05, 2)) = 1
		_PosX ("HUD Pos X", Range (-2, 2)) = 0
		_PosY ("HUD Pos Y", Range (-2, 2)) = 0
	}

	SubShader {
		Tags { "RenderType"="Transparent"  "Queue"="Transparent" }
		ZTest Always	// 常に前面に表示
		Cull Off    	// 裏面が見えている場合も処理する

		Pass {
			// Meta
			CGPROGRAM
			#pragma target 3.0
			#include "UnityCG.cginc"

			// Struct
			struct appdata {
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};
			struct v2f {
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			// Param
			sampler2D _MainTex, _SwitchTex;
			Float _Zoom, _PosX, _PosY;
			float4 _MainTex_TexelSize;

			// Vertex
			#pragma vertex vert
			v2f vert (appdata v) {
				v2f o;
				// float4(u, v, w=0, LOD(0-7))
				half4 showBoolTex = tex2Dlod(_SwitchTex, float4(0, 0, 0, 0));
				bool showFlag = showBoolTex.r != 0;
				v.vertex.x = v.vertex.x * max(_MainTex_TexelSize.x, _MainTex_TexelSize.y) / _MainTex_TexelSize.x;
				v.vertex.y = v.vertex.y * max(_MainTex_TexelSize.x, _MainTex_TexelSize.y) / _MainTex_TexelSize.y;
				o.vertex = float4 (v.vertex.xy * _Zoom + float2(_PosX, _PosY), 1, 1) * showFlag;
				o.uv = v.uv;
				return o;
			}

			// Fragment
			#pragma fragment frag
			fixed4 frag (v2f i) : SV_Target {
				return tex2D(_MainTex, i.uv * float2(1, -1) + float2(0, 1));
			}

			ENDCG
		}
	}
}
