#version 330 core

in vec2 vLocalUV;
flat in vec4 vCenterClipPos;
in float vRadius;
in float vThickness;
// in vec4 vBorderColor;
// in vec4 vFillColor;

flat in vec4 vClipCenter;
flat in vec4 vClipEdge;

uniform vec2 u_Viewport;
uniform float u_zZoom; 


out vec4 FragColor;

vec2 clip_2_screen(vec4 vtx)
{
    vec2 ndc = vtx.xy / vtx.w;
    return (ndc * 0.5 + 0.5) * u_Viewport;
}


void main()
{
    // vec3 ndc = vCenterClipPos.xyz / vCenterClipPos.w; // NDC ∈ [-1,1]
    // vec2 center = (ndc.xy * 0.5 + 0.5) * u_Viewport; // screen-space ∈ [0, resolution]

    // float dist = length(gl_FragCoord.xy - center);

    // if(dist < 10)
    //     FragColor = vec4(1.0, 0, 0, 1.0);
    // else if (dist < 20)
    // {
    //     FragColor = vec4(0.0, 1.0, 0, 1.0);
    // }
    // else if (dist < 30)
    // {
    //     FragColor = vec4(1.0, 1.0, 0, 1.0);
    // }
    // else if (dist < 40)
    // {
    //     FragColor = vec4(0.0, 1.0, 1.0, 1.0);
    // }
    // else 
    //     FragColor = vec4(0.0, 0.0, 1.0, 0.5);

    // float dist = length(vLocalUV); // from center
    // float outer = 1.0;
    // float inner = (outer - (vThickness / vRadius)); // normalized thickness

    // float aa = fwidth(dist);

    // float borderAlpha = smoothstep(outer, outer - aa, dist) * smoothstep(inner, inner + aa, dist);
    // float fillAlpha   = smoothstep(inner, inner - aa, dist);

    // vec4 color = mix(vec4(1.0, 1.0, 0.0, 1.0), vec4(1.0, 0.0, 0.0, 1.0), borderAlpha);
    // color.a = max(borderAlpha, fillAlpha) * color.a;

    // if (color.a < 0.01)
    //     discard;

    // FragColor = color;


    // Tính bán kính pixel thật trên màn hình
    vec2 centerNDC = vClipCenter.xy / vClipCenter.w;
    vec2 edgeNDC   = vClipEdge.xy   / vClipEdge.w;

    vec2 centerPx = (centerNDC * 0.5f + 0.5f) * u_Viewport;
    vec2 edgePx   = (edgeNDC   * 0.5f + 0.5f) * u_Viewport;

    float radiusPx = length(centerPx - edgePx);

    // // Chuyển thickness từ pixel sang UV
    // float uvThickness = vThickness / radiusPx;

    // // Dùng vLocalUV để tô hình tròn với độ dày chuẩn hóa
    // float dist = length(vLocalUV);   // ∈ [0, 1]
    // float outer = 1.0;
    // float inner = outer - uvThickness;

    // float aa = fwidth(dist);

    // float borderAlpha = smoothstep(outer, outer - aa, dist) *
    //                     smoothstep(inner, inner + aa, dist);
    // float fillAlpha = smoothstep(inner, inner - 0.0001, dist);

    // vec4 fillColor = vec4(1.0, 1.0, 0.0, 1.0);   // vàng
    // vec4 borderColor = vec4(1.0, 0.0, 0.0, 1.0); // đỏ

    // if(dist <= inner )
    // {
    //     FragColor = vec4(fillColor.rgb, fillAlpha);
    // }
    // else 
    // {
    //     FragColor = vec4(borderColor.rgb, borderAlpha);
    // }

    vec2 fragToCenter = gl_FragCoord.xy - centerPx;
    float distPx = length(fragToCenter);

    float outer = radiusPx;
    float inner = radiusPx - (vThickness + 1.3f);

    float aa = fwidth(distPx);

    float borderAlpha = smoothstep(outer, outer - aa - 0.25f, distPx) *
                        smoothstep(inner, inner + aa + 0.25f, distPx);

    float fillAlpha = smoothstep(inner + 2.f, inner, distPx + 0.5);

    vec4 fillColor = vec4(1.0, 1.0, 0.0, 1.0);   // vàng
    vec4 borderColor = vec4(1.0, 0.0, 0.0, 1.0); // đỏ

    if(distPx < inner + 0.1)
        FragColor = vec4(fillColor.rgb, fillAlpha);
    else
        FragColor = vec4(borderColor.rgb, borderAlpha);

    // vec4 color = mix(fillColor, borderColor, borderAlpha);
    // color.a = max(borderAlpha, fillAlpha) * color.a;

    // if (color.a < 0.01)
    //     discard;

    // FragColor = color;
}