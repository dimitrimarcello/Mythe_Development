Shader "MyShaders/LightingSystem"
{
	Properties
	{
		_CameraTex("The Normal cam image", 2D) = "Blue" {}
		_DarkMulti("The color for the lighting system", Color) = (0,0,0,0)
		_LightSource("The source of the light position", vector) = (0,0,0)
		_LightStrenght("The dark light strenght", Range(1,10)) = 2
		_CoreStrenght("The source start strenght", Range(1, 10)) = 1
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
				float4 worldPos : TEXCOORD1;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.worldPos = mul(unity_ObjectToWorld, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			float4 _DarkMulti;
			sampler2D _CameraTex;
			float3 _LightSource;
			float _LightStrenght;
			float _CoreStrenght;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_CameraTex, i.uv);

				// multiply the texture with the color
				float4 multiple = (1,1,1,1) * _CoreStrenght;
				float dist = distance(i.uv, _LightSource);
				multiple = lerp(multiple, _DarkMulti, dist * _LightStrenght);
				col = col * _DarkMulti * multiple;
				return col;
			}
			ENDCG
		}
	}
}
