#version 150 core

layout(lines) in;
layout(triangle_strip, max_vertices = 9) out;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom; // Tỷ lệ zoom, có thể dùng để điều chỉnh độ dày đường vẽ

in vec4 vColor[];     // from vertex shader
in float vThickness[]; // from vertex shader

out vec4 fColor;
out vec2 vLoc;
flat out vec2 vSLine;
flat out vec2 vELine;
flat out float fThickness;
flat out int fblur;

flat out int nStartJoin;
flat out int nEndJoin;
flat out int nIsAxis;

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
    vec2 screen = clip_2_screen(clipPos);

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

bool IsVectorHorizontal(vec2 v, float epsilon)
{
    return abs(v.y) < epsilon;
}

bool IsVectorVertical(vec2 v, float epsilon)
{
    return abs(v.x) < epsilon;
}

bool IsVectorAxisAligned(vec2 v, float epsilon)
{
    return abs(v.x) < epsilon || abs(v.y) < epsilon;
}

void main()
{
    vec2 p0 = clip_2_ndc(gl_in[0].gl_Position);
    vec2 p1 = clip_2_ndc(gl_in[1].gl_Position);

    vec2 v = normalize(p1 - p0);

    // Calculate the normal vectors for the segments
    vec2 n = vec2(-v.y, v.x);

    float thickness = vThickness[0] * 0.5 + 1.f;
    float half_thickness = vThickness[0] * 0.5;

    vec4 pos1, pos2, pos3, pos4;

    vec2 offsetP1 = n * thickness;
    vec2 offsetP2 = n * thickness;

    nStartJoin = 1;
    pos1 = OffsetInClipNoSnap(gl_in[0].gl_Position, - v * half_thickness + offsetP1, u_Viewport);
    pos2 = OffsetInClipNoSnap(gl_in[0].gl_Position, - v * half_thickness - offsetP1, u_Viewport);

    nEndJoin = 1;
    pos3 = OffsetInClipNoSnap(gl_in[1].gl_Position, v * half_thickness + offsetP2, u_Viewport);
    pos4 = OffsetInClipNoSnap(gl_in[1].gl_Position, v * half_thickness - offsetP2, u_Viewport);

    nIsAxis = IsVectorAxisAligned(v, 1e-5) ? 1 : 0;

    vec2 pPix1 = clip_2_screen(pos1);
    vec2 pPix2 = clip_2_screen(pos2);
    vec2 pPix3 = clip_2_screen(pos3);
    vec2 pPix4 = clip_2_screen(pos4);

    p0 = clip_2_screen(gl_in[0].gl_Position);
    p1 = clip_2_screen(gl_in[1].gl_Position);

    // blur
    if(nIsAxis == 0)
    {
        vSLine = p0;
        vELine = p1;
        fblur = 1;

        fColor = vColor[0];
        vLoc = pPix1;
        fThickness = vThickness[0];
        gl_Position = pos1;
        EmitVertex();

        fColor = vColor[0];
        vLoc = pPix2;
        fThickness = vThickness[0];
        gl_Position = pos2;
        EmitVertex();

        fColor = vColor[0];
        vLoc = pPix3;
        fThickness = vThickness[0];
        gl_Position = pos3;
        EmitVertex();

        fColor = vColor[0];
        vLoc = pPix4;
        fThickness = vThickness[0];
        gl_Position = pos4;
        EmitVertex();

        EndPrimitive();
    }
 
    // stroke
    {
        vSLine = p0;
        vELine = p1;
        fblur = 0;

        fColor = vColor[0];
        vLoc = pPix1;
        fThickness = vThickness[0];
        gl_Position = pos1;
        EmitVertex();

        fColor = vColor[0];
        vLoc = pPix2;
        fThickness = vThickness[0];
        gl_Position = pos2;
        EmitVertex();

        fColor = vColor[0];
        vLoc = pPix3;
        fThickness = vThickness[0];
        gl_Position = pos3;
        EmitVertex();

        fColor = vColor[0];
        vLoc = pPix4;
        fThickness = vThickness[0];
        gl_Position = pos4;
        EmitVertex();

        EndPrimitive();
    }
}