#version 150 core

layout(lines_adjacency) in;
layout(triangle_strip, max_vertices = 7) out;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom; // Tỷ lệ zoom, có thể dùng để điều chỉnh độ dày đường vẽ

in vec4 vColor[];     // from vertex shader
in float vThickness[]; // from vertex shader
in int vPolygonID[];

out vec4 fColor;
out vec2 vLoc;
flat out vec2 vSLine;
flat out vec2 vELine;
flat out float fThickness;

flat out int nStartJoin;
flat out int nEndJoin;

// Clip Space   : Sau MVP, trước chia w
// NDC Space    : Sau chia w, [-1, 1] x [-1, 1]
// Screen Space : [0, width] x [0, height] (view port)
vec2 ClipToScreen(vec4 vtx)
{
    vec2 ndc = vtx.xy / vtx.w;
    return (ndc * 0.5 + 0.5) * u_Viewport;
}

vec2 ScreenToClip(vec2 screenPos, float w)
{
    vec2 ndc = (screenPos / u_Viewport) * 2.0 - 1.0;
    return ndc * w;
}

bool IsCardinalDirection(vec2 dir)
{
    const float epsilon = 0.0001;

    bool right = abs(dir.x - 1.0) < epsilon && abs(dir.y) < epsilon;
    bool left  = abs(dir.x + 1.0) < epsilon && abs(dir.y) < epsilon;
    bool up    = abs(dir.y - 1.0) < epsilon && abs(dir.x) < epsilon;
    bool down  = abs(dir.y + 1.0) < epsilon && abs(dir.x) < epsilon;

    return right || left || up || down;
}

vec4 OffsetClipByPixel(vec4 clipPos, float offset, vec2 normal, vec2 viewport)
{
    // Convert normal (pixel space) to NDC
    vec2 ndcOffset = (offset * normalize(normal) / viewport) * 2.0;

    // Convert NDC offset to clip space offset (multiply by w)
    vec2 clipOffset = ndcOffset * clipPos.w;

    // Apply offset to clipPos
    return clipPos + vec4(clipOffset, 0.0, 0.0);
}

// OffsetInClip: Tính toán vị trí mới trong clip space dựa trên offset pixel
// Nhân 2.0 vì Chuyển từ pixel → tỷ lệ 0..1  nhưng trong NDC là [-1, 1]
vec4 OffsetInClipSnap(vec4 clipPos, vec2 offsetPixel, vec2 viewport, bool snap)
{
    // Chuyển clip → screen
    vec2 screen = ClipToScreen(clipPos);

    // Thêm offset theo pixel
    vec2 offsetScreen = screen + offsetPixel;

    // Snap về tâm pixel
    if(snap)
    {
        offsetScreen = floor(offsetScreen);
    }
    else
    {
        offsetScreen = offsetScreen; // Snap to lower left corner pixel
    }

    // Quay lại clip space
    vec2 snappedClipXY = ScreenToClip(offsetScreen, clipPos.w);

    return vec4(snappedClipXY, clipPos.zw);
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

void main()
{
    if(vPolygonID[0] != vPolygonID[1] || vPolygonID[0] != vPolygonID[2])
        return;

    vec2 p0 = ClipToScreen(gl_in[0].gl_Position);
    vec2 p1 = ClipToScreen(gl_in[1].gl_Position);
    vec2 p2 = ClipToScreen(gl_in[2].gl_Position);
    vec2 p3 = ClipToScreen(gl_in[3].gl_Position);

    // Calculate the direction vectors for the segments
    // p0 to p1, p1 to p2, and p2 to p3
    vec2 v0 = normalize(p1 - p0);
    vec2 v1 = normalize(p2 - p1);
    vec2 v2 = normalize(p3 - p2);

    // Calculate the normal vectors for the segments
    // vec2 n0 = vec2(-v0.y, v0.x);
    vec2 n1 = vec2(-v1.y, v1.x);
    // vec2 n2 = vec2(-v2.y, v2.x);

    // float thickness = vThickness[0] * 0.5 * (u_zZoom); // Nhân 2 vì pixel thickness
    float thickness = vThickness[0] * 0.5 + 1.f; // Nhân 2 vì pixel thickness
    float half_thickness = vThickness[0] * 0.5;

    vec4 pos1, pos2, pos3, pos4;

    vec2 miter_p1 = n1;
    float length_a = thickness;

    vec2 miter_p2 = n1;
    float length_b = thickness;

    vec2 offsetP1 = miter_p1 * length_a;
    vec2 offsetP2 = miter_p2 * length_b;

    nStartJoin = IsOrthogonal2D(v0, v1) ? 0 : 1;
    pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position, - v1 * half_thickness + offsetP1, u_Viewport);
    pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, - v1 * half_thickness - offsetP1, u_Viewport);

    nEndJoin = IsOrthogonal2D(v1, v2) ? 0 : 1;
    if(nEndJoin == 1)
    {
        pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position, v1 * (half_thickness + 0.8f) + offsetP2, u_Viewport);
        pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, v1 * (half_thickness + 0.8f) - offsetP2, u_Viewport);
    }
    else 
    {
        pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position, v1 * half_thickness + offsetP2, u_Viewport);
        pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, v1 * half_thickness - offsetP2, u_Viewport);
    }

    fColor = vColor[0];
    vLoc = ClipToScreen(pos1);
    vSLine = p1;
    vELine = p2;
    fThickness = vThickness[0];
    gl_Position = pos1;
    EmitVertex();

    fColor = vColor[0];
    vLoc =  ClipToScreen(pos2);
    vSLine = p1;
    vELine = p2;
    fThickness = vThickness[0];
    gl_Position = pos2;
    EmitVertex();

    fColor = vColor[0];
    vLoc = ClipToScreen(pos3);
    vSLine = p1;
    vELine = p2;
    fThickness = vThickness[0];
    gl_Position = pos3;
    EmitVertex();
   
    fColor = vColor[0];
    vLoc = ClipToScreen(pos4);
    vSLine = p1;
    vELine = p2;
    fThickness = vThickness[0];
    gl_Position = pos4;
    EmitVertex();

    EndPrimitive();
}