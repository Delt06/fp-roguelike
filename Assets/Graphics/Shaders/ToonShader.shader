﻿Shader "Stylized/ToonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint", Color) = (1.0, 1.0, 1.0)
        _ShadowTint ("ShadowTint", Color) = (0.0, 0.0, 0.0)
        _ShadowHardness ("ShadowHardness", Range(0, 1)) = 1
        _Ramp0 ("Ramp0", Range(-1, 1)) = 0
        _Ramp1 ("Ramp1", Range(-1, 1)) = 0.5
        _RampSmoothness ("RampSmoothness", Range(0, 1)) = 0.005
        
        [HDR] _Emission ("Emission", Color) = (0.0, 0.0, 0.0, 0.0)
        
        _FresnelThickness ("FresnelThickness", Range(0, 1)) = 0.25
        _FresnelSmoothness ("FresnelSmoothness", Range(0, 1)) = 0.1
        _FresnelOpacity ("FresnelOpacity", Range(0, 1)) = 1
        
        _SpecularSize ("SpecularSize", Range(0, 1)) = 0.025
        _SpecularSmoothness ("SpecularSmoothness", Range(0, 1)) = 0.05
        _SpecularOpacity ("SpecularOpacity", Range(0, 1)) = 1
    }
    SubShader
    {
        Tags{"RenderType" = "Opaque" "RenderPipeline" = "UniversalRenderPipeline" "IgnoreProjector" = "True"}
        LOD 100

        Pass
        {
            HLSLPROGRAM
            
            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0
            
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS
            #pragma multi_compile _ _MAIN_LIGHT_SHADOWS_CASCADE
            #pragma multi_compile _ _SHADOWS_SOFT
            #pragma multi_compile_fog

            #pragma prefer_hlslcc gles
            #pragma exclude_renderers d3d11_9x
            #pragma target 2.0

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"

            struct appdata
            {
                float4 positionOS : POSITION;
                float3 normalOS : NORMAL;
                float4 tangentOS : TANGENT;
                float2 uv : TEXCOORD0;
                float2 uvLM : TEXCOORD1;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float2 uvLM : TEXCOORD1;
                float4 positionWSAndFogFactor   : TEXCOORD2; // xyz: positionWS, w: vertex fog factor
                half3  normalWS : TEXCOORD3;

#ifdef _MAIN_LIGHT_SHADOWS
                float4 shadowCoord : TEXCOORD6;
#endif
                float4 positionCS : SV_POSITION;
            };

            CBUFFER_START(UnityPerMaterial)
            sampler2D _MainTex;
            float4 _MainTex_ST;
            half3 _ShadowTint;
            half _Ramp0;
            half _Ramp1;
            half _ShadowHardness;
            half _RampSmoothness;
            half3 _Tint;
            half3 _Emission;
            
            half _FresnelOpacity;
            half _FresnelSmoothness;
            half _FresnelThickness;

            half _SpecularOpacity;
            half _SpecularSmoothness;
            half _SpecularSize;
            
            CBUFFER_END

            v2f vert (appdata v)
            {
                v2f output;
                VertexPositionInputs vertex_position_inputs = GetVertexPositionInputs(v.positionOS.xyz);
                VertexNormalInputs vertex_normal_inputs = GetVertexNormalInputs(v.normalOS, v.tangentOS);
                output.uv = TRANSFORM_TEX(v.uv, _MainTex);
                output.uvLM = v.uvLM.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                float fog_factor = ComputeFogFactor(vertex_position_inputs.positionCS.z);
                output.positionWSAndFogFactor = float4(vertex_position_inputs.positionWS, fog_factor);
                output.normalWS = vertex_normal_inputs.normalWS;
#ifdef _MAIN_LIGHT_SHADOWS
                output.shadowCoord = GetShadowCoord(vertex_position_inputs);
#endif
                output.positionCS = vertex_position_inputs.positionCS;
                return output;
            }

            inline Light get_main_light(v2f input)
            {
                float4 shadow_coord;
#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
                shadow_coord = input.shadowCoord;
#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
                shadow_coord = TransformWorldToShadowCoord(i.positionWSAndFogFactor.xyz);
#else
                shadow_coord = float4(0, 0, 0, 0);
#endif
                return GetMainLight(shadow_coord);
            }

            inline half3 get_simple_ramp(half3 color, half opacity, half thickness, half smoothness, half value)
            {
                smoothness *= thickness;
                color *= opacity;
                return color * smoothstep(1 - thickness, 1 - thickness + smoothness, value);
            }

            inline half get_specular(half3 view_direction_ws, half3 normal_ws, half3 light_direction_ws)
            {
                half3 half_vector = (view_direction_ws + light_direction_ws) * 0.5;
                return dot(normal_ws, half_vector);
            }

            inline half3 get_specular_color(half3 light_color, half3 view_direction_ws, half3 normal_ws, half3 light_direction_ws, half brightness)
            {
                half specular = get_specular(view_direction_ws, normal_ws, light_direction_ws);
                return get_simple_ramp(light_color, _SpecularOpacity, _SpecularSize, _SpecularSmoothness, specular * brightness);
            }

            inline half get_fresnel(half3 view_direction_ws, half3 normal_ws)
            {
                return 1 - dot(view_direction_ws, normal_ws); 
            }

            inline half3 get_fresnel_color(half3 light_color, half3 view_direction_ws, half3 normal_ws, half brightness)
            {
                half fresnel = get_fresnel(view_direction_ws, normal_ws);
                return get_simple_ramp(light_color, _FresnelOpacity, _FresnelThickness, _FresnelSmoothness, brightness * fresnel);
            }

            inline half get_triple_ramp(half value)
            {
                half ramp0 = smoothstep(_Ramp0, _Ramp0 + _RampSmoothness, value) * 0.5;
                half ramp1 = smoothstep(_Ramp1, _Ramp1 + _RampSmoothness, value) * 0.5;
                return ramp0 + ramp1;
            }

            inline half get_brightness(half3 normal_ws, half3 light_direction, half shadow_attenuation, half distance_attenuation)
            {
                half dot_value = dot(normal_ws, light_direction);
                half ramp = get_triple_ramp(dot_value);
                half attenuation = shadow_attenuation * distance_attenuation;
                return saturate(ramp * attenuation);
            }

            half3 frag (v2f input) : SV_Target
            {
                Light main_light = get_main_light(input);
                half3 normal_ws = normalize(input.normalWS);
                half3 light_direction_ws = normalize(main_light.direction);
                half3 view_direction_ws = SafeNormalize(GetCameraPositionWS() - input.positionWSAndFogFactor.xyz);
 
                half3 sample_color = (half3) tex2D(_MainTex, input.uv) * _Tint;
                sample_color *= main_light.color;
                half brightness = get_brightness(normal_ws, light_direction_ws, main_light.shadowAttenuation, main_light.distanceAttenuation);
                half3 shadow_color = lerp(sample_color, _ShadowTint, _ShadowHardness);
                half3 fragment_color = lerp(shadow_color, sample_color, brightness);
                fragment_color += get_specular_color(main_light.color, view_direction_ws, normal_ws, light_direction_ws, brightness);
                fragment_color += get_fresnel_color(main_light.color, view_direction_ws, normal_ws, brightness);
                
                fragment_color += _Emission;
                float fog_factor = input.positionWSAndFogFactor.w;
                fragment_color = MixFog(fragment_color, fog_factor);
                
                return saturate(fragment_color);
            }
            ENDHLSL
        }
        
        UsePass "Universal Render Pipeline/Lit/ShadowCaster"
        UsePass "Universal Render Pipeline/Lit/DepthOnly"
    }
}
