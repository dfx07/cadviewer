#version 330 core

in vec4  v_v4ThicknessColor;
in vec3  v_v3Pos;

flat in vec2   vf_v2CenterPosPx;
flat in vec3   vf_v3CenterPos;
flat in vec2   vf_v2SizePx;
flat in vec2   vf_v2Size;
flat in vec2   vf_v2NegRotAngle;
flat in float  vf_fThickness;
flat in float  vf_fThicknessPx;
flat in int    vf_nAA;

uniform vec2 u_Viewport;
uniform float u_zZoom;

out vec4 FragColor;

float sdRoundBox(vec2 p, vec2 b, vec4 r)
{
	r.xy = (p.x > 0.0) ? r.xy : r.zw;
	r.x = (p.y > 0.0) ? r.x : r.y;

	vec2 q = abs(p) - b + r.x;
	return min(max(q.x, q.y), 0.0) + length(max(q, 0.0)) - r.x;
}

float sdBox(vec2 p, vec2 b)
{
	vec2 d = abs(p) - b - vf_fThicknessPx;
	return length(max(d, 0.0)) + min(max(d.x, d.y), 0.0) + vf_fThicknessPx;
}

float sdBoxRotated(vec2 fragCoord, vec2 center, vec2 halfSize, vec2 vAngleSC)
{
    vec2 p = fragCoord - center;

    float c = vAngleSC.x;
    float s = vAngleSC.y;
    mat2 rot = mat2(c, -s, s, c);

    vec2 local = rot * p;

    return sdBox(local, halfSize);
}

float map_range_or_keep(float value, float inMin, float inMax, float outMin, float outMax)
{
    if (value < inMin || value > inMax)
        return value;

    float t = (value - inMin) / (inMax - inMin);
    return mix(outMin, outMax, t);
}

void main()
{
    if(vf_nAA == 1)
    {
        float fHalfThickness = vf_fThickness * 0.3;

        vec2 v2Pos = v_v3Pos.xy;
        float fDist = sdBoxRotated(v2Pos, vf_v3CenterPos.xy, vf_v2Size / 2.f, vf_v2NegRotAngle);

        float aa = fwidth(fDist) * 0.5;
        float fBorderAlpha = 1 - smoothstep(fHalfThickness - aa, fHalfThickness + aa, abs(fDist));

        FragColor = vec4(v_v4ThicknessColor.rgb, v_v4ThicknessColor.a * fBorderAlpha);
    }
    else 
    {
        float fHalfThicknessPx = vf_fThicknessPx * 0.5;

        vec2 vSize = vf_v2SizePx + vec2(0.5);
        vec2 v2Pos = gl_FragCoord.xy;

        float fDist = sdBoxRotated(v2Pos, vf_v2CenterPosPx, vSize / 2.f, vf_v2NegRotAngle);

        if(abs(fDist) <= fHalfThicknessPx)
        {
            FragColor = v_v4ThicknessColor;
        }
        else 
        {
            discard;
        }
    }
}