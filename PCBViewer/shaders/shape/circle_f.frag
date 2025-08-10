#version 330 core

in vec3  v_v3WorldPos;
in vec4  v_v4Color;

flat in vec3  vf_v3WorldCenterPos;
in float v_fRadius;

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
    // vec2 centerPx = clip_2_screen(vClipCenter);
    // vec2 edgePx   = clip_2_screen(vClipEdge);

    // float radiusPx = length(centerPx - edgePx);

    // float distPx = length(gl_FragCoord.xy - centerPx);

    // float outer = radiusPx;
    // float inner = radiusPx - (vThickness + 1.3f);

    // float aa = fwidth(distPx);

    // float borderAlpha = smoothstep(outer, outer - aa - 0.25f, distPx) *
    //                     smoothstep(inner, inner + aa + 0.25f, distPx);

    // float fillAlpha = smoothstep(inner + 3.f, inner, distPx + 1.0);

    // if(distPx < inner + 0.3)
    // {
    //     if(vFillColor.a <= 0)
    //         discard;
    //     else
    //         FragColor = vec4(vFillColor.rgb, fillAlpha);
    // }
    // else
    
    float fDist = length(v_v3WorldPos.xy - vf_v3WorldCenterPos.xy);

    // float fFillAlpha = 1 - smoothstep(v_fRadius * u_zZoom - 1.0, v_fRadius * u_zZoom + 1.0, fDist);
 
    if(fDist < v_fRadius)
    {
        FragColor = vec4(v_v4Color.xyz, 1.0);
    }
    else 
    {
        FragColor = vec4(1.0, 1.0, 0.0, 1.0);
    }

}