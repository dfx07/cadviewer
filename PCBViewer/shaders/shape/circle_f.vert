#version 330 core

layout(location = 0) in vec2  a_v2LocalUV;
layout(location = 1) in vec3  a_v3WorldCenterPos;
layout(location = 2) in float a_fRadius;
layout(location = 3) in vec4  a_v4Color;

out vec4  v_v4Color;

flat out vec2  vf_v2CenterPosPx;
flat out float vf_fRadiusPx;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom;

vec2 WorldToPixel(vec3 worldPos, vec2 viewSize)
{
    // Step 1: World → Clip Space
    vec4 clipPos = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    // Step 1: Clip Space → NDC
    vec3 ndc = clipPos.xyz / clipPos.w; // [-1, 1]

    // Step 1: NDC → Pixel Space
    vec2 pixelPos = (ndc.xy * 0.5 + 0.5) * viewSize;

    return pixelPos;
}

void main()
{
    vec3 v3WorldPos = a_v3WorldCenterPos + vec3((a_fRadius + 1.0 / u_zZoom) * a_v2LocalUV, a_v3WorldCenterPos.z);
    gl_Position = u_Proj * u_View * u_Model * vec4(v3WorldPos, 1.0);

    vf_v2CenterPosPx = WorldToPixel(a_v3WorldCenterPos, u_Viewport);

    vec3 v3WorldEdgePos = a_v3WorldCenterPos + vec3(a_fRadius * vec2(1.0, 0.0), a_v3WorldCenterPos.z);
    vec2 v2EdgePosPx = WorldToPixel(v3WorldEdgePos, u_Viewport);

    vf_fRadiusPx = length(v2EdgePosPx - vf_v2CenterPosPx);

    v_v4Color = a_v4Color;
}