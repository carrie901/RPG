// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Shader created with Shader Forge v1.13 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.13;sub:START;pass:START;ps:flbk:,lico:1,lgpr:1,nrmq:1,nrsp:0,limd:3,spmd:0,trmd:0,grmd:1,uamb:True,mssp:True,bkdf:False,rprd:False,enco:False,rmgx:True,rpth:0,hqsc:True,hqlp:True,tesm:0,bsrc:0,bdst:1,culm:0,dpts:2,wrdp:True,dith:0,ufog:True,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,ofsf:0,ofsu:0,f2p0:False;n:type:ShaderForge.SFN_Final,id:9361,x:33209,y:32712,varname:node_9361,prsc:2|diff-851-RGB,spec-119-RGB,gloss-166-OUT,normal-7645-RGB,emission-7642-OUT,amspl-4150-OUT,clip-851-A;n:type:ShaderForge.SFN_LightVector,id:6869,x:31375,y:32722,varname:node_6869,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:9684,x:31363,y:32887,prsc:2,pt:True;n:type:ShaderForge.SFN_Dot,id:7782,x:31586,y:32792,cmnt:Lambert,varname:node_7782,prsc:2,dt:0|A-6869-OUT,B-9684-OUT;n:type:ShaderForge.SFN_Dot,id:3269,x:31586,y:32982,cmnt:Blinn-Phong,varname:node_3269,prsc:2,dt:1|A-9684-OUT,B-6473-OUT;n:type:ShaderForge.SFN_Tex2d,id:851,x:31802,y:32002,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_851,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Slider,id:5328,x:32291,y:33179,ptovrint:False,ptlb:amb_light,ptin:_amb_light,varname:node_5328,prsc:2,min:0,cur:0.5,max:1;n:type:ShaderForge.SFN_Tex2d,id:7645,x:32802,y:32783,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_7645,prsc:2,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:119,x:32055,y:32488,ptovrint:False,ptlb:Specular,ptin:_Specular,varname:node_119,prsc:2,ntxv:0,isnm:False;n:type:ShaderForge.SFN_ViewVector,id:6473,x:31363,y:33072,varname:node_6473,prsc:2;n:type:ShaderForge.SFN_Add,id:3501,x:31865,y:32794,varname:node_3501,prsc:2|A-7782-OUT,B-3269-OUT;n:type:ShaderForge.SFN_Blend,id:7498,x:32225,y:32990,varname:node_7498,prsc:2,blmd:1,clmp:True|SRC-6983-OUT,DST-3501-OUT;n:type:ShaderForge.SFN_OneMinus,id:5691,x:32355,y:32569,varname:node_5691,prsc:2|IN-119-R;n:type:ShaderForge.SFN_Power,id:166,x:32568,y:32743,varname:node_166,prsc:2|VAL-5691-OUT,EXP-3629-OUT;n:type:ShaderForge.SFN_Slider,id:3629,x:32184,y:32740,ptovrint:False,ptlb:Roughness,ptin:_Roughness,varname:node_3629,prsc:2,min:0,cur:1,max:5;n:type:ShaderForge.SFN_Multiply,id:6246,x:32538,y:32243,varname:node_6246,prsc:2|A-851-RGB,B-2343-OUT;n:type:ShaderForge.SFN_Slider,id:2343,x:32018,y:32368,ptovrint:False,ptlb:Emiss,ptin:_Emiss,varname:node_2343,prsc:2,min:0,cur:0.3,max:1;n:type:ShaderForge.SFN_Add,id:7642,x:32848,y:32367,varname:node_7642,prsc:2|A-4089-OUT,B-6246-OUT;n:type:ShaderForge.SFN_Fresnel,id:2922,x:32579,y:31875,varname:node_2922,prsc:2|EXP-8518-OUT;n:type:ShaderForge.SFN_Vector1,id:8518,x:32333,y:31909,varname:node_8518,prsc:2,v1:1;n:type:ShaderForge.SFN_Multiply,id:3393,x:32853,y:31877,varname:node_3393,prsc:2|A-2922-OUT,B-2922-OUT,C-6716-OUT;n:type:ShaderForge.SFN_Slider,id:6716,x:32489,y:32080,ptovrint:False,ptlb:Fresnel_range,ptin:_Fresnel_range,varname:node_6716,prsc:2,min:0,cur:0,max:2;n:type:ShaderForge.SFN_Slider,id:8163,x:32820,y:32081,ptovrint:False,ptlb:Fresnel_strength,ptin:_Fresnel_strength,varname:node_8163,prsc:2,min:0.1,cur:0.1,max:5;n:type:ShaderForge.SFN_Multiply,id:4089,x:33133,y:31874,varname:node_4089,prsc:2|A-3393-OUT,B-3393-OUT,C-8163-OUT;n:type:ShaderForge.SFN_Multiply,id:8527,x:32685,y:33002,varname:node_8527,prsc:2|A-7498-OUT,B-5328-OUT;n:type:ShaderForge.SFN_OneMinus,id:6983,x:32448,y:32886,varname:node_6983,prsc:2|IN-166-OUT;n:type:ShaderForge.SFN_Blend,id:4150,x:32881,y:32968,varname:node_4150,prsc:2,blmd:10,clmp:True|SRC-851-RGB,DST-8527-OUT;proporder:851-7645-119-3629-6716-8163-5328-2343;pass:END;sub:END;*/

Shader "Shader Forge/HeroShaderRim2" {
    Properties {
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Normal ("Normal", 2D) = "bump" {}
        _Specular ("Specular", 2D) = "white" {}
        _Roughness ("Roughness", Range(0, 5)) = 1
        _Fresnel_range ("Fresnel_range", Range(0, 2)) = 0
        _Fresnel_strength ("Fresnel_strength", Range(0.1, 5)) = 0.1
        _amb_light ("amb_light", Range(0, 1)) = 0.5
        _Emiss ("Emiss", Range(0, 1)) = 0.3
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
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _amb_light;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform float _Roughness;
            uniform float _Emiss;
            uniform float _Fresnel_range;
            uniform float _Fresnel_strength;
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
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float node_166 = pow((1.0 - _Specular_var.r),_Roughness);
                float gloss = 1.0 - node_166; // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                UnityGI gi = UnityGlobalIllumination (d, 1, gloss, normalDirection);
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = _Specular_var.rgb;
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = (floor(attenuation) * _LightColor0.xyz) * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (0 + saturate(( (saturate(((1.0 - node_166)*(dot(lightDirection,normalDirection)+max(0,dot(normalDirection,viewDirection)))))*_amb_light) > 0.5 ? (1.0-(1.0-2.0*((saturate(((1.0 - node_166)*(dot(lightDirection,normalDirection)+max(0,dot(normalDirection,viewDirection)))))*_amb_light)-0.5))*(1.0-_Diffuse_var.rgb)) : (2.0*(saturate(((1.0 - node_166)*(dot(lightDirection,normalDirection)+max(0,dot(normalDirection,viewDirection)))))*_amb_light)*_Diffuse_var.rgb) )));
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float3 diffuseColor = _Diffuse_var.rgb;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_2922 = pow(1.0-max(0,dot(normalDirection, viewDirection)),1.0);
                float node_3393 = (node_2922*node_2922*_Fresnel_range);
                float3 emissive = ((node_3393*node_3393*_Fresnel_strength)+(_Diffuse_var.rgb*_Emiss));
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
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma multi_compile_fog
            #pragma exclude_renderers xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Specular; uniform float4 _Specular_ST;
            uniform float _Roughness;
            uniform float _Emiss;
            uniform float _Fresnel_range;
            uniform float _Fresnel_strength;
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
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float4 _Specular_var = tex2D(_Specular,TRANSFORM_TEX(i.uv0, _Specular));
                float node_166 = pow((1.0 - _Specular_var.r),_Roughness);
                float gloss = 1.0 - node_166; // Convert roughness to gloss
                float specPow = exp2( gloss * 10.0+1.0);
////// Specular:
                float NdotL = max(0, dot( normalDirection, lightDirection ));
                float LdotH = max(0.0,dot(lightDirection, halfDirection));
                float3 specularColor = _Specular_var.rgb;
                float specularMonochrome = max( max(specularColor.r, specularColor.g), specularColor.b);
                float NdotV = max(0.0,dot( normalDirection, viewDirection ));
                float NdotH = max(0.0,dot( normalDirection, halfDirection ));
                float VdotH = max(0.0,dot( viewDirection, halfDirection ));
                float visTerm = SmithBeckmannVisibilityTerm( NdotL, NdotV, 1.0-gloss );
                float normTerm = max(0.0, NDFBlinnPhongNormalizedTerm(NdotH, RoughnessToSpecPower(1.0-gloss)));
                float specularPBL = max(0, (NdotL*visTerm*normTerm) * unity_LightGammaCorrectionConsts_PIDiv4 );
                float3 directSpecular = attenColor * pow(max(0,dot(halfDirection,normalDirection)),specPow)*specularPBL*lightColor*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float3 directDiffuse = ((1 +(fd90 - 1)*pow((1.00001-NdotL), 5)) * (1 + (fd90 - 1)*pow((1.00001-NdotV), 5)) * NdotL) * attenColor;
                float3 diffuseColor = _Diffuse_var.rgb;
                diffuseColor *= 1-specularMonochrome;
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
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
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
                o.pos = UnityObjectToClipPos(v.vertex);
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
/////// Vectors:
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
