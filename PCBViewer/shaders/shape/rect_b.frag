#version 150 core

in vec4  g_v4Color;
in vec2  g_v2PosPx;

flat in vec2  gf_v2StartPosPx;
flat in vec2  gf_v2EndPosPx;
flat in float gf_fThicknessPx;
flat in int   gf_nAxisAligned;

out vec4 FragColor;

float distance_to_segment(vec2 P, vec2 A, vec2 B)
{
   vec2 g = B - A;
   vec2 h = P - A;
   float d = length(h - g * clamp(dot(g, h) / dot(g,g), 0.0, 1.0));
   return d;
}

float signed_distance_to_line(vec2 P, vec2 A, vec2 B)
{
    vec2 dir = normalize(B - A);
    vec2 normal = vec2(-dir.y, dir.x);
    return dot(P - A, normal);
}

float distance_to_line(vec2 P, vec2 A, vec2 B)
{
    return abs(signed_distance_to_line(P, A, B));
}

void main()
{
    if(gf_nAxisAligned == 0)
    {
        float fHalfThickness = gf_fThicknessPx * 0.5;
        float dist = distance_to_line(g_v2PosPx, gf_v2StartPosPx, gf_v2EndPosPx);
        
        float aa = max(fwidth(dist), 1.0) * 0.4;
        float alpha = smoothstep(fHalfThickness + aa, fHalfThickness - aa, dist);

        FragColor = vec4(g_v4Color.rgb, g_v4Color.a * alpha);
    }
    else
    {
        FragColor = vec4(g_v4Color.rgb, g_v4Color.a);
    }
}