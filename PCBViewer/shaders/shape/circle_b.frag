#version 330 core

in vec4 v_v4Color;
in float v_fThickness;

flat in vec2  vf_v2CenterPosPx;
flat in float vf_fRadiusPx;

out vec4 FragColor;

float map_range_or_keep(float value, float inMin, float inMax, float outMin, float outMax)
{
    if (value < inMin || value > inMax)
        return value;

    float t = (value - inMin) / (inMax - inMin);
    return mix(outMin, outMax, t);
}

void main()
{
    vec2 v2PosPx = gl_FragCoord.xy;

    float fDist = length(v2PosPx - vf_v2CenterPosPx);

    float fScale = map_range_or_keep(v_fThickness, 1, 4, 0.3, 0.5);

    float halfT = v_fThickness * fScale;

    float aa = fwidth(fDist) * 0.8;

    float borderAlpha = 1.0 - smoothstep(halfT - aa, halfT + aa, abs(vf_fRadiusPx - fDist));

    FragColor = vec4(v_v4Color.rgb, v_v4Color.a * borderAlpha);
}