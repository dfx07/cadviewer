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

float sdBox(vec2 p, vec2 b)
{
    vec2 d = abs(p)- b - vThickness * 0.5;
    return length(max(d,0.0)) + min(max(d.x,d.y),0.0) + vThickness * 0.5;
}

float sdRoundBox(vec2 p, vec2 b, vec4 r) 
{
    r.xy = (p.x>0.0)?r.xy : r.zw;
    r.x  = (p.y>0.0)?r.x  : r.y;
    
    vec2 q = abs(p)-b+r.x;
    return min(max(q.x,q.y),0.0) + length(max(q,0.0)) - r.x;
}

float sdBoxRotatedPixel(vec2 fragCoord, vec2 center, vec2 halfSize, float angle)
{
    float angleRad = radians(angle);

    // Đưa pixel về hệ tọa độ local (xoay ngược lại)
    vec2 p = fragCoord - center;

    float c = cos(-angleRad);
    float s = sin(-angleRad);
    mat2 rot = mat2(c, -s, s, c);

    vec2 local = rot * p;

    return sdBox(local, halfSize);
}

void main()
{
    vec2 centerPx = clip_2_screen(vPosCenter);
    vec2 borderPx = clip_2_screen(vPosBorder);

    vec2 sizePx = abs(borderPx - centerPx);

    // 1. Tính khoảng cách không xoay 
    // float dist = sdBox(localPx, vec2(50, 50) - vec2(vThickness / 2.0));

    // 2. Tính khoảng các khi xoay
    float dist = sdBoxRotatedPixel(gl_FragCoord.xy, centerPx, sizePx - vec2(vThickness / 2.0) - vec2(3.0), -35.0);


    // 1. Border khi ở vị trí không xoay
    // float border = 1.0 - step(vThickness * 0.5, abs(dist));

    // 2. Border ở vị trí xoay
    float halfT = vThickness * 0.5;
    float edge = fwidth(dist) * 0.5; // lấy độ rộng biên theo độ dốc
    float border = 1.0 - smoothstep(halfT - edge, halfT + edge, abs(dist));


    // 
    float alpha = vThinkessColor.a * border;
    FragColor = vec4(vThinkessColor.rgb, alpha);


    // if (abs(dist) < vThickness)
    // {
    //     FragColor = vec4(1.0, 1.0, 0.0, 1.0);
    // }
    // else 
    // {
    //     FragColor = vec4(1.0, 0.0, 0.0, 1.0);
    // }





    // vec2 centerPx = clip_2_screen(vPosCenter);
    // vec2 borderPx = clip_2_screen(vPosBorder);

    // vec2 sizePx = (borderPx - centerPx);
    // vec2 localPx = centerPx - gl_FragCoord.xy;

    // float radius = 10.f * u_zZoom;
    // float dist = sdRoundBox(localPx, sizePx - vec2(vThickness / 2.0), vec4(radius));

    // float aa = fwidth(dist);

    // float border = 1.0;

    // vec2 absP = abs(localPx);
    // vec2 inner = sizePx - vec2(vThickness) - vec2(radius);
    // bool inCorner = absP.x > inner.x && absP.y > inner.y;

    // if(vFillColor.a <= 0)
    // {
    //     border = 1.0 - (inCorner ? smoothstep(vThickness * 0.5 - aa * 0.7, vThickness * 0.5 + aa * 0.4, abs(dist)) :
    //                               step(vThickness * 0.5, abs(dist)));

    //     float alpha = vThinkessColor.a * border;
    //     FragColor = vec4(vThinkessColor.rgb, alpha);
    // }
    // else if(vThickness <= 0 || vThinkessColor.a <= 0)
    // {
    //     float fill = 1.0 - smoothstep(-aa, aa, dist);
    //     FragColor = vec4(vFillColor.rgb, vFillColor.a * fill);
    // }
    // else 
    // {
    //     float fill = 1.0 - smoothstep(-aa - 1, aa + 0.5, dist);

    //     if(inCorner)
    //     {
    //         border = 1.0 - smoothstep(vThickness * 0.5 - aa, vThickness * 0.7 + aa * (vThickness == 1 ? 0.1 : 0.1), abs(dist));
    //         border = pow(border, vThickness == 1 ? 0.15 : 0.5); 
    //     }
    //     else 
    //     {
    //         border = 1.0 - step(vThickness * 0.5, abs(dist));
    //     }

    //     float alpha = 1.0;
    //     vec3 color;

    //     if(inCorner)
    //     {
    //         color = mix(vThinkessColor.rgb, vFillColor.rgb, fill * (1.0 - border));
    //         alpha = max(border, fill);
    //         FragColor = vec4(color, alpha);
    //     }
    //     else 
    //     {
    //         color = (fill >= border) ? vFillColor.rgb : vThinkessColor.rgb;
    //         alpha = (fill >= border) ? fill : border;
    //         FragColor = vec4(color, alpha);
    //     }
    // }
}