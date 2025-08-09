#version 330 core

layout(lines_adjacency) in;
layout(triangle_strip, max_vertices = 7) out;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom;

in vec4  v_v4Color[];
in float v_fThickness[];
in float v_fWorldThickness[];
in int   v_nRectID[];
in vec2  v_v2WorldPos[];

out vec4  g_v4Color;
out vec2  g_v2PosPx;

flat out vec2  gf_v2StartPosPx;
flat out vec2  gf_v2EndPosPx;
flat out float gf_fThicknessPx;
flat out int   gf_nAxisAligned;

// Clip Space   : Sau MVP, trước chia w
// NDC Space    : Sau chia w, [-1, 1] x [-1, 1]
// Screen Space : [0, width] x [0, height] (view port)
vec2 clip_2_screen(vec4 vtx)
{
    vec2 ndc = vtx.xy / vtx.w;
    return (ndc * 0.5 + 0.5) * u_Viewport;
}

vec2 clip_2_ndc(vec4 clipPosition)
{
    return clipPosition.xy / clipPosition.w;
}

vec2 ScreenToClip(vec2 screenPos, float w)
{
    vec2 ndc = (screenPos / u_Viewport) * 2.0 - 1.0;
    return ndc * w;
}

vec4 OffsetInClipNoSnap(vec4 clipPos, vec2 offsetPixel, vec2 viewport)
{
    vec2 ndcOffset = (offsetPixel / viewport) * 2.0;
    return clipPos + vec4(ndcOffset * clipPos.w, 0.0, 0.0);
}

bool IsOrthogonal2D(vec2 a, vec2 b)
{
    return abs(dot(a, b)) < 1e-6;
}

bool is_horizontal(vec2 p1, vec2 p2, float epsilon)
{
    return abs(p1.y - p2.y) <= epsilon;
}

bool is_vertical(vec2 p1, vec2 p2, float epsilon)
{
    return abs(p1.x - p2.x) <= epsilon;
}

bool is_axis_aligned_line(vec2 p1, vec2 p2)
{
    float eps = 0.001;
    return is_horizontal(p1, p2, eps) || is_vertical(p1, p2, eps);
}

vec4 WorldToClip(vec3 worldPos) {
    return u_Proj * u_View * u_Model * vec4(worldPos, 1.0);
}

void main()
{
    if(v_nRectID[0] != v_nRectID[1] || v_nRectID[0] != v_nRectID[2])
        return;

    vec2 p0 = clip_2_ndc(gl_in[0].gl_Position);
    vec2 p1 = clip_2_ndc(gl_in[1].gl_Position);
    vec2 p2 = clip_2_ndc(gl_in[2].gl_Position);
    vec2 p3 = clip_2_ndc(gl_in[3].gl_Position);

    // Calculate the direction vectors for the segments
    // p0 to p1, p1 to p2, and p2 to p3
    vec2 v0 = normalize(p1 - p0);
    vec2 v1 = normalize(p2 - p1);
    vec2 v2 = normalize(p3 - p2);

    // Calculate the normal vectors for the segments
    vec2 n0 = vec2(-v0.y, v0.x);
    vec2 n1 = vec2(-v1.y, v1.x);
    vec2 n2 = vec2(-v2.y, v2.x);

    // Calculate the miter vector
    vec2 v2Miter1 = normalize(n0 + n1);
    vec2 v2Miter2 = normalize(n1 + n2);

    float an1 = dot(v2Miter1, n1);
    float bn1 = dot(v2Miter2, n2);

    if(abs(an1) < 0.2)
        an1 = 1;

    if(abs(bn1) < 0.2)
        bn1 = 1;

    gf_v2StartPosPx = clip_2_screen(gl_in[1].gl_Position);
    gf_v2EndPosPx = clip_2_screen(gl_in[2].gl_Position);
    gf_fThicknessPx = v_fThickness[0];
    gf_nAxisAligned = is_axis_aligned_line(gf_v2StartPosPx, gf_v2EndPosPx) ? 1 : 0;

    float fHalfThickness = (v_fThickness[0] * 0.5 ) + (gf_nAxisAligned == 1 ? 0.0 : 1.0);

    float fThickness0 = fHalfThickness / an1;
    float fThickness1 = fHalfThickness / bn1;

    vec2 v2Offset1 = v2Miter1 * fThickness0;
    vec2 v2Offset2 = v2Miter2 * fThickness1;
    
    vec4 pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position,  v2Offset1, u_Viewport);
    vec4 pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, -v2Offset1, u_Viewport);
    vec4 pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position,  v2Offset2, u_Viewport);
    vec4 pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, -v2Offset2, u_Viewport);

    vec2 PixLoc1 = clip_2_screen(pos1);
    vec2 PixLoc2 = clip_2_screen(pos2);
    vec2 PixLoc3 = clip_2_screen(pos3);
    vec2 PixLoc4 = clip_2_screen(pos4);

    g_v2PosPx = PixLoc1;
    g_v4Color = v_v4Color[0];
    gl_Position = pos1;
    EmitVertex();

    g_v2PosPx = PixLoc2;
    g_v4Color = v_v4Color[0];
    gl_Position = pos2;
    EmitVertex();

    g_v2PosPx = PixLoc3;
    g_v4Color = v_v4Color[0];
    gl_Position = pos3;
    EmitVertex();

    g_v2PosPx = PixLoc4;
    g_v4Color = v_v4Color[0];
    gl_Position = pos4;
    EmitVertex();
    EndPrimitive();
}