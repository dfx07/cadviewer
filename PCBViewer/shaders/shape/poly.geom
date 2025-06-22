#version 150 core

layout(lines_adjacency) in;
layout(triangle_strip, max_vertices = 4) out;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

in vec4 vColor[];     // from vertex shader
in float vThickness[]; // from vertex shader

out vec4 fColor;

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

// OffsetInClip: Tính toán vị trí mới trong clip space dựa trên offset pixel
// Nhân 2.0 vì Chuyển từ pixel → tỷ lệ 0..1  nhưng trong NDC là [-1, 1]
vec4 OffsetInClipSnap(vec4 clipPos, vec2 offsetPixel, vec2 viewport)
{
    // Chuyển clip → screen
    vec2 screen = ClipToScreen(clipPos);

    // Thêm offset theo pixel
    vec2 offsetScreen = screen + offsetPixel;

    // Snap về tâm pixel
    vec2 snappedScreen = floor(offsetScreen); // Snap to center pixel

    // Quay lại clip space
    vec2 snappedClipXY = ScreenToClip(snappedScreen, clipPos.w);

    return vec4(snappedClipXY, clipPos.zw);
}

vec4 OffsetInClipNoSnap(vec4 clipPos, vec2 offsetPixel, vec2 viewport)
{
    vec2 ndcOffset = (offsetPixel / viewport) * 2.0;
    return clipPos + vec4(ndcOffset * clipPos.w, 0.0, 0.0);
}

void main() {
    vec2 p0 = ClipToScreen(gl_in[0].gl_Position);
    vec2 p1 = ClipToScreen(gl_in[1].gl_Position);
    vec2 p2 = ClipToScreen(gl_in[2].gl_Position);
    vec2 p3 = ClipToScreen(gl_in[3].gl_Position);

    vec2 dir = normalize(p2 - p1);
    vec2 normal = vec2(-dir.y, dir.x);

    vec4 pos1, pos2, pos3, pos4;

    if(IsCardinalDirection(dir))
    {
        float thickness = vThickness[0]; // Nhân 2 vì pixel thickness
        vec2 offset = normal * (thickness * 0.5);

        // Nếu là đường chéo, snap về pixel
        pos1 = OffsetInClipSnap(gl_in[1].gl_Position,  offset, u_Viewport);
        pos2 = OffsetInClipSnap(gl_in[1].gl_Position, -offset, u_Viewport);
        pos3 = OffsetInClipSnap(gl_in[2].gl_Position,  offset, u_Viewport);
        pos4 = OffsetInClipSnap(gl_in[2].gl_Position, -offset, u_Viewport);
    }
    else
    {
        float thickness = vThickness[0] - 0.25;
        vec2 offset = normal * (thickness * 0.5);

        // Nếu là đường thẳng ngang hoặc dọc, không cần snap
        pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position,  offset, u_Viewport);
        pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, -offset, u_Viewport);
        pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position,  offset, u_Viewport);
        pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, -offset, u_Viewport);
    }

    fColor = vColor[0];
    gl_Position = pos1;
    EmitVertex();

    fColor = vColor[0];
    gl_Position = pos2;
    EmitVertex();

    fColor = vColor[0];
    gl_Position = pos3;
    EmitVertex();

    fColor = vColor[0];
    gl_Position = pos4;
    EmitVertex();

    EndPrimitive();
}