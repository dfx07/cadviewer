#version 330 core

in vec2  vLocalUV;
in vec2  vSize;
in vec4  vFillColor;
in vec2  vRealPos;

flat in vec2 vRealPosCenter;
flat in vec4 vPosCenter;
flat in vec4 vRealSize;
flat in vec4 vPosBorder;
flat in vec2 vNegRotAngle;
flat in int nLevelAA;

uniform vec2 u_Viewport;
uniform float u_zZoom;

out vec4 FragColor;

vec2 clip_2_screen(vec4 vtx)
{
    vec2 ndc = vtx.xy / vtx.w;
    return (ndc * 0.5 + 0.5) * u_Viewport;
}

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
    // Disable anti-aliasing for debugging purposes
    if(nLevelAA == 0)
    {
        FragColor = vFillColor;
        return;
    }

    vec2 centerPx = clip_2_screen(vPosCenter);
    vec2 borderPx = clip_2_screen(vPosBorder);

    // vec2 halfRectPx = borderPx - centerPx;

    // mat2 rot = mat2(vNegRotAngle.x, -vNegRotAngle.y,
    //                 vNegRotAngle.y,  vNegRotAngle.x);

    // vec2 local = rot * halfRectPx;

    // vec2 sizePx = 2.0 * abs(local);

    vec2 sizePx = abs(borderPx - centerPx);

    float radius = 10.0;
    float dist = sdBoxRotatedPixel(vRealPos, vRealPosCenter, vSize / 2.f, vNegRotAngle);

    float aa = fwidth(dist) * 0.5;
    float fill = 1.0 - smoothstep(-aa, aa, dist);
    FragColor = vec4(vFillColor.rgb, vFillColor.a * fill);
}