Shader "Unlit/AltasShader"
{
	Properties
	{
		_MainTex0 ("Texture", 2D) = "white" {}
		_MainTex1 ("Texture", 2D) = "white" {}
		_MainTex2 ("Texture", 2D) = "white" {}
		_Index ("下标", Float) = 1 
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
		LOD 100

		Pass
		{

			//关闭深度写入
            //ZWrite Off
            //开启透明度混合
            Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			// #pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex0;
			float4 _MainTex0_ST;
			
			sampler2D _MainTex1;
			float4 _MainTex1_ST;
			
			sampler2D _MainTex2;
			float4 _MainTex2_ST;
			
			float _Index;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				if(_Index==0)
					o.uv = TRANSFORM_TEX(v.uv, _MainTex0);
				else if(_Index==1)
					o.uv = TRANSFORM_TEX(v.uv, _MainTex1);
				else if(_Index==2)
					o.uv = TRANSFORM_TEX(v.uv, _MainTex2);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture


				fixed4 col;
				if(_Index==0){
					col=tex2D(_MainTex0, i.uv);
				}else if(_Index==1){
					col=tex2D(_MainTex1, i.uv);
				}else if(_Index==2){
					col=tex2D(_MainTex2, i.uv);
				}
				//if ((col.a) < 0.01) {
                // 	discard;
              	//}
				//clip(col.a-1);
				// col=col*col.z;
				// apply fog
				//UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
