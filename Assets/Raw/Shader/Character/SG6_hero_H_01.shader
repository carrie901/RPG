// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.27 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.27;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:4013,x:33133,y:32751,varname:node_4013,prsc:2|diff-8578-OUT,spec-2462-OUT,gloss-4294-OUT,normal-8403-RGB,emission-7610-OUT,lwrap-4470-OUT,amspl-4470-OUT,clip-4836-A;n:type:ShaderForge.SFN_Tex2d,id:2512,x:30710,y:32812,varname:node_2512,prsc:2,ntxv:0,isnm:False|TEX-1793-TEX;n:type:ShaderForge.SFN_Cubemap,id:5519,x:30741,y:33069,ptovrint:False,ptlb:Cube,ptin:_Cube,varname:node_5519,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,cube:478368f3893152949b26b3640861941b,pvfc:0;n:type:ShaderForge.SFN_Tex2d,id:4836,x:30795,y:32527,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_4836,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5b7f9515180d0034eaced9b5a771e5a4,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:9978,x:31166,y:33049,varname:node_9978,prsc:2|A-2512-B,B-5519-RGB,C-1899-OUT;n:type:ShaderForge.SFN_Blend,id:2978,x:31598,y:33021,varname:node_2978,prsc:2,blmd:10,clmp:True|SRC-4836-RGB,DST-203-OUT;n:type:ShaderForge.SFN_Tex2d,id:8403,x:32696,y:32354,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_8403,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Multiply,id:3963,x:31644,y:32747,varname:node_3963,prsc:2|A-4836-RGB,B-2512-G;n:type:ShaderForge.SFN_Add,id:3536,x:32272,y:32625,varname:node_3536,prsc:2|A-3540-OUT,B-8940-OUT;n:type:ShaderForge.SFN_Slider,id:162,x:31487,y:32604,ptovrint:False,ptlb:ClothingPower,ptin:_ClothingPower,varname:node_162,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:14.72737,max:20;n:type:ShaderForge.SFN_Multiply,id:8271,x:32009,y:32561,varname:node_8271,prsc:2|A-162-OUT,B-3963-OUT,C-7359-RGB;n:type:ShaderForge.SFN_Fresnel,id:8946,x:31360,y:32246,varname:node_8946,prsc:2|EXP-1634-OUT;n:type:ShaderForge.SFN_Multiply,id:3621,x:31543,y:32246,varname:node_3621,prsc:2|A-8946-OUT,B-8946-OUT;n:type:ShaderForge.SFN_Multiply,id:4843,x:31730,y:32246,varname:node_4843,prsc:2|A-3621-OUT,B-3621-OUT,C-1316-OUT;n:type:ShaderForge.SFN_Slider,id:1316,x:31483,y:32474,ptovrint:False,ptlb:Fresnel,ptin:_Fresnel,varname:node_1316,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:10;n:type:ShaderForge.SFN_Color,id:4361,x:31730,y:32085,ptovrint:False,ptlb:FresnelColor,ptin:_FresnelColor,varname:node_4361,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5804498,c2:0.8577557,c3:0.8970588,c4:1;n:type:ShaderForge.SFN_Multiply,id:9547,x:31940,y:32183,varname:node_9547,prsc:2|A-4361-RGB,B-4843-OUT;n:type:ShaderForge.SFN_HalfVector,id:8851,x:31256,y:34225,varname:node_8851,prsc:2;n:type:ShaderForge.SFN_Color,id:762,x:31298,y:33243,ptovrint:False,ptlb:CubeColor,ptin:_CubeColor,varname:node_762,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.8805274,c3:0.7720588,c4:1;n:type:ShaderForge.SFN_Multiply,id:203,x:31392,y:33049,varname:node_203,prsc:2|A-9978-OUT,B-762-RGB;n:type:ShaderForge.SFN_Slider,id:1899,x:30810,y:33232,ptovrint:False,ptlb:CubePower,ptin:_CubePower,varname:node_1899,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0.1,cur:1.036757,max:2;n:type:ShaderForge.SFN_Blend,id:8940,x:31890,y:33036,varname:node_8940,prsc:2,blmd:7,clmp:True|SRC-2978-OUT,DST-2978-OUT;n:type:ShaderForge.SFN_NormalVector,id:662,x:31256,y:34011,prsc:2,pt:False;n:type:ShaderForge.SFN_LightVector,id:9824,x:31270,y:33832,varname:node_9824,prsc:2;n:type:ShaderForge.SFN_Dot,id:414,x:31519,y:33905,varname:node_414,prsc:2,dt:1|A-9824-OUT,B-662-OUT;n:type:ShaderForge.SFN_Dot,id:1758,x:31519,y:34125,varname:node_1758,prsc:2,dt:1|A-662-OUT,B-8851-OUT;n:type:ShaderForge.SFN_Add,id:8429,x:32130,y:33935,varname:node_8429,prsc:2|A-7296-OUT,B-6784-OUT;n:type:ShaderForge.SFN_Multiply,id:687,x:31469,y:33582,varname:node_687,prsc:2|A-4836-RGB,B-2512-R;n:type:ShaderForge.SFN_Blend,id:7296,x:31899,y:33821,varname:node_7296,prsc:2,blmd:10,clmp:True|SRC-687-OUT,DST-414-OUT;n:type:ShaderForge.SFN_Multiply,id:6784,x:31906,y:34094,varname:node_6784,prsc:2|A-687-OUT,B-1758-OUT;n:type:ShaderForge.SFN_Multiply,id:7856,x:32409,y:33864,varname:node_7856,prsc:2|A-8429-OUT,B-1562-RGB;n:type:ShaderForge.SFN_AmbientLight,id:1562,x:32154,y:34094,varname:node_1562,prsc:2;n:type:ShaderForge.SFN_Blend,id:6693,x:31882,y:33513,varname:node_6693,prsc:2,blmd:1,clmp:True|SRC-687-OUT,DST-687-OUT;n:type:ShaderForge.SFN_Multiply,id:6234,x:32357,y:33063,varname:node_6234,prsc:2|A-6693-OUT,B-7856-OUT,C-3486-OUT,D-6815-OUT;n:type:ShaderForge.SFN_Slider,id:3486,x:32164,y:33414,ptovrint:False,ptlb:SkinEmission,ptin:_SkinEmission,varname:node_3486,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Multiply,id:3540,x:31907,y:33216,varname:node_3540,prsc:2|A-687-OUT,B-687-OUT,C-9969-OUT;n:type:ShaderForge.SFN_Vector1,id:9969,x:31687,y:33327,varname:node_9969,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Add,id:4986,x:32521,y:32844,varname:node_4986,prsc:2|A-1371-OUT,B-6234-OUT,C-3963-OUT;n:type:ShaderForge.SFN_Multiply,id:1371,x:31951,y:32875,varname:node_1371,prsc:2|A-2978-OUT,B-2978-OUT;n:type:ShaderForge.SFN_LightColor,id:7152,x:32687,y:33792,varname:node_7152,prsc:2;n:type:ShaderForge.SFN_LightAttenuation,id:2128,x:32544,y:33376,varname:node_2128,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4470,x:32929,y:33396,varname:node_4470,prsc:2|A-2128-OUT,B-7152-RGB,C-687-OUT,D-8599-OUT,E-6488-OUT;n:type:ShaderForge.SFN_Vector1,id:6815,x:32243,y:33301,varname:node_6815,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:5312,x:32551,y:32635,varname:node_5312,prsc:2|A-3536-OUT,B-9739-OUT;n:type:ShaderForge.SFN_Vector1,id:9739,x:32372,y:32748,varname:node_9739,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Multiply,id:8757,x:32755,y:32844,varname:node_8757,prsc:2|A-4986-OUT,B-4986-OUT;n:type:ShaderForge.SFN_Multiply,id:7085,x:32669,y:33213,varname:node_7085,prsc:2|A-2512-B,B-9886-OUT;n:type:ShaderForge.SFN_ToggleProperty,id:8599,x:33042,y:33622,ptovrint:False,ptlb:AmbientLight,ptin:_AmbientLight,varname:node_8599,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,on:True;n:type:ShaderForge.SFN_Vector1,id:9886,x:32499,y:33247,varname:node_9886,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Color,id:7359,x:32033,y:32379,ptovrint:False,ptlb:Clothing_Specular_Color,ptin:_Clothing_Specular_Color,varname:node_7359,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2111808,c2:0.27246,c3:0.3088235,c4:1;n:type:ShaderForge.SFN_Vector1,id:1634,x:31161,y:32280,varname:node_1634,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:5878,x:32307,y:32291,varname:node_5878,prsc:2|A-9547-OUT,B-4836-RGB;n:type:ShaderForge.SFN_Vector1,id:6488,x:32703,y:33570,varname:node_6488,prsc:2,v1:1;n:type:ShaderForge.SFN_Add,id:2462,x:32755,y:32714,varname:node_2462,prsc:2|A-8271-OUT,B-5312-OUT;n:type:ShaderForge.SFN_Multiply,id:8095,x:32645,y:33033,varname:node_8095,prsc:2|A-2512-G,B-157-OUT;n:type:ShaderForge.SFN_Vector1,id:157,x:32510,y:33129,varname:node_157,prsc:2,v1:0.6;n:type:ShaderForge.SFN_Add,id:4294,x:32836,y:33065,varname:node_4294,prsc:2|A-8095-OUT,B-7085-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1793,x:30121,y:32773,ptovrint:False,ptlb:Mask,ptin:_Mask,varname:node_1793,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:2146,x:32376,y:32032,varname:node_2146,prsc:2|A-5878-OUT,B-2512-R;n:type:ShaderForge.SFN_Color,id:345,x:32376,y:31847,ptovrint:False,ptlb:skin_color,ptin:_skin_color,varname:node_345,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_Add,id:2102,x:32677,y:32148,varname:node_2102,prsc:2|A-3730-OUT,B-5878-OUT;n:type:ShaderForge.SFN_Multiply,id:3730,x:32594,y:31867,varname:node_3730,prsc:2|A-345-RGB,B-2146-OUT;n:type:ShaderForge.SFN_Color,id:7656,x:33001,y:32325,ptovrint:False,ptlb:hit_color,ptin:_hit_color,varname:node_7656,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Blend,id:8578,x:33227,y:32186,varname:node_8578,prsc:2,blmd:8,clmp:True|SRC-2102-OUT,DST-7656-RGB;n:type:ShaderForge.SFN_Multiply,id:4498,x:33054,y:32567,varname:node_4498,prsc:2|A-4836-RGB,B-9666-OUT;n:type:ShaderForge.SFN_Slider,id:9666,x:32724,y:32640,ptovrint:False,ptlb:emiss,ptin:_emiss,varname:node_9666,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Add,id:7610,x:33245,y:32605,varname:node_7610,prsc:2|A-4498-OUT,B-8757-OUT;proporder:4836-1793-8403-5519-162-7359-1316-4361-762-1899-3486-8599-345-7656-9666;pass:END;sub:END;*/

Shader "Shader Forge/SG6_hero_H_01" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Mask ("Mask", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Cube ("Cube", Cube) = "_Skybox" {}
        _ClothingPower ("ClothingPower", Range(0.1, 20)) = 14.72737
        _Clothing_Specular_Color ("Clothing_Specular_Color", Color) = (0.2111808,0.27246,0.3088235,1)
        _Fresnel ("Fresnel", Range(0, 10)) = 0
        _FresnelColor ("FresnelColor", Color) = (0.5804498,0.8577557,0.8970588,1)
        _CubeColor ("CubeColor", Color) = (1,0.8805274,0.7720588,1)
        _CubePower ("CubePower", Range(0.1, 2)) = 1.036757
        _SkinEmission ("SkinEmission", Range(0, 2)) = 0
        [MaterialToggle] _AmbientLight ("AmbientLight", Float ) = 1
        _skin_color ("skin_color", Color) = (0.5,0.5,0.5,1)
        _hit_color ("hit_color", Color) = (0,0,0,1)
        _emiss ("emiss", Range(0, 1)) = 0
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform samplerCUBE _Cube;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _ClothingPower;
            uniform float _Fresnel;
            uniform float4 _FresnelColor;
            uniform float4 _CubeColor;
            uniform float _CubePower;
            uniform float _SkinEmission;
            uniform fixed _AmbientLight;
            uniform float4 _Clothing_Specular_Color;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _skin_color;
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
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip(_Diffuse_var.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 node_2512 = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float gloss = ((node_2512.g*0.6)+(node_2512.b*0.2));
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 node_687 = (_Diffuse_var.rgb*node_2512.r);
                float3 node_4470 = (attenuation*_LightColor0.rgb*node_687*_AmbientLight*1.0);
                float3 node_3963 = (_Diffuse_var.rgb*node_2512.g);
                float3 node_2978 = saturate(( ((node_2512.b*texCUBE(_Cube,viewReflectDirection).rgb*_CubePower)*_CubeColor.rgb) > 0.5 ? (1.0-(1.0-2.0*(((node_2512.b*texCUBE(_Cube,viewReflectDirection).rgb*_CubePower)*_CubeColor.rgb)-0.5))*(1.0-_Diffuse_var.rgb)) : (2.0*((node_2512.b*texCUBE(_Cube,viewReflectDirection).rgb*_CubePower)*_CubeColor.rgb)*_Diffuse_var.rgb) ));
                float3 specularColor = ((_ClothingPower*node_3963*_Clothing_Specular_Color.rgb)+(((node_687*node_687*0.1)+saturate((node_2978/(1.0-node_2978))))*0.6));
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (0 + node_4470)*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 w = node_4470*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float node_8946 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0);
                float node_3621 = (node_8946*node_8946);
                float3 node_5878 = ((_FresnelColor.rgb*(node_3621*node_3621*_Fresnel))+_Diffuse_var.rgb);
                float3 diffuseColor = saturate((((_skin_color.rgb*(node_5878*node_2512.r))+node_5878)+_hit_color.rgb));
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 node_4986 = ((node_2978*node_2978)+(saturate((node_687*node_687))*((saturate(( max(0,dot(lightDirection,i.normalDir)) > 0.5 ? (1.0-(1.0-2.0*(max(0,dot(lightDirection,i.normalDir))-0.5))*(1.0-node_687)) : (2.0*max(0,dot(lightDirection,i.normalDir))*node_687) ))+(node_687*max(0,dot(i.normalDir,halfDirection))))*UNITY_LIGHTMODEL_AMBIENT.rgb)*_SkinEmission*1.0)+node_3963);
                float3 emissive = ((_Diffuse_var.rgb*_emiss)+(node_4986*node_4986));
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
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform samplerCUBE _Cube;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform float _ClothingPower;
            uniform float _Fresnel;
            uniform float4 _FresnelColor;
            uniform float4 _CubeColor;
            uniform float _CubePower;
            uniform float _SkinEmission;
            uniform fixed _AmbientLight;
            uniform float4 _Clothing_Specular_Color;
            uniform sampler2D _Mask; uniform float4 _Mask_ST;
            uniform float4 _skin_color;
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
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                clip(_Diffuse_var.a - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float4 node_2512 = tex2D(_Mask,TRANSFORM_TEX(i.uv0, _Mask));
                float gloss = ((node_2512.g*0.6)+(node_2512.b*0.2));
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float3 node_3963 = (_Diffuse_var.rgb*node_2512.g);
                float3 node_687 = (_Diffuse_var.rgb*node_2512.r);
                float3 node_2978 = saturate(( ((node_2512.b*texCUBE(_Cube,viewReflectDirection).rgb*_CubePower)*_CubeColor.rgb) > 0.5 ? (1.0-(1.0-2.0*(((node_2512.b*texCUBE(_Cube,viewReflectDirection).rgb*_CubePower)*_CubeColor.rgb)-0.5))*(1.0-_Diffuse_var.rgb)) : (2.0*((node_2512.b*texCUBE(_Cube,viewReflectDirection).rgb*_CubePower)*_CubeColor.rgb)*_Diffuse_var.rgb) ));
                float3 specularColor = ((_ClothingPower*node_3963*_Clothing_Specular_Color.rgb)+(((node_687*node_687*0.1)+saturate((node_2978/(1.0-node_2978))))*0.6));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = dot( normalDirection, lightDirection );
                float3 node_4470 = (attenuation*_LightColor0.rgb*node_687*_AmbientLight*1.0);
                float3 w = node_4470*0.5; // Light wrapping
                float3 NdotLWrap = NdotL * ( 1.0 - w );
                float3 forwardLight = max(float3(0.0,0.0,0.0), NdotLWrap + w );
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = forwardLight * attenColor;
                float node_8946 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0);
                float node_3621 = (node_8946*node_8946);
                float3 node_5878 = ((_FresnelColor.rgb*(node_3621*node_3621*_Fresnel))+_Diffuse_var.rgb);
                float3 diffuseColor = saturate((((_skin_color.rgb*(node_5878*node_2512.r))+node_5878)+_hit_color.rgb));
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
