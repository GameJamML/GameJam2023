#ifndef CUSTOM_LIGHTING_INCLUDED
#define CUSTOM_LIGHTING_INCLUDED

// This is a neat trick to work around a bug in the shader graph when
// enabling shadow keywords. Created by @cyanilux
// https://github.com/Cyanilux/URP_ShaderGraphCustomLighting
#ifndef SHADERGRAPH_PREVIEW
	#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
	#if (SHADERPASS != SHADERPASS_FORWARD)
		#undef REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
	#endif
#endif

struct CustomLightingData
{
	float3 positionWS;
	float3 normalWS;
	float3 viewDirectionWS;
	float4 shadowCoord;

	// Surface attributes
	float3 albedo;
	float smoothness;
};

float GetSmoothnessPower(float rawSmoothness)
{
	return exp2(10 * rawSmoothness + 1);
}

#ifndef SHADERGRAPH_PREVIEW
float3 CustomLightHandling(CustomLightingData data, Light light)
{
	float3 radiance = light.color * (light.distanceAttenuation * light.shadowAttenuation);

	float diffuse = saturate(dot(data.normalWS, light.direction));
	float specularDot = saturate(dot(data.normalWS, normalize(light.direction + data.viewDirectionWS)));
	float specular = pow(specularDot, GetSmoothnessPower(data.smoothness)) * diffuse;

	float3 color = data.albedo * radiance * (diffuse + specular);

	return color;
}
#endif

float3 CalculateCustomLighting(CustomLightingData data)
{
#ifdef SHADERGRAPH_PREVIEW
	float3 lightDir = float3(0.5, 0.5, 0);
	float intensity = saturate(dot(data.normalWS, lightDir)) +
		pow(saturate(dot(data.normalWS, normalize(data.viewDirectionWS + lightDir))), GetSmoothnessPower(data.smoothness));
	return data.albedo * intensity;
#else
	Light mainLight = GetMainLight(data.shadowCoord, data.positionWS, 1);

	float3 color = 0;

	color += CustomLightHandling(data, mainLight);

#ifdef _ADDITIONAL_LIGHTS
	uint numAdditionalLights = GetAdditionalLightsCount();
	for (uint i = 0; i < numAdditionalLights; i++)
	{
		Light light = GetAdditionalLight(i, data.positionWS, 1);
		color += CustomLightHandling(data, light);
	}
#endif

	return color;
#endif
}

void CalculateCustomLighting_float(float3 Position, float3 Normal, float3 ViewDirection, float3 Albedo, float Smoothness, out float3 Color)
{
	CustomLightingData data;
	data.positionWS = Position;
	data.normalWS = Normal;
	data.viewDirectionWS = ViewDirection;
	data.albedo = Albedo;
	data.smoothness = Smoothness;

#ifdef SHADERGRAPH_PREVIEW
	data.shadowCoord = 0;
#else
	float4 positionCS = TransformWorldToHClip(Position);
	#if SHADOWS_SCREEN
		data.shadowCoord = ComputeScreenPos(positionCS);
	#else
		data.shadowCoord = TransformWorldToShadowCoord(Position);
	#endif
#endif

	Color = CalculateCustomLighting(data);
}

#endif