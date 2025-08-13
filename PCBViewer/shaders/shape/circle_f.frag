#version 330 core

in vec3  v_v3WorldPos;
in vec4  v_v4Color;

flat in vec3  vf_v3WorldCenterPos;
in float v_fRadius;

uniform vec2 u_Viewport;
uniform float u_zZoom;

out vec4 FragColor;

void main()
{
    float fDist = length(v_v3WorldPos.xy - vf_v3WorldCenterPos.xy);

    float aa = fwidth(fDist) * 0.6;

    float fFillAlpha = 1 - smoothstep(v_fRadius - aa, v_fRadius + aa, fDist);
 
    FragColor = vec4(v_v4Color.xyz, v_v4Color.a * fFillAlpha);
}