#version 330 core

in vec4  v_v4Color;

flat in vec2  vf_v2CenterPosPx;
flat in float vf_fRadiusPx;

flat in vec3  vf_v3WorldCenterPos;
flat in float vf_fWorldRadius;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom;

out vec4 FragColor;

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
    vec2 v2PosPx = gl_FragCoord.xy;

    float fDist = length(v2PosPx - vf_v2CenterPosPx);

    float aa = fwidth(fDist);

    float fFillAlpha = 1 - smoothstep(vf_fRadiusPx - aa, vf_fRadiusPx + aa, fDist);
 
    FragColor = vec4(v_v4Color.rgb, v_v4Color.a * fFillAlpha);
}