#version 330 core

in vec2  vLocalUV;
in vec2  vSize;
in float vThickness;
in vec4  vThinkessColor;
in vec4  vFillColor;

flat in vec4 vPosCenter;
flat in vec4 vRealSize;
flat in vec4 vPosBorder;

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
    r.xy = (p.x>0.0)?r.xy : r.zw;
    r.x  = (p.y>0.0)?r.x  : r.y;
    
    vec2 q = abs(p)-b+r.x;
    return min(max(q.x,q.y),0.0) + length(max(q,0.0)) - r.x;
}

void main()
{
    vec2 centerPx = clip_2_screen(vPosCenter);
    vec2 borderPx = clip_2_screen(vPosBorder);

    vec2 sizePx = borderPx - centerPx;

    float radius = 5.f * u_zZoom;

    float dist = sdRoundBox(centerPx - gl_FragCoord.xy, sizePx - vec2(vThickness), vec4(radius));

    float aa = fwidth(dist);

    float border = 1.0;

    border = 1.0 - smoothstep(vThickness * 0.5 - aa - 0.1, vThickness * 0.5 + aa - 0.1, abs(dist));

    float fill   = 1.0 - smoothstep(0.0 - aa, 0.0 + aa, dist);

    if(vFillColor.a <= 0)
    {
        FragColor = vec4(vThinkessColor.rgb, vThinkessColor.a * border);
    }
    else if(vThickness <= 0 || vThinkessColor.a <= 0)
    {
        FragColor = vec4(vFillColor.rgb, vFillColor.a * fill);
    }
    else 
    {
        vec3 color = mix(vThinkessColor.rgb, vFillColor.rgb, fill * (1.0 - border));
        float alpha = max(border, fill);

        FragColor = vec4(color, alpha);
    }
}