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

// OffsetInClip: Tính toán vị trí mới trong clip space dựa trên offset pixel
// Nhân 2.0 vì Chuyển từ pixel → tỷ lệ 0..1  nhưng trong NDC là [-1, 1]
vec4 OffsetInClip(vec4 clipPos, vec2 offsetPixel, vec2 viewport)
{
    // vec2 ndcOffset = (offsetPixel / viewport) * 2.0;
    // return clipPos + vec4(ndcOffset * clipPos.w, 0.0, 0.0);

    // // offse pixel → NDC
    // vec2 ndcOffset = (offsetPixel / viewport) * 2.0;

    // // snap đến tâm pixel gần nhất
    // vec2 screenNDC = (clipPos.xy / clipPos.w) * 0.5 + 0.5;
    // vec2 snappedScreenNDC = floor(screenNDC * viewport) + 0.5; // snap to center
    // vec2 snappedNDC = (snappedScreenNDC / viewport) * 2.0 - 1.0;

    // vec2 snappedOffset = snappedNDC - (clipPos.xy / clipPos.w);
    // return clipPos + vec4(snappedOffset * clipPos.w, 0.0, 0.0);

    // Chuyển clip → screen
    vec2 ndc = clipPos.xy / clipPos.w;
    vec2 screen = (ndc * 0.5 + 0.5) * viewport;

    // Thêm offset theo pixel
    vec2 offsetScreen = screen + offsetPixel;

    // Snap về tâm pixel
    vec2 snappedScreen = floor(offsetScreen) - 1.0; // Snap to center pixel

    // Quay lại clip space
    vec2 snappedNDC = (snappedScreen / viewport) * 2.0 - 1.0;
    vec2 snappedClipXY = snappedNDC * clipPos.w;

    return vec4(snappedClipXY, clipPos.zw);
}

void main() {
    vec2 p0 = ClipToScreen(gl_in[0].gl_Position);
    vec2 p1 = ClipToScreen(gl_in[1].gl_Position);
    vec2 p2 = ClipToScreen(gl_in[2].gl_Position);
    vec2 p3 = ClipToScreen(gl_in[3].gl_Position);

    vec2 dir = normalize(p2 - p1);
    vec2 normal = vec2(-dir.y, dir.x);

    float thickness = (vThickness[0] + vThickness[0]) * 0.5;
    vec2 offset = normal * (thickness * 0.5);

    vec4 pos1 = OffsetInClip(gl_in[1].gl_Position, offset, u_Viewport);
    vec4 pos2 = OffsetInClip(gl_in[1].gl_Position, -offset, u_Viewport);
    vec4 pos3 = OffsetInClip(gl_in[2].gl_Position, offset, u_Viewport);
    vec4 pos4 = OffsetInClip(gl_in[2].gl_Position, -offset, u_Viewport);

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