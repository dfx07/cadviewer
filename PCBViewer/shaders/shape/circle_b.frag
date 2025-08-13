#version 330 core

in vec4 v_v4Color;
in float v_fThickness;

flat in vec2  vf_v2CenterPosPx;
flat in float vf_fRadius;

out vec4 FragColor;

void main()
{
    FragColor = vec4(v_v4Color.rgb, 1.0);

    vec2 v2PosPx = gl_FragCoord.xy;

    float fDist = length(v2PosPx - vf_v2CenterPosPx);

    float fOuter = vf_fRadius + v_fThickness * 0.5;
    float fInner = vf_fRadius - v_fThickness * 0.5;

    float aa = fwidth(fDist);

    float fBorderAlpha = smoothstep(fOuter, fOuter - aa, fDist) *
                         smoothstep(fInner, fInner + aa, fDist);

    FragColor = vec4(v_v4Color.rgb, v_v4Color.a * fBorderAlpha);
}