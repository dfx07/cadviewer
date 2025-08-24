#version 330 core

in vec4 v_v4Color;
in float v_fThickness;

flat in vec2  vf_v2CenterPosPx;
flat in float vf_fRadiusPx;

out vec4 FragColor;

void main()
{
    vec2 v2PosPx = gl_FragCoord.xy;

    float fDist = length(v2PosPx - vf_v2CenterPosPx);

    float halfT = v_fThickness * 0.5;

    float aa = fwidth(fDist) * 0.8;

    float borderAlpha = 1.0 - smoothstep(halfT - aa, halfT + aa, abs(vf_fRadiusPx - fDist));

    FragColor = vec4(v_v4Color.rgb, v_v4Color.a * borderAlpha);

    // if(fDist <= vf_fRadiusPx)
    // {
    //     FragColor = vec4(v_v4Color.rgb, 1.0);
    // }
    // else 
    // {
    //     FragColor = vec4(0.0, 1.0, 0.0, 1.0);
    // }
}