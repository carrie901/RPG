// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:32837,y:32736,varname:node_4013,prsc:2|diff-8857-OUT,spec-3033-OUT,gloss-5318-OUT,normal-2110-RGB,emission-6246-OUT,clip-630-A;n:type:ShaderForge.SFN_Tex2d,id:630,x:31934,y:32454,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_630,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:6993,x:30793,y:32673,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_6993,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2110,x:31856,y:33052,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_2110,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Add,id:6589,x:31671,y:32766,varname:node_6589,prsc:2|A-263-OUT,B-6792-OUT,C-1525-OUT;n:type:ShaderForge.SFN_Multiply,id:263,x:31318,y:32644,varname:node_263,prsc:2|A-7128-OUT,B-6993-R;n:type:ShaderForge.SFN_Slider,id:7128,x:30955,y:32565,ptovrint:False,ptlb:Skin,ptin:_Skin,varname:node_7128,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1664303,max:1;n:type:ShaderForge.SFN_Multiply,id:6792,x:31318,y:32769,varname:node_6792,prsc:2|A-6993-G,B-5730-OUT;n:type:ShaderForge.SFN_Slider,id:5730,x:30957,y:32919,ptovrint:False,ptlb:cloth,ptin:_cloth,varname:node_5730,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1.018886,max:3;n:type:ShaderForge.SFN_Multiply,id:1525,x:31318,y:32921,varname:node_1525,prsc:2|A-6993-B,B-2472-OUT;n:type:ShaderForge.SFN_Slider,id:2472,x:30919,y:33161,ptovrint:False,ptlb:metal,ptin:_metal,varname:node_2472,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:0.7839262,max:5;n:type:ShaderForge.SFN_Fresnel,id:1723,x:31458,y:32354,varname:node_1723,prsc:2|EXP-1262-OUT;n:type:ShaderForge.SFN_Vector1,id:1262,x:31251,y:32429,varname:node_1262,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5034,x:31804,y:32188,varname:node_5034,prsc:2|A-7777-OUT,B-7777-OUT,C-6245-OUT;n:type:ShaderForge.SFN_Slider,id:6245,x:31598,y:32526,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_6245,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:5;n:type:ShaderForge.SFN_Color,id:6541,x:31398,y:32114,ptovrint:False,ptlb:fresnel_color,ptin:_fresnel_color,varname:node_6541,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2647059,c2:1,c3:0.9391481,c4:1;n:type:ShaderForge.SFN_Blend,id:7777,x:31604,y:32171,varname:node_7777,prsc:2,blmd:0,clmp:True|SRC-6541-RGB,DST-1723-OUT;n:type:ShaderForge.SFN_Multiply,id:6802,x:32086,y:32912,varname:node_6802,prsc:2|A-6589-OUT,B-1627-OUT;n:type:ShaderForge.SFN_Vector1,id:1627,x:32050,y:33200,varname:node_1627,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:7526,x:32367,y:32508,varname:node_7526,prsc:2|A-5034-OUT,B-6802-OUT,C-9685-OUT;n:type:ShaderForge.SFN_Slider,id:7676,x:31929,y:32793,ptovrint:False,ptlb:gloss,ptin:_gloss,varname:node_7676,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7,max:1;n:type:ShaderForge.SFN_Multiply,id:5318,x:32298,y:32734,varname:node_5318,prsc:2|A-6993-B,B-7676-OUT;n:type:ShaderForge.SFN_Multiply,id:4770,x:32243,y:32027,varname:node_4770,prsc:2|A-630-RGB,B-1307-OUT;n:type:ShaderForge.SFN_Vector1,id:1307,x:32035,y:31893,varname:node_1307,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Color,id:1237,x:32268,y:31849,ptovrint:False,ptlb:ice_color,ptin:_ice_color,varname:node_208,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2759516,c2:0.3235294,c3:0.3215606,c4:1;n:type:ShaderForge.SFN_Blend,id:1928,x:32524,y:31865,varname:node_1928,prsc:2,blmd:17,clmp:True|SRC-1237-RGB,DST-4770-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:3494,x:32456,y:32096,ptovrint:False,ptlb:ice_debuff,ptin:_ice_debuff,varname:node_2259,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:False;n:type:ShaderForge.SFN_Multiply,id:9685,x:32729,y:32046,varname:node_9685,prsc:2|A-1928-OUT,B-3494-OUT;n:type:ShaderForge.SFN_Blend,id:3033,x:32086,y:32621,varname:node_3033,prsc:2,blmd:1,clmp:True|SRC-630-RGB,DST-6589-OUT;n:type:ShaderForge.SFN_Color,id:2883,x:32842,y:32500,ptovrint:False,ptlb:hit_color,ptin:_hit_color,varname:node_2883,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:0.153;n:type:ShaderForge.SFN_Blend,id:8857,x:33071,y:32432,varname:node_8857,prsc:2,blmd:8,clmp:True|SRC-630-RGB,DST-2883-RGB;n:type:ShaderForge.SFN_Multiply,id:7375,x:32583,y:32398,varname:node_7375,prsc:2|A-630-RGB,B-7811-OUT;n:type:ShaderForge.SFN_Slider,id:7811,x:32243,y:32304,ptovrint:False,ptlb:emiss,ptin:_emiss,varname:node_7811,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3,max:1;n:type:ShaderForge.SFN_Add,id:6246,x:32634,y:32862,varname:node_6246,prsc:2|A-7375-OUT,B-7526-OUT;proporder:630-6993-2110-7128-5730-2472-7676-6245-6541-1237-3494-2883-7811;pass:END;sub:END;*/

Shader "Shader Forge/SG6_hero_L_01" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Skin ("Skin", Range(0, 1)) = 0.1664303
        _cloth ("cloth", Range(0.1, 3)) = 1.018886
        _metal ("metal", Range(0.1, 5)) = 0.7839262
        _gloss ("gloss", Range(0, 1)) = 0.7
        _Fresnel ("Fresnel", Range(0, 5)) = 0
        _fresnel_color ("fresnel_color", Color) = (0.2647059,1,0.9391481,1)
        _ice_color ("ice_color", Color) = (0.2759516,0.3235294,0.3215606,1)
        [MaterialToggle] _ice_debuff ("ice_debuff", Float ) = 0
        _hit_color ("hit_color", Color) = (0,0,0,0.153)
        _emiss ("emiss", Range(0, 1)) = 0.3
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _Skin;
            uniform float _cloth;
            uniform float _metal;
            uniform float _Fresnel;
            uniform float4 _fresnel_color;
            uniform float _gloss;
            uniform float4 _ice_color;
            uniform fixed _ice_debuff;
            uniform float4 _hit_color;
            uniform float _emiss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip(_Diffuse_var.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float gloss = (_Mask_var.b*_gloss);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_6589 = ((_Skin*_Mask_var.r)+(_Mask_var.g*_cloth)+(_Mask_var.b*_metal));
                float3 specularColor = saturate((_Diffuse_var.rgb*node_6589));
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = saturate((_Diffuse_var.rgb+_hit_color.rgb));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 node_7777 = saturate(min(_fresnel_color.rgb,pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0)));
                float3 emissive = ((_Diffuse_var.rgb*_emiss)+((node_7777*node_7777*_Fresnel)+(node_6589*0.1)+(saturate(abs(_ice_color.rgb-(_Diffuse_var.rgb*0.1)))*_ice_debuff)));
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _Skin;
            uniform float _cloth;
            uniform float _metal;
            uniform float _Fresnel;
            uniform float4 _fresnel_color;
            uniform float _gloss;
            uniform float4 _ice_color;
            uniform fixed _ice_debuff;
            uniform float4 _hit_color;
            uniform float _emiss;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                LIGHTING_COORDS(5,6)
                UNITY_FOG_COORDS(7)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 normalLocal = _Normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip(_Diffuse_var.a - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 _Mask_var = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float gloss = (_Mask_var.b*_gloss);
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float node_6589 = ((_Skin*_Mask_var.r)+(_Mask_var.g*_cloth)+(_Mask_var.b*_metal));
                float3 specularColor = saturate((_Diffuse_var.rgb*node_6589));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = saturate((_Diffuse_var.rgb+_hit_color.rgb));
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * 1,0);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip(_Diffuse_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
