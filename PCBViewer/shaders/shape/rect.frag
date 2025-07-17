#version 330 core

in vec2 vLocalUV;
flat in vec4 vCenterClipPos;
in float vRadius;
in float vThickness;
in vec4 vBorderColor;
in vec4 vFillColor;

flat in vec4 vClipCenter;
flat in vec4 vClipEdge;

uniform vec2 u_Viewport;

out vec4 FragColor;

vec2 clip_2_screen(vec4 vtx)
{
    vec2 ndc = vtx.xy / vtx.w;
    return (ndc * 0.5 + 0.5) * u_Viewport;
}

void main()
{
    vec2 centerPx = clip_2_screen(vClipCenter);
    vec2 edgePx   = clip_2_screen(vClipEdge);

    float radiusPx = length(centerPx - edgePx);

    float distPx = length(gl_FragCoord.xy - centerPx);

    float outer = radiusPx;
    float inner = radiusPx - (vThickness + 1.3f);

    float aa = fwidth(distPx);

    float borderAlpha = smoothstep(outer, outer - aa - 0.25f, distPx) *
                        smoothstep(inner, inner + aa + 0.25f, distPx);

    float fillAlpha = smoothstep(inner + 2.f, inner, distPx + 0.5);

    if(distPx < inner + 0.1)
    {
        if(vFillColor.a <= 0)
            discard;
        else
            FragColor = vec4(vFillColor.rgb, fillAlpha);
    }
    else
        FragColor = vec4(vBorderColor.rgb, borderAlpha);
}