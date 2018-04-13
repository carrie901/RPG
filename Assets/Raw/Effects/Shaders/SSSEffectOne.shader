// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

/*
 *      Author: Starking. 
 *      Version: 17.11.16
 */
 
Shader "SSS/AllInOne" {
	Properties {
		_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex("Main Texture", 2D) = "white" {}
		[KeywordEnum(NONE,ALPHAMASK,CHANNELMASK,DISSOLVE,ADDITIVE,ALPHABLEND,DESATURATE)] _SecondType("Second Texture Type", float) = 0
		[Enum(R,0,G,1,B,2,A,3)]_MaskChannel("Mask Channel",float) = 0
		_MaskTex("Second Texture", 2D) = "white" {}
		_Strength("Strength", Range(0, 1)) = 1
		_Cutoff("Cutoff" ,Range(0,1)) = 0
		_EdgeColor("Edge Color", Color) = (1,1,1,1)
		_EdgeWidth("Edge Width", Range(0,3)) = 0.15
		[KeywordEnum(NONE,SCROLL,ANISPRITES)] _ScrollType("Animation Type", float) = 0
		_ScrollMain("Main Count (UV), Speed (UV)", vector) = (1,1,0,0)
		_ScrollMask("Second Count (UV), Speed (UV)", vector) = (1,1,0,0)
		[MaterialToggle]_Lut("Use LUT?",float) = 0
		_LutMap("LUT(U)",2D) = "white" {}
		_LutStrength("LUT Strength",Range(0,1)) = 0.5
		_LutValue("LUT Value",Range(0,1)) = 0
		[Toggle]_VertColorAlphaIsLifeTime("Use Vert Color (A) Ctrl?",float) = 0
		[MaterialToggle]_Rim("Use Rim?",float) = 0
		_RimColor("Rim Color", Color) = (1,1,1,1)
		_RimPower("Rim Power", Range(0.3,5)) = 3
		[MaterialToggle] _Displacement("Use Displacement?", float) = 0
		_DisplacementTex("Displacement Texture",2D) = "black" {}
		_DisplacementParams("Displacement (R, G, B, H)", vector) = (1,1,1,0.1)
		_Vibrance("Vibrance",float) = 10
		_Explosion("Explosion (Delay, Add, Min, Max)",vector) = (0,0.5,0.02,0.98)
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull", float) = 0
		[HideInInspector] _Mode("Blend Mode", float) = 0
		[Enum(UnityEngine.Rendering.BlendMode)]_SrcBlend("Src Blend", float) = 1
		[Enum(UnityEngine.Rendering.BlendMode)]_DstBlend("Dst Blend", float) = 0
		[Toggle]_ZWrite("ZWrite", float) = 0
		[Enum(UnityEngine.Rendering.CompareFunction)] _ZTest("ZTest", float) = 4
		_ColorMask("Color Mask", float) = 14
	}
	
	CGINCLUDE
	
	#include "UnityCG.cginc"
	
	struct a2v {
	fixed4 vertex : POSITION;
	fixed4 color : COLOR;
	fixed2 texcoord : TEXCOORD0;
	#if (defined(_DISPLACEMENT_ON) || defined(_RIM_ON))
	fixed3 normal : NORMAL;
	#endif
	};

	struct v2f {
		half4 vertex : SV_POSITION;
		fixed4 color : COLOR;
		half2 uvMain : TEXCOORD0;
		UNITY_FOG_COORDS(1)

	#if (defined(_SECONDTYPE_ALPHAMASK) || defined(_SECONDTYPE_DISSOLVE) || defined(_SECONDTYPE_ADDITIVE) || defined(_SECONDTYPE_ALPHABLEND)) 
		half2 uvMask : TEXCOORD2;
	#endif

	#ifdef _RIM_ON
		half3 normal : TEXCOORD3;
		half3 viewDir : TEXCOORD4;
	#endif

	#ifdef _DISPLACEMENT_ON
		half3 disp : TEXCOORD5;
	#endif
	};

	fixed4 _TintColor;

	sampler2D _MainTex;	
	float4 _MainTex_ST;

	fixed _Mode;

	#ifndef _SECONDTYPE_NONE
	fixed _MaskType;
	fixed _MaskChannel;
	sampler2D _MaskTex;	
	float4 _MaskTex_ST;
	half _Strength;
		#ifdef _SECONDTYPE_DISSOLVE
		half _Cutoff;
		half _EdgeWidth;
		fixed4 _EdgeColor;
		#endif
	#endif

	#ifndef _SCROLLTYPE_NONE
	fixed _ScrollType;
	half4 _ScrollMain;
	half4 _ScrollMask;
	#endif

	#if !defined(_SECONDTYPE_NONE) || defined(_LUT_ON) || defined(_DISPLACEMENT_ON)
	fixed _VertColorAlphaIsLifeTime;
	#endif

	#ifdef _LUT_ON
	sampler2D _LutMap;
	half _LutStrength;
	half _LutValue;
	#endif

	#ifdef _DISPLACEMENT_ON
	sampler2D _DisplacementTex;
	half4 _DisplacementParams;
	half _Vibrance;
	half4 _Explosion;
	#endif

	#ifdef _RIM_ON
	fixed4 _RimColor;
	half _RimPower;
	#endif

	//================================================================================================

	//Vert func vertex offset
	inline half3 Displacement(half3 normal, fixed4 dispColor, half4 displacement, half vibrance, out half3 disp) {
		disp = 1;
		half lifeFactor = _Time.x * vibrance;
		disp.x = sin(lifeFactor * 2 * UNITY_PI) * 0.5f + 0.5f;
		disp.y = sin((lifeFactor + 0.33333333f) * 2 * UNITY_PI) * 0.5f + 0.5f;
		disp.z = sin((lifeFactor + 0.66666667f) * 2 * UNITY_PI) * 0.5f + 0.5f;
		disp.xyz *= displacement.xyz;
		disp = normalize(disp);
		half d = dot(dispColor.rgb, disp.xyz);
		return normal * displacement.w * d;
	}

	//Vert func uv scroll
	inline void ScrollUV(inout half2 uv, half4 param) {
	#if defined(_SCROLLTYPE_SCROLL)
		uv = uv * param.xy + _Time.xx * param.zw;
	#elif defined(_SCROLLTYPE_ANISPRITES)
		half2 d = 1 / param.xy;
		half speed = _Time.y * param.z;
		uv = uv * d.xy + half2(floor(speed) * d.x, 1 - floor(speed * d.x) * d.y);
	#endif
	}

	//frag func channelmask
	inline fixed4 ChannelMask(half maskChannel) {
		fixed AOn = step(2.5, maskChannel);//greater than A level
		fixed BOn = step(1.5, maskChannel);//greater than B Level
		fixed GOn = step(0.5, maskChannel);//greater than G Level 
		fixed ROn = 1 - GOn;//less than G Level
		GOn *= 1 - BOn;//less than B Level
		BOn *= 1 - AOn;//less than A Level
		return fixed4(ROn, GOn, BOn, AOn);
	}

	//frag func main texture combine second texture
	inline void TextureBlend(inout fixed4 tex, fixed4 mask, fixed strength) {
	#if defined(_SECONDTYPE_ALPHAMASK)
		fixed4 channelMask = ChannelMask(_MaskChannel);
		tex.a *= lerp(1, dot(mask, channelMask), strength);
	#elif defined(_SECONDTYPE_ADDITIVE)
		tex += mask * mask.a * strength;
	#elif defined(_SECONDTYPE_ALPHABLEND)
		tex = lerp(tex, mask, (1 - tex.a) * strength);
	#endif
	}

	//frag func main texture blend
	inline void TextureBlend(inout fixed4 tex, fixed strength) {
	#if defined(_SECONDTYPE_DESATURATE)
		half grayscale = tex.r * 0.3 + tex.g * 0.59 + tex.b * 0.11;
		fixed4 grayColor = fixed4(grayscale, grayscale, grayscale, tex.a);
		tex = lerp(tex, grayColor, strength);
	#elif defined(_SECONDTYPE_CHANNELMASK)
		fixed4 channelMask = ChannelMask(_MaskChannel);
		tex.rgb = dot(tex, channelMask);
	#endif	
	}

	#if defined(_SECONDTYPE_DISSOLVE)

	//frag func dissolve
	inline void Dissolve(inout fixed3 col, half dissMask, half dissolve, half cutoff, half edgeWidth, fixed3 edgeColor) {
		dissolve = dissolve - (1 - dissolve) * cutoff;
		clip(dissMask - dissolve);
		dissolve = saturate(dissolve);
		fixed edgeFactor = 1 - saturate((dissMask - dissolve) / (edgeWidth * dissolve + 0.001));
		col = lerp(col, col + edgeColor * 2 - 1, edgeFactor * (1 - step(edgeWidth, 0)));
	}

	inline void Dissolve_S(inout fixed3 col, fixed4 maskBase, fixed dissolve) {
		half dissMask = dot(maskBase, ChannelMask(_MaskChannel));
		Dissolve(/* out */col.rgb, dissMask, dissolve, _Cutoff, _EdgeWidth, _EdgeColor.rgb);
	}

	#endif

	//frag func dissolve with lut and displacement
	inline fixed3 LUTDisplacementDissolve(fixed3 dispColor, half3 disp, sampler2D lutMap, half4 explosion, half dissolve, out half dissMask) {
		half dlut = dot(dispColor, disp);
		dissMask = 1 - dlut;
		half delay = saturate(explosion.x);//emission time
		half maxDist = max(delay, 1 - delay);
		half near = abs(dissolve - delay) / maxDist;
		dlut = dlut - pow((1 - near), 0.75) * explosion.y;
		dlut = saturate(dlut * abs(explosion.w - explosion.z) + min(explosion.z, explosion.w) + dissolve);
		dlut = clamp(dlut, 0.02, 0.98);
		return tex2D(lutMap, float2(dlut, 0.5)).rgb;
	}

	#ifdef _RIM_ON

	inline void AddRim(inout fixed4 color, half3 normal, half3 viewDir){
		half nv = saturate(dot(normalize(normal), normalize(viewDir)));
		fixed3 rimColor = pow(1 - nv, _RimPower) * (_RimColor.rgb * 2 - 1);
		color.rgb += rimColor;
	}

	#endif

	//================================================================================================

	//vert main func
	v2f vert(a2v v) {
		
		v2f o;
		
		UNITY_INITIALIZE_OUTPUT(v2f, o);

		o.color = v.color;
		o.uvMain = (v.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw);
		
	#ifndef _SCROLLTYPE_NONE
		ScrollUV(/* inout */o.uvMain, _ScrollMain);
	#endif

	#if (defined(_SECONDTYPE_ALPHAMASK) || defined(_SECONDTYPE_DISSOLVE) || defined(_SECONDTYPE_ADDITIVE) || defined(_SECONDTYPE_ALPHABLEND))
		o.uvMask = (v.texcoord.xy * _MaskTex_ST.xy + _MaskTex_ST.zw);
		#ifndef _SCROLLTYPE_NONE
		ScrollUV(/* inout */o.uvMask, _ScrollMask);
		#endif
	#endif

	#ifdef _DISPLACEMENT_ON
		half2 uvDisp;
		#ifdef _SECONDTYPE_DISSOLVE
		uvDisp = o.uvMask;
		#else
		uvDisp = v.texcoord.xy;
		#endif
		fixed4 displaceColor = tex2Dlod(_DisplacementTex, float4(uvDisp, 0, 0));
		v.vertex.xyz += Displacement(v.normal, displaceColor, _DisplacementParams, _Vibrance, /* out */o.disp);
	#endif

		o.vertex = UnityObjectToClipPos(v.vertex);

	#ifdef _RIM_ON
		float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
		o.normal = mul(v.normal, (float3x3)unity_WorldToObject);							
		o.viewDir = _WorldSpaceCameraPos.xyz - worldPos;
	#endif

		UNITY_TRANSFER_FOG(o, o.vertex);
		return o;
	}

	//================================================================================================

	//frag main func
	fixed4 frag(v2f i) : SV_Target {

		fixed4 tex = tex2D(_MainTex, i.uvMain);
		
		fixed4 vertColor = i.color;//vert color

	#if !defined(_SECONDTYPE_NONE) || defined(_LUT_ON) || defined(_DISPLACEMENT_ON)
		vertColor.a = lerp(vertColor.a, 1, _VertColorAlphaIsLifeTime);
	#endif

	#ifndef _SECONDTYPE_NONE

		half strength = lerp(_Strength, i.color.a, _VertColorAlphaIsLifeTime);
		
		#if (defined(_SECONDTYPE_ALPHAMASK) || defined(_SECONDTYPE_ADDITIVE) || defined(_SECONDTYPE_ALPHABLEND))
		TextureBlend(/* inout */tex, tex2D(_MaskTex, i.uvMask), strength);
		#elif (defined(_SECONDTYPE_CHANNELMASK) || defined(_SECONDTYPE_DESATURATE))
		TextureBlend(/* inout */tex, strength);
		#endif
		
	#endif

		fixed4 col = vertColor * tex;

	#ifdef _LUT_ON

		fixed3 lutColor = fixed3(0, 0, 0);

		#if !(defined(_DISPLACEMENT_ON) && defined(_SECONDTYPE_DISSOLVE))
		half lut = lerp(_LutValue, _LutValue * (1 - i.color.a), _VertColorAlphaIsLifeTime);
		lutColor = tex2D(_LutMap, float2(clamp(lut, 0.02, 0.98), 0.5)).rgb;
		#endif

		#if defined(_DISPLACEMENT_ON)
		
			#ifdef _SECONDTYPE_DISSOLVE
			fixed3 dispColor = tex2D(_DisplacementTex, i.uvMask).rgb;
			half dissolve = 1 - strength;
			#else
			fixed3 dispColor = tex2D(_DisplacementTex, i.uvMain).rgb;
			half dissolve = lerp(_TintColor.a, 1 - i.color.a, _VertColorAlphaIsLifeTime);
			#endif
			
			half dissMask = 0;
			lutColor = LUTDisplacementDissolve(dispColor, i.disp, _LutMap, _Explosion, dissolve, /* out */dissMask);
			#ifdef _SECONDTYPE_DISSOLVE
			Dissolve(/* out */lutColor, dissMask, dissolve, _Cutoff, _EdgeWidth, _EdgeColor.rgb);
			#endif
			
		#elif defined(_SECONDTYPE_DISSOLVE)	
			Dissolve_S(/* out */lutColor, tex2D(_MaskTex, i.uvMask), 1 - strength);
		#endif

		col.rgb = lerp(col.rgb, lerp(lutColor * col.rgb, lutColor, saturate(_LutStrength * 2 - 1)), saturate(_LutStrength * 2));

	#elif defined(_SECONDTYPE_DISSOLVE)
		Dissolve_S(/* out */col.rgb, tex2D(_MaskTex, i.uvMask), 1 - strength);
	#endif

		col *= _TintColor * 2.0;

	#ifdef _RIM_ON
		AddRim(col, i.normal, i.viewDir);
	#endif

		//UNITY_APPLY_FOG_COLOR(i.fogCoord, col, (1 - step(2.5, _Mode)) * unity_FogColor);
		return col;
	}
	
	ENDCG

	SubShader{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
		LOD 100
		Blend [_SrcBlend] [_DstBlend]
		ZWrite [_ZWrite]
		ZTest [_ZTest]
		Cull [_Cull]
		ColorMask [_ColorMask]
		Offset -1, -1

		Pass {

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 2.0
			#pragma multi_compile_fog
			#pragma multi_compile _SECONDTYPE_NONE _SECONDTYPE_ALPHAMASK _SECONDTYPE_CHANNELMASK _SECONDTYPE_DISSOLVE _SECONDTYPE_ADDITIVE _SECONDTYPE_ALPHABLEND _SECONDTYPE_DESATURATE
			#pragma multi_compile _SCROLLTYPE_NONE _SCROLLTYPE_SCROLL _SCROLLTYPE_ANISPRITES
			#pragma multi_compile _RIM_OFF _RIM_ON
			#pragma shader_feature _LUT_OFF _LUT_ON
			#pragma shader_feature _DISPLACEMENT_OFF _DISPLACEMENT_ON

			ENDCG 
		}	
	}
	CustomEditor "SSSEffectShaderGUI"
}
