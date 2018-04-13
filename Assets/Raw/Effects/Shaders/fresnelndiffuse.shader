// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-440-OUT;n:type:ShaderForge.SFN_Color,id:797,x:32313,y:32566,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Fresnel,id:2798,x:32204,y:32731,varname:node_2798,prsc:2|EXP-7680-OUT;n:type:ShaderForge.SFN_Slider,id:7680,x:31790,y:32696,ptovrint:False,ptlb:node_7680,ptin:_node_7680,varname:node_7680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:10;n:type:ShaderForge.SFN_TexCoord,id:3196,x:31475,y:32991,varname:node_3196,prsc:2,uv:0;n:type:ShaderForge.SFN_Panner,id:9323,x:31816,y:32946,varname:node_9323,prsc:2,spu:0.45,spv:0.45|UVIN-3196-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:1208,x:32380,y:33033,ptovrint:False,ptlb:node_1208,ptin:_node_1208,varname:node_1208,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-180-OUT;n:type:ShaderForge.SFN_Tex2d,id:9856,x:31986,y:33219,ptovrint:False,ptlb:node_9856,ptin:_node_9856,varname:node_9856,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-5506-UVOUT;n:type:ShaderForge.SFN_Multiply,id:440,x:32535,y:32766,varname:node_440,prsc:2|A-797-RGB,B-2798-OUT,C-1974-OUT,D-1208-RGB;n:type:ShaderForge.SFN_Add,id:180,x:32211,y:33103,varname:node_180,prsc:2|A-9323-UVOUT,B-9856-R;n:type:ShaderForge.SFN_Panner,id:5506,x:31743,y:33164,varname:node_5506,prsc:2,spu:0,spv:-0.1|UVIN-3196-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:1974,x:32077,y:32918,ptovrint:False,ptlb:node_1974,ptin:_node_1974,varname:node_1974,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;proporder:797-7680-1208-9856-1974;pass:END;sub:END;*/

Shader "Shader Forge/fresnelndiffuse" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _node_7680 ("node_7680", Range(0, 10)) = 2
        _node_1208 ("node_1208", 2D) = "white" {}
        _node_9856 ("node_9856", 2D) = "white" {}
        _node_1974 ("node_1974", Float ) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _TintColor;
            uniform float _node_7680;
            uniform sampler2D _node_1208; uniform float4 _node_1208_ST;
            uniform sampler2D _node_9856; uniform float4 _node_9856_ST;
            uniform float _node_1974;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_631 = _Time + _TimeEditor;
                float2 node_5506 = (i.uv0+node_631.g*float2(0,-0.1));
                float4 _node_9856_var = tex2D(_node_9856,TRANSFORM_TEX(node_5506, _node_9856));
                float2 node_180 = ((i.uv0+node_631.g*float2(0.45,0.45))+_node_9856_var.r);
                float4 _node_1208_var = tex2D(_node_1208,TRANSFORM_TEX(node_180, _node_1208));
                float3 emissive = (_TintColor.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_7680)*_node_1974*_node_1208_var.rgb);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
