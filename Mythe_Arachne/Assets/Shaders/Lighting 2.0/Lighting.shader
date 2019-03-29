// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "MyShaders/Lighting"
{
	Properties
	{
		_MainTex ("The sprite of the object", 2D) = "black" {}
		_Strenght("Lighting magnitude", range(0,10)) = 0
		_TestPos("Test", vector) = (0,0,0,0)
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

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
				float3 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _Points[100];
			int _Points_Lenght;
			float3 _TestPos;
			float _Strenght;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldPos = mul( unity_ObjectToWorld,v.vertex).xyz;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);

				float dist;
				float dist2;
				// int index = 0;
				// for(int j = 0; j < _Points_Lenght; j++){
					float2 lightSource = float2(_Points[0], _Points[1]);
					float2 lightSource2 = float2(_Points[2], _Points[3]);
					dist = distance(lightSource, i.worldPos) / _Strenght;
					dist2 = distance(lightSource2, i.worldPos) / _Strenght;
					// index += 2;
					col /= dist * dist2;
				// }


				return col;
			}

			ENDCG
		}
	}
}
