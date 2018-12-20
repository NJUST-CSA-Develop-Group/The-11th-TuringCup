// Copyright (c) CSA, NJUST. All rights reserved.
// Licensed under the Mozilla license. See LICENSE file in the project root for full license information.

Shader "Hidden/BlurShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Offset ("_Offset", Float) = 1.0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				float2 taps[4] : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float _Offset;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				float2 offset = _MainTex_TexelSize.xy * _Offset;
				o.uv=v.uv - offset;
				o.taps[0] = o.uv + offset;
				o.taps[1] = o.uv - offset;
				o.taps[2] = o.uv + offset * float2(1,-1);
				o.taps[3] = o.uv - offset * float2(1,-1);
				return o;
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.taps[0]);
				color += tex2D(_MainTex, i.taps[1]);
				color += tex2D(_MainTex, i.taps[2]);
				color += tex2D(_MainTex, i.taps[3]);
				return color * 0.25;
			}
			ENDCG
		}
	}
}
