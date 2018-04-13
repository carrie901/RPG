// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// version 1.01 update "Queue"="AlphaTest" from "Queue"="Transparent"
// version 1.02 update "Queue"="Transparent" from "Queue"="AlphaTest",不然alpha 乱序


Shader "xyl/ParticleMulAddBlend_uv2_rot" {
Properties {
	
	_Color("color", Color) = (1,1,1,1)
	_MainTex ("Particle Texture", 2D) = "white" {}
	_Xdif_speed("Xdif_speed", Float) = 0
    _Ydif_speed("Ydif_speed", Float) = 0
    _rotate1("RotateAngle", Float) = 0.0
	_rotatespeed1("RotateSpeed", Float) = 0.0
    _rtx1("rtx", Float) = 0.5
    _rty1("rty", Float) = 0.5
    
    _decal("decal", 2D) = "white" {}
    _Xdecal_speed("Xdecal_speed", Float) = 0
    _Ydecal_speed("Ydecal_speed", Float) = 0
    _rotate2("RotateAngle", Float) = 0.0
	_rotatespeed2("RotateSpeed", Float) = 0.0// 0221  add fog()
    _rtx2("rtx", Float) = 0.5
    _rty2("rty", Float) = 0.5
    
    _Mask("Mask", 2D) = "white" {}
    _Xmask_speed("Xmask_speed", Float) = 0
    _Ymask_speed("Ymask_speed", Float) = 0
     _rotate3("RotateAngle", Float) = 0.0
	_rotatespeed3("RotateSpeed", Float) = 0.0
    _rtx3("rtx", Float) = 0.5
    _rty3("rty", Float) = 0.5
    
    _Illumin_power("Illumin_power", Float) = 1
    _Alpha("ALpha", Float) = 1
    
     _lm("lm", 2D) = "white" {}
    _Xlm_speed("Xlm_speed", Float) = 0
    _Ylm_speed("Ylm_speed", Float) = 0
      _rotate4("RotateAngle", Float) = 0.0
	_rotatespeed4("RotateSpeed", Float) = 0.0
    _rtx4("rtx", Float) = 0.5
    _rty4("rty", Float) = 0.5
    
    
	//_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
}

Category {
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
	Blend SrcAlpha One
	
	AlphaTest Greater .01
	//Offset -1,-1
	ColorMask RGB
	Cull Off Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
		//Bind "texcoord", texcoord
		//Bind "texcoord1", texcoord1
		//Bind "texcoord2", texcoord2
		
		
	}
	

	
	// ---- Fragment program cards
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_particles
            #pragma target 3.0
			#include "UnityCG.cginc"

			sampler2D _MainTex;	
			float4 _Color;
			float _Xdif_speed;
            float _Ydif_speed;
            float _rotate1;
            float _rtx1;
            float _rty1;
            float _rotatespeed1;
            
            sampler2D _decal;
            float _Xdecal_speed;
            float _Ydecal_speed;
            float _rotate2;
            float _rtx2;
            float _rty2;
            float _rotatespeed2;
            
            sampler2D _Mask;
            float _Xmask_speed;
            float _Ymask_speed;
            float _rotate3;
            float _rtx3;
            float _rty3;
            float _rotatespeed3;
            
            
            float _Illumin_power;
			float _Alpha;
			
			 sampler2D _lm;
            float _Xlm_speed;
            float _Ylm_speed;
             float _rotate4;
            float _rtx4;
            float _rty4;
            float _rotatespeed4;
			
			
			
			struct appdata_t {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float2 texcoord : TEXCOORD; // uv1使用maintex
				float4 texcoord1 : TEXCOORD1;// uv2使用LM
				
			};

			struct v2f {
				float4 vertex : POSITION;
				fixed4 color : COLOR;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				
				
				//float2 uv_MainTex;
				//#ifdef SOFTPARTICLES_ON
				//float4 projPos : TEXCOORD1;
				//#endif
			};
			
			float4 _MainTex_ST;
			float4 _decal_ST;
			float4 _Mask_ST;
			float4 _lm_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				//#ifdef SOFTPARTICLES_ON
				//o.projPos = ComputeScreenPos (o.vertex);
				//COMPUTE_EYEDEPTH(o.projPos.z);
				//#endif
				o.color = v.color;
				o.texcoord.xy = TRANSFORM_TEX(v.texcoord,_MainTex);
				o.texcoord.zw = TRANSFORM_TEX(v.texcoord,_decal);
				o.texcoord2.xy = TRANSFORM_TEX(v.texcoord,_Mask);
				o.texcoord1.xy = TRANSFORM_TEX(v.texcoord1,_lm);
				
				
				return o;
			}

			//sampler2D _CameraDepthTexture;
			//float _InvFade;
			
			fixed4 frag (v2f i) : COLOR
			{
				
				//#ifdef SOFTPARTICLES_ON
				//float sceneZ = LinearEyeDepth (UNITY_SAMPLE_DEPTH(tex2Dproj(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos))));
				//float partZ = i.projPos.z;
				//float fade = saturate (_InvFade * (sceneZ-partZ));
				//i.color.a *= fade;
				//#endif
				
				//dif uv pan + time
				float4 Multiply9=_Time * _Xdif_speed.xxxx;
                float4 UV_Pan5=float4((i.texcoord.xyxy).x + Multiply9.x,(i.texcoord.xyxy).y,(i.texcoord.xyxy).z,(i.texcoord.xyxy).w);
                float4 Multiply10=_Time * _Ydif_speed.xxxx;
                float4 UV_Pan4=float4(UV_Pan5.x,UV_Pan5.y + Multiply10.x,UV_Pan5.z,UV_Pan5.w);
                
                //dif uv rotate + time
                float Rspeed=_Time*_rotatespeed1*10;
	            float rad1 = float(radians(_rotate1)+Rspeed);
                float tx=UV_Pan4.x-_rtx1;
	            float ty=UV_Pan4.y-_rty1;
	            float2 UV_rat1;
	            UV_rat1.x=float(tx*cos(rad1)-ty*sin(rad1));
	            UV_rat1.y=float(tx*sin(rad1)+ty*cos(rad1));
	            UV_rat1.x+=_rtx1;
	            UV_rat1.y+=_rty1;   
	            
	            
	                                     
                
                //dif tex2D+ color
                float4 Tex2D1=tex2D(_MainTex,UV_rat1.xy);
				float4 Multiply1=Tex2D1 * _Color;
                
                
                //decal uv pan + time
                float4 Multiply7=_Time * _Xdecal_speed.xxxx;
                float4 UV_Pan2=float4((i.texcoord.zwzw).x + Multiply7.x,(i.texcoord.zwzw).y,(i.texcoord.zwzw).z,(i.texcoord.zwzw).w);
                float4 Multiply8=_Time * _Ydecal_speed.xxxx;
                float4 UV_Pan3=float4(UV_Pan2.x,UV_Pan2.y + Multiply8.x,UV_Pan2.z,UV_Pan2.w);
	            
	            //decal uv rotate + time
	            float Rspeed2=_Time*_rotatespeed2*10;
	            float rad2 = float(radians(_rotate2)+Rspeed2);
                float tx2=UV_Pan3.x-_rtx2;
	            float ty2=UV_Pan3.y-_rty2;
	            float2 UV_rat2;
	            UV_rat2.x=float(tx2*cos(rad2)-ty2*sin(rad2));
	            UV_rat2.y=float(tx2*sin(rad2)+ty2*cos(rad2));
	            UV_rat2.x+=_rtx2;
	            UV_rat2.y+=_rty2;
	           
                //decalmap tex2D + (dif tex2D+ color)
                float4 Tex2D2=tex2D(_decal,UV_rat2.xy);
                float4 Multiply6=Multiply1 * Tex2D2;
                
                
                
                 //mask uv pan + time
                float4 Multiply0=_Time * _Xmask_speed.xxxx;
                float4 UV_Pan1=float4((i.texcoord2.xyxy).x + Multiply0.x,(i.texcoord2.xyxy).y,(i.texcoord2.xyxy).z,(i.texcoord2.xyxy).w);
                float4 Multiply4=_Time * _Ymask_speed.xxxx;
                float4 UV_Pan0=float4(UV_Pan1.x,UV_Pan1.y + Multiply4.x,UV_Pan1.z,UV_Pan1.w);
                
                
           
	            //mask uv rotate + time
	            float Rspeed3=_Time*_rotatespeed3*10;
	            float rad3 = float(radians(_rotate3)+Rspeed3);
                float tx3=UV_Pan0.x-_rtx3;
	            float ty3=UV_Pan0.y-_rty3;
	            float2 UV_rat3;
	            UV_rat3.x=float(tx3*cos(rad3)-ty3*sin(rad3));
	            UV_rat3.y=float(tx3*sin(rad3)+ty3*cos(rad3));
	            UV_rat3.x+=_rtx3;
	            UV_rat3.y+=_rty3;
                
                
                float4 Tex2D3=tex2D(_Mask,UV_rat3.xy);
                float4 Multiply2=Multiply6 * Tex2D3;
                float4 Multiply3=Multiply2 * _Illumin_power.xxxx;
				
				float4 Saturate0=saturate(_Alpha.xxxx);
                float4 Lerp0=lerp(float4( 0,0,0,0 ),Multiply3,Saturate0);
				
				
			    float4 Multiply09=_Time * _Xlm_speed.xxxx;
                float4 UV_Pan05=float4((i.texcoord1.xyxy).x + Multiply09.x,(i.texcoord1.xyxy).y,(i.texcoord1.xyxy).z,(i.texcoord1.xyxy).w);
                float4 Multiply010=_Time * _Ylm_speed.xxxx;
                float4 UV_Pan04=float4(UV_Pan05.x,UV_Pan05.y + Multiply010.x,UV_Pan05.z,UV_Pan05.w);
                
                float Rspeed4=_Time*_rotatespeed4*10;
	            float rad4 = float(radians(_rotate4)+Rspeed4);
                float tx4=UV_Pan04.x-_rtx4;
	            float ty4=UV_Pan04.y-_rty4;
	            float2 UV_rat4;
	            UV_rat4.x=float(tx4*cos(rad4)-ty4*sin(rad4));
	            UV_rat4.y=float(tx4*sin(rad4)+ty4*cos(rad4));
	            UV_rat4.x+=_rtx4;
	            UV_rat4.y+=_rty4;
	            
                float4 Tex2D01=tex2D(_lm,UV_rat4.xy);
				
				
				
				
				
				return i.color * Lerp0 * Tex2D01;
			}
			ENDCG 
		}
	} 	
	
	// ---- Dual texture cards
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				constantColor [_TintColor]
				combine constant * primary
			}
			SetTexture [_MainTex] {
				combine texture * previous DOUBLE
			}
		}
	}
	
	// ---- Single texture cards (does not do color tint)
	SubShader {
		Pass {
			SetTexture [_MainTex] {
				combine texture * primary
			}
		}
	}
}
}
