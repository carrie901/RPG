// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:32724,y:32693,varname:node_4795,prsc:2|emission-2393-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:32495,y:32793,varname:node_2393,prsc:2|A-6586-OUT,B-797-RGB,C-9248-OUT,D-5373-RGB;n:type:ShaderForge.SFN_Color,id:797,x:32043,y:32850,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:9248,x:31998,y:32999,varname:node_9248,prsc:2,v1:1;n:type:ShaderForge.SFN_Tex2d,id:9218,x:31869,y:32599,ptovrint:False,ptlb:node_9218,ptin:_node_9218,varname:node_9218,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5d97dc8da20e053479edcee2bc7e0e99,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5373,x:32043,y:33092,ptovrint:False,ptlb:node_5373,ptin:_node_5373,varname:node_5373,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:08dfddda5da46f441986eda8837086f5,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Add,id:6586,x:32168,y:32428,varname:node_6586,prsc:2|A-686-RGB,B-9218-RGB;n:type:ShaderForge.SFN_Tex2d,id:686,x:31869,y:32412,ptovrint:False,ptlb:node_686,ptin:_node_686,varname:node_686,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:43d22c9c03f6f484da6ded847273ea7d,ntxv:0,isnm:False;proporder:797-9218-5373-686;pass:END;sub:END;*/

Shader "Shader Forge/eff_H_WangYi_01_skill_04_wind" {
    Properties {
        _TintColor ("Color", Color) = (0.5,0.5,0.5,1)
        _node_9218 ("node_9218", 2D) = "white" {}
        _node_5373 ("node_5373", 2D) = "white" {}
        _node_686 ("node_686", 2D) = "white" {}
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
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TintColor;
            uniform sampler2D _node_9218; uniform float4 _node_9218_ST;
            uniform sampler2D _node_5373; uniform float4 _node_5373_ST;
            uniform sampler2D _node_686; uniform float4 _node_686_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 _node_686_var = tex2D(_node_686,TRANSFORM_TEX(i.uv0, _node_686));
                float4 _node_9218_var = tex2D(_node_9218,TRANSFORM_TEX(i.uv0, _node_9218));
                float4 _node_5373_var = tex2D(_node_5373,TRANSFORM_TEX(i.uv0, _node_5373));
                float3 emissive = ((_node_686_var.rgb+_node_9218_var.rgb)*_TintColor.rgb*1.0*_node_5373_var.rgb);
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
