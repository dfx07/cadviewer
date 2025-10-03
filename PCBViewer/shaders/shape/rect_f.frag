#version 330 core

in vec4  v_v4FillColor;
in vec3  v_v3WorldPos;

flat in vec2  vf_v2CenterPosPx;
flat in vec3  vf_v3CenterPos;
flat in vec2  vf_v2SizePx;
flat in vec2  vf_v2Size;
flat in vec2  vf_v2NegRotAngle;

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
	vec2 d = abs(p) - b;
	return length(max(d, 0.0)) + min(max(d.x, d.y), 0.0);
}

float sdBoxRotatedPixel(vec2 fragCoord, vec2 center, vec2 halfSize, vec2 vAngleSC)
{
    vec2 p = fragCoord - center;

    float c = vAngleSC.x;
    float s = vAngleSC.y;
    mat2 rot = mat2(c, -s, s, c);

    vec2 local = rot * p;

    return sdBox(local, halfSize);
}

void main()
{
    vec2 v2Pos = v_v3WorldPos.xy;
    float fDist = sdBoxRotatedPixel(v2Pos, vf_v3CenterPos.xy, vf_v2Size / 2.f, vf_v2NegRotAngle);

    float aa = fwidth(fDist) * 0.5;
    float fFillAlpha = 1.0 - smoothstep(-aa, aa, fDist);

    FragColor = vec4(v_v4FillColor.rgb, v_v4FillColor.a * fFillAlpha);
}