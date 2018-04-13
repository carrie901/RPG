// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:5107,x:33736,y:32656,varname:node_5107,prsc:2|emission-6555-OUT,alpha-5268-OUT;n:type:ShaderForge.SFN_Tex2d,id:6818,x:32238,y:32544,ptovrint:False,ptlb:raodong01,ptin:_raodong01,varname:node_6818,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-8296-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:701,x:32238,y:32742,ptovrint:False,ptlb:raodong02,ptin:_raodong02,varname:node_701,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-2702-UVOUT;n:type:ShaderForge.SFN_Panner,id:8296,x:32050,y:32544,varname:node_8296,prsc:2,spu:0.1,spv:0.03|UVIN-3335-UVOUT;n:type:ShaderForge.SFN_Panner,id:2702,x:32050,y:32742,varname:node_2702,prsc:2,spu:-0.02,spv:0.05|UVIN-9197-UVOUT;n:type:ShaderForge.SFN_Multiply,id:2992,x:32443,y:32670,varname:node_2992,prsc:2|A-6818-R,B-701-R;n:type:ShaderForge.SFN_Tex2d,id:3895,x:33031,y:32666,ptovrint:False,ptlb:diffuse,ptin:_diffuse,varname:node_3895,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False|UVIN-9176-OUT;n:type:ShaderForge.SFN_Add,id:9176,x:32820,y:32637,varname:node_9176,prsc:2|A-9826-UVOUT,B-7816-OUT;n:type:ShaderForge.SFN_TexCoord,id:9826,x:32570,y:32490,varname:node_9826,prsc:2,uv:0;n:type:ShaderForge.SFN_Multiply,id:1176,x:33231,y:32775,varname:node_1176,prsc:2|A-3895-RGB,B-6517-RGB;n:type:ShaderForge.SFN_Color,id:6517,x:33031,y:32907,ptovrint:False,ptlb:color,ptin:_color,varname:node_6517,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4264706,c2:0.4233348,c3:0.4233348,c4:1;n:type:ShaderForge.SFN_Multiply,id:6901,x:33231,y:32953,varname:node_6901,prsc:2|A-3895-A,B-6517-A;n:type:ShaderForge.SFN_Multiply,id:7816,x:32620,y:32739,varname:node_7816,prsc:2|A-2992-OUT,B-7432-OUT;n:type:ShaderForge.SFN_Slider,id:7432,x:32273,y:32943,ptovrint:False,ptlb:raodong_qiangdu,ptin:_raodong_qiangdu,varname:node_7432,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.5802328,max:1;n:type:ShaderForge.SFN_VertexColor,id:9475,x:32943,y:32424,varname:node_9475,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6555,x:33444,y:32592,varname:node_6555,prsc:2|A-9475-RGB,B-1176-OUT;n:type:ShaderForge.SFN_Multiply,id:5268,x:33514,y:32828,varname:node_5268,prsc:2|A-9475-A,B-6901-OUT;n:type:ShaderForge.SFN_TexCoord,id:3335,x:31783,y:32532,varname:node_3335,prsc:2,uv:0;n:type:ShaderForge.SFN_TexCoord,id:9197,x:31771,y:32717,varname:node_9197,prsc:2,uv:0;proporder:3895-6818-701-6517-7432;pass:END;sub:END;*/

Shader "Shader Forge/S_lizi_raodong01" {
    Properties {
        _diffuse ("diffuse", 2D) = "white" {}
        _raodong01 ("raodong01", 2D) = "white" {}
        _raodong02 ("raodong02", 2D) = "white" {}
        _color ("color", Color) = (0.4264706,0.4233348,0.4233348,1)
        _raodong_qiangdu ("raodong_qiangdu", Range(0, 1)) = 0.5802328
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _raodong01; uniform float4 _raodong01_ST;
            uniform sampler2D _raodong02; uniform float4 _raodong02_ST;
            uniform sampler2D _diffuse; uniform float4 _diffuse_ST;
            uniform float4 _color;
            uniform float _raodong_qiangdu;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_2841 = _Time + _TimeEditor;
                float2 node_8296 = (i.uv0+node_2841.g*float2(0.1,0.03));
                float4 _raodong01_var = tex2D(_raodong01,TRANSFORM_TEX(node_8296, _raodong01));
                float2 node_2702 = (i.uv0+node_2841.g*float2(-0.02,0.05));
                float4 _raodong02_var = tex2D(_raodong02,TRANSFORM_TEX(node_2702, _raodong02));
                float2 node_9176 = (i.uv0+((_raodong01_var.r*_raodong02_var.r)*_raodong_qiangdu));
                float4 _diffuse_var = tex2D(_diffuse,TRANSFORM_TEX(node_9176, _diffuse));
                float3 emissive = (i.vertexColor.rgb*(_diffuse_var.rgb*_color.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(i.vertexColor.a*(_diffuse_var.a*_color.a)));
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
