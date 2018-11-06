// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:False,tesm:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:4013,x:32887,y:32563,varname:node_4013,prsc:2|diff-5509-RGB,spec-8247-OUT,normal-5068-RGB,emission-3320-OUT,amdfl-9259-OUT,amspl-3619-OUT,clip-5509-A;n:type:ShaderForge.SFN_Color,id:1304,x:32374,y:32833,ptovrint:False,ptlb:Amb_Color,ptin:_Amb_Color,varname:node_1304,prsc:2,glob:False,c1:0.1176471,c2:0.1107266,c3:0.1107266,c4:1;n:type:ShaderForge.SFN_Tex2d,id:5509,x:32476,y:32482,ptovrint:False,ptlb:diff,ptin:_diff,varname:node_5509,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:5068,x:32661,y:32665,ptovrint:False,ptlb:norm,ptin:_norm,varname:node_5068,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:8330,x:31883,y:32596,ptovrint:False,ptlb:spec,ptin:_spec,varname:node_8330,prsc:2,ntxv:2,isnm:False;n:type:ShaderForge.SFN_Slider,id:5200,x:31852,y:32846,ptovrint:False,ptlb:Spec_power,ptin:_Spec_power,varname:node_5200,prsc:2,min:0,cur:0.1006861,max:1.5;n:type:ShaderForge.SFN_Multiply,id:8247,x:32476,y:32665,varname:node_8247,prsc:2|A-8330-RGB,B-1979-OUT;n:type:ShaderForge.SFN_Add,id:1979,x:32176,y:32683,varname:node_1979,prsc:2|A-8683-OUT,B-5200-OUT;n:type:ShaderForge.SFN_Cubemap,id:9093,x:31793,y:33075,ptovrint:False,ptlb:cube,ptin:_cube,varname:node_9093,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8722,x:32128,y:33124,varname:node_8722,prsc:2|A-9093-RGB,B-7651-OUT;n:type:ShaderForge.SFN_Slider,id:7651,x:31714,y:33299,ptovrint:False,ptlb:cube_strenth,ptin:_cube_strenth,varname:node_7651,prsc:2,min:0,cur:0.2364294,max:1;n:type:ShaderForge.SFN_Multiply,id:3619,x:32445,y:33133,varname:node_3619,prsc:2|A-8722-OUT,B-8683-OUT;n:type:ShaderForge.SFN_Slider,id:8399,x:32256,y:32997,ptovrint:False,ptlb:Amb_light,ptin:_Amb_light,varname:node_8399,prsc:2,min:0,cur:1.757912,max:6;n:type:ShaderForge.SFN_Multiply,id:9259,x:32583,y:32833,varname:node_9259,prsc:2|A-1304-RGB,B-8399-OUT;n:type:ShaderForge.SFN_Multiply,id:2888,x:31734,y:32317,varname:node_2888,prsc:2|A-8330-R,B-8330-B;n:type:ShaderForge.SFN_Add,id:5858,x:31908,y:32317,varname:node_5858,prsc:2|A-2888-OUT,B-2888-OUT;n:type:ShaderForge.SFN_Add,id:8683,x:32087,y:32317,varname:node_8683,prsc:2|A-5858-OUT,B-5858-OUT;n:type:ShaderForge.SFN_Fresnel,id:6597,x:32508,y:32219,varname:node_6597,prsc:2|EXP-448-OUT;n:type:ShaderForge.SFN_Slider,id:448,x:32161,y:32203,ptovrint:False,ptlb:fresnel_range,ptin:_fresnel_range,varname:node_448,prsc:2,min:0,cur:10,max:15;n:type:ShaderForge.SFN_Slider,id:4888,x:32430,y:32382,ptovrint:False,ptlb:fresnel_strenth,ptin:_fresnel_strenth,varname:node_4888,prsc:2,min:0,cur:2.192162,max:9;n:type:ShaderForge.SFN_Multiply,id:6763,x:32767,y:32219,varname:node_6763,prsc:2|A-6597-OUT,B-4888-OUT;n:type:ShaderForge.SFN_Multiply,id:3320,x:32996,y:32219,varname:node_3320,prsc:2|A-6763-OUT,B-7648-RGB;n:type:ShaderForge.SFN_Color,id:7648,x:32816,y:32375,ptovrint:False,ptlb:fresnel_color,ptin:_fresnel_color,varname:node_7648,prsc:2,glob:False,c1:0.5,c2:0.5,c3:0.5,c4:1;proporder:5509-5068-8330-5200-9093-7651-1304-8399-448-4888-7648;pass:END;sub:END;*/

Shader "Shader Forge/HeroShaderRim" {
    Properties {
        _diff ("diff", 2D) = "black" {}
        _norm ("norm", 2D) = "bump" {}
        _spec ("spec", 2D) = "black" {}
        _Spec_power ("Spec_power", Range(0, 1.5)) = 0.1006861
        _cube ("cube", Cube) = "_Skybox" {}
        _cube_strenth ("cube_strenth", Range(0, 1)) = 0.2364294
        _Amb_Color ("Amb_Color", Color) = (0.1176471,0.1107266,0.1107266,1)
        _Amb_light ("Amb_light", Range(0, 6)) = 1.757912
        _fresnel_range ("fresnel_range", Range(0, 15)) = 10
        _fresnel_strenth ("fresnel_strenth", Range(0, 9)) = 2.192162
        _fresnel_color ("fresnel_color", Color) = (0.5,0.5,0.5,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="Opaque"
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform float4 _Amb_Color;
            uniform sampler2D _diff; uniform float4 _diff_ST;
            uniform sampler2D _norm; uniform float4 _norm_ST;
            uniform sampler2D _spec; uniform float4 _spec_ST;
            uniform float _Spec_power;
            uniform samplerCUBE _cube;
            uniform float _cube_strenth;
            uniform float _Amb_light;
            uniform float _fresnel_range;
            uniform float _fresnel_strenth;
            uniform float4 _fresnel_color;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                UNITY_TRANSFER_FOG(o,o.pos);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _norm_var = UnpackNormal(tex2D(_norm,TRANSFORM_TEX(i.uv0, _norm)));
                float3 normalLocal = _norm_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 _diff_var = tex2D(_diff,TRANSFORM_TEX(i.uv0, _diff));
                clip(_diff_var.a - 0.5);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _spec_var = tex2D(_spec,TRANSFORM_TEX(i.uv0, _spec));
                float node_2888 = (_spec_var.r*_spec_var.b);
                float node_5858 = (node_2888+node_2888);
                float node_8683 = (node_5858+node_5858);
                float3 specularColor = (_spec_var.rgb*(node_8683+_Spec_power));
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 indirectSpecular = (0 + ((texCUBE(_cube,viewReflectDirection).rgb*_cube_strenth)*node_8683))*specularColor;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                indirectDiffuse += (_Amb_Color.rgb*_Amb_light); // Diffuse Ambient Light
                float3 diffuseColor = _diff_var.rgb;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float3 emissive = ((pow(1.0-max(0,dot(normalDirection, viewDirection)),_fresnel_range)*_fresnel_strenth)*_fresnel_color.rgb);
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _diff; uniform float4 _diff_ST;
            uniform sampler2D _norm; uniform float4 _norm_ST;
            uniform sampler2D _spec; uniform float4 _spec_ST;
            uniform float _Spec_power;
            uniform float _fresnel_range;
            uniform float _fresnel_strenth;
            uniform float4 _fresnel_color;
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
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
/////// Vectors:
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _norm_var = UnpackNormal(tex2D(_norm,TRANSFORM_TEX(i.uv0, _norm)));
                float3 normalLocal = _norm_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 _diff_var = tex2D(_diff,TRANSFORM_TEX(i.uv0, _diff));
                clip(_diff_var.a - 0.5);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
///////// Gloss:
                float gloss = 0.5;
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float4 _spec_var = tex2D(_spec,TRANSFORM_TEX(i.uv0, _spec));
                float node_2888 = (_spec_var.r*_spec_var.b);
                float node_5858 = (node_2888+node_2888);
                float node_8683 = (node_5858+node_5858);
                float3 specularColor = (_spec_var.rgb*(node_8683+_Spec_power));
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularColor;
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 diffuseColor = _diff_var.rgb;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                return fixed4(finalColor * 1,0);
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
            #pragma exclude_renderers d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _diff; uniform float4 _diff_ST;
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
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
                float4 _diff_var = tex2D(_diff,TRANSFORM_TEX(i.uv0, _diff));
                clip(_diff_var.a - 0.5);
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
