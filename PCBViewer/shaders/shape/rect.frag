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

    vec2 sizePx = (borderPx - centerPx);
    vec2 localPx = centerPx - gl_FragCoord.xy;

    float radius = 10.f * u_zZoom;

    vec2 offset = vThickness == 1 ? vec2(0.5) : vec2(vThickness - 1);

    float dist = sdRoundBox(localPx, sizePx - offset, vec4(radius));

    float aa = fwidth(dist);

    float border = 1.0;

    vec2 absP = abs(localPx);
    vec2 inner = sizePx - vec2(vThickness) - vec2(radius);
    bool inCorner = absP.x > inner.x && absP.y > inner.y;

    float fill = 1.0 - smoothstep(-aa, aa, dist);

    if(vFillColor.a <= 0)
    {
        if(inCorner)
        {
            border = 1.0 - smoothstep(vThickness * 0.5 - aa* 0.4, vThickness * 0.5 + aa * 0.3, abs(dist));
        }
        else 
        {
            border = 1.0 - step(vThickness * 0.5, abs(dist));
        }

        FragColor = vec4(vThinkessColor.rgb, vThinkessColor.a * border);
    }
    else if(vThickness <= 0 || vThinkessColor.a <= 0)
    {
        FragColor = vec4(vFillColor.rgb, vFillColor.a * fill);
    }
    else 
    {
        if(inCorner)
        {
            border = 1.0 - smoothstep(vThickness * 0.5 - aa * 0.4, vThickness * 0.5 + aa * 0.3, abs(dist));
        }
        else 
        {
            border = 1.0 - step(vThickness * 0.5, abs(dist));
        }
        float borderStrong = pow(border, 0.5); 


        float blend = pow(fill * (1.0 - border), 0.6);
        vec3 color = mix(vThinkessColor.rgb, vFillColor.rgb, blend);

        float alpha = max(borderStrong, fill);

        FragColor = vec4(color, alpha);
    }
}