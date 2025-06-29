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
in vec3 vWorldPos[];

out vec4 fColor;
out vec2 vLoc;
flat out vec2 vSLine;
flat out vec2 vELine;
flat out float fThickness;

float MiterLimit = 0.75; // Giới hạn miter, có thể điều chỉnh

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

float distance_to_line_2(vec2 P, vec2 A, vec2 dir)
{
    vec2 AP = P - A;
    float cross = dir.x * AP.y - dir.y * AP.x;
    float dist = abs(cross) / length(dir);
    return dist;
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

float angleBetween(vec2 a, vec2 b) {
    float dotProd = dot(a, b);
    float lenA = length(a);
    float lenB = length(b);
    float cosTheta = clamp(dotProd / (lenA * lenB), -1.0, 1.0);
    return acos(cosTheta); // kết quả là radian
}

bool isPerpendicular(vec2 a, vec2 b) {
    return abs(dot(a, b)) < 1e-4; // Gần 0 là coi như vuông
}

void main()
{
    vec2 p0 = ClipToScreen(gl_in[0].gl_Position);
    vec2 p1 = ClipToScreen(gl_in[1].gl_Position);
    vec2 p2 = ClipToScreen(gl_in[2].gl_Position);
    vec2 p3 = ClipToScreen(gl_in[3].gl_Position);

    if(vPolygonID[0] != vPolygonID[1] || vPolygonID[0] != vPolygonID[2])
    {
        // Nếu polygon ID không khớp, không vẽ gì cả
        return;
    }

    vec2 pw0 = vWorldPos[0].xy;
    vec2 pw1 = vWorldPos[1].xy;
    vec2 pw2 = vWorldPos[2].xy;
    vec2 pw3 = vWorldPos[3].xy;

    // Calculate the direction vectors for the segments
    // p0 to p1, p1 to p2, and p2 to p3
    vec2 v0 = normalize(pw1 - pw0);
    vec2 v1 = normalize(pw2 - pw1);
    vec2 v2 = normalize(pw3 - pw2);

    // Calculate the normal vectors for the segments
    vec2 n0 = vec2(-v0.y, v0.x);
    vec2 n1 = vec2(-v1.y, v1.x);
    vec2 n2 = vec2(-v2.y, v2.x);

    vec2 ori_miter_p1;
    vec2 ori_miter_p2;

    // Calculate the miter vector
    vec2 miter_p1 = normalize(n0 + n1);
    vec2 miter_p2 = normalize(n1 + n2);

    ori_miter_p1 = miter_p1;
    ori_miter_p2 = miter_p2;

    // Dot của 2 vector đơn vị chính là cos(a) của góc miter_p1, n1 với n1 là cạnh huyền
    float an1 = 0.f;
    float bn1 = 0.f;

    an1 = dot(miter_p1, n1);
    bn1 = dot(miter_p2, n2);

    // float thickness = vThickness[0] * 0.5 * (u_zZoom); // Nhân 2 vì pixel thickness
    float thickness = vThickness[0] * 0.5 + 1.f; // Nhân 2 vì pixel thickness

    // if((abs(angleBetween(miter_p1, n1)) - 90) < 0.01)
    // {
    //     an1 = 1.f;
    // }

    // if((abs(angleBetween(miter_p2, n2)) - 90) < 0.01)
    // {
    //     bn1 = 1.f;
    // }


    // an1 = max(an1, 0.05 / u_zZoom);
    // bn1 = max(bn1, 0.05 / u_zZoom);

    bool bSnap = IsCardinalDirection(v1);



    // Kiểm tra xem v1 có phải là hướng chính (cardinal direction) không
    // Nếu là hướng chính thì sẽ snap về pixel, nếu không thì không snap

    // if(abs(an1) < 0.2)
    //     an1 = 1;

    // if(abs(bn1) < 0.2)
    //     bn1 = 1;

    // Tính toán độ dài của đoạn thẳng theo miter
    float length_a = thickness / an1;
    float length_b = thickness / bn1;

    // if(length_a > 5.f)
    //     length_a = 5.f;

    // if(length_b > 5.f)
    //     length_b = 5.f;

    vec4 pos1, pos2, pos3, pos4;

    vec2 offsetP1_ori = miter_p1 * length_a;
    vec2 offsetP2_ori = miter_p2 * length_b;


    // bool b = false;
    // vec4 mut;

    bool bCut1 = false;
    bool bCut2 = false;

    bool bP1OutSize = false;
    bool bP2OutSize = false;

    miter_p1 = n1;
    length_a = thickness;

    miter_p2 = n1;
    length_b = thickness;

    // if(dot( v0, v1 ) < -MiterLimit)
    // {
    //     // xác định cắt vát

    //     miter_p1 = n1;
    //     length_a = thickness;

    //     bCut1 = true;

    //     float z1 = v0.x * v1.y - v0.y * v1.x;
    //     bP1OutSize = z1 > 0.0;

    //     // vec2 temp1 = p1 + n1* thickness;

    //     // float kc = distance_to_line_2(temp1, p1, ori_miter_p1);

    //     // vec2 nt = vec2(-ori_miter_p1.y, ori_miter_p1.x);

    //     // vec2 pre = temp1 + nt * kc;

    //     // vec2 tmp = ScreenToClip(pre, gl_in[0].gl_Position.w);

    //     // mut = vec4(tmp, gl_in[0].gl_Position.zw);

    //     // b = true;
    // }



    // if( dot( v1, v2 ) < -MiterLimit )
    // {
    //     miter_p2 = n1;
    //     length_b = thickness;

    //     bCut2 = true;

    //     float z2 = v1.x * v2.y - v1.y * v2.x; // cross -> quay phải
    //     bP2OutSize = z2 > 0.0;

    //     // pos1 = OffsetClipByPixel(gl_in[1].gl_Position, -1.f, ori_miter_p1, u_Viewport);

    //     // fColor = vColor[0];
    //     // vLoc = ClipToScreen(pos1);
    //     // vSLine = p1;
    //     // vELine = p2;
    //     // fThickness = vThickness[0];
    //     // gl_Position = pos1;
    //     // EmitVertex();
    // }

    // float maxLength = length(p2 - p1);
    // if(abs(length_a) > maxLength / 2)
    // {
    //     length_a = maxLength / 2;
    //     // bCut1 = false;
    // }

    // if(abs(length_b) > maxLength / 2)
    // {
    //     length_b = maxLength / 2;

    //     // bCut2 = false;
    // }

    vec2 offsetP1 = miter_p1 * length_a;
    vec2 offsetP2 = miter_p2 * length_b;

    // pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position, isP1OutSize ?  offsetP1 : offsetP1_ori, u_Viewport);
    // pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, isP1OutSize ?  -offsetP1 : -offsetP1_ori, u_Viewport);

    if(bCut1)
    {
        pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position, bP1OutSize ? offsetP1_ori : offsetP1, u_Viewport);
        pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, bP1OutSize ? -offsetP1 : -offsetP1_ori, u_Viewport);
    }
    else 
    {

        pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position,  offsetP1, u_Viewport);
        pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, -offsetP1, u_Viewport);

        // pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position,  - v1 * 8.f + offsetP1, u_Viewport);
        // pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position,  - v1 * 8.f - offsetP1, u_Viewport);
    }


    if(bCut2)
    {
        pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position, bP2OutSize ? offsetP2_ori : offsetP2, u_Viewport);
        pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, bP2OutSize ? -offsetP2 : -offsetP2_ori , u_Viewport);
    }
    else 
    {
        pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position,  offsetP2, u_Viewport);
        pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, -offsetP2, u_Viewport);

        // pos3 = OffsetInClipNoSnap(gl_in[2].gl_Position, v1 * thickness + offsetP2, u_Viewport);
        // pos4 = OffsetInClipNoSnap(gl_in[2].gl_Position, v1 * thickness - offsetP2, u_Viewport);
    }

    fColor = vColor[0];
    vLoc = ClipToScreen(pos1);
    vSLine = p1;
    vELine = p2;
    fThickness = vThickness[0];
    gl_Position = pos1;
    EmitVertex();

    vec4 pmid; 

    if(bCut1)
    {
        vec2 temp1 = p1 + n1* thickness;

        float kc = distance_to_line_2(temp1, p1, ori_miter_p1);

        vec2 mid;
        
        if(bP1OutSize)
        {
            mid = p1 - ori_miter_p1 * (kc);
        }
        else 
        {
            mid = p1 + ori_miter_p1 * (kc);
        }


        pmid = vec4(ScreenToClip(mid, gl_in[1].gl_Position.w), gl_in[1].gl_Position.zw);

        // vec2 snappedClipXY = ScreenToClip(offsetScreen, clipPos.w);

        // return vec4(snappedClipXY, clipPos.zw);

        // vec4 pmid = OffsetInClipNoSnap(gl_in[1].gl_Position, -ori_miter_p1 * 3.f, u_Viewport);

        fColor = vColor[0];
        vLoc = ClipToScreen(pmid);
        vSLine = p1;
        vELine = p2;
        fThickness = vThickness[0];
        gl_Position = pmid;
        EmitVertex();

        fColor = vColor[0];
        vLoc = ClipToScreen(pos3);
        vSLine = p1;
        vELine = p2;
        fThickness = vThickness[0];
        gl_Position = pos3;
        EmitVertex();

        fColor = vColor[0];
        vLoc =  ClipToScreen(pos2);
        vSLine = p1;
        vELine = p2;
        fThickness = vThickness[0];
        gl_Position = pos2;
        EmitVertex();

    }
    else 
    {
        if(bCut2)
        {
            fColor = vColor[0];
            vLoc = ClipToScreen(pos3);
            vSLine = p1;
            vELine = p2;
            fThickness = vThickness[0];
            gl_Position = pos3;
            EmitVertex();

            fColor = vColor[0];
            vLoc =  ClipToScreen(pos2);
            vSLine = p1;
            vELine = p2;
            fThickness = vThickness[0];
            gl_Position = pos2;
            EmitVertex();
        }
        else 
        {
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
        }
    }


    if(bCut2)
    {
        vec2 temp1 = p2 + n1* thickness;

        float kc = distance_to_line_2(temp1, p2, ori_miter_p2);

        vec2 mid;
        
        if(bP2OutSize)
        {
            mid = p2 - ori_miter_p2 * (kc);
        }
        else 
        {
            mid = p2 + ori_miter_p2 * (kc);
        }

        pmid = vec4(ScreenToClip(mid, gl_in[2].gl_Position.w), gl_in[2].gl_Position.zw);

        // vec2 snappedClipXY = ScreenToClip(offsetScreen, clipPos.w);

        // return vec4(snappedClipXY, clipPos.zw);

        // vec4 pmid = OffsetInClipNoSnap(gl_in[1].gl_Position, -ori_miter_p1 * 3.f, u_Viewport);

        fColor = vColor[0];
        vLoc = ClipToScreen(pmid);
        vSLine = p1;
        vELine = p2;
        fThickness = vThickness[0];
        gl_Position = pmid;
        EmitVertex();
    }


    fColor = vColor[0];
    vLoc = ClipToScreen(pos4);
    vSLine = p1;
    vELine = p2;
    fThickness = vThickness[0];
    gl_Position = pos4;
    EmitVertex();


    EndPrimitive();

        /* close the gap */
        // if( dot( v0, n1 ) > 0 )
        // {
        //     pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position,  _thickness * n0 , u_Viewport);
        //     fColor = vColor[0];
        //     vLoc = ClipToScreen(pos1);
        //     vSLine = p1;
        //     vELine = p2;
        //     fThickness = vThickness[0];
        //     gl_Position = pos1;

        //     EmitVertex();

 
        //     pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position,  _thickness * n1 , u_Viewport);
        //     fColor = vColor[0];
        //     vLoc = ClipToScreen(pos2);
        //     vSLine = p1;
        //     vELine = p2;
        //     fThickness = vThickness[0];
        //     gl_Position = pos2;

        //     EmitVertex();

        //     pos3 = gl_in[1].gl_Position;
        //     fColor = vColor[0];
        //     vLoc = ClipToScreen(pos3);
        //     vSLine = p1;
        //     vELine = p2;
        //     fThickness = vThickness[0];
        //     gl_Position = pos3;

        //     EmitVertex();

        //     EndPrimitive();
        // }
        // else 
        // {

        //     pos1 = OffsetInClipNoSnap(gl_in[1].gl_Position, -_thickness * n1 , u_Viewport);

        //     fColor = vColor[0];
        //     vLoc = ClipToScreen(pos1);
        //     vSLine = p1;
        //     vELine = p2;
        //     fThickness = vThickness[0];
        //     gl_Position = pos1;

        //     EmitVertex();

        //     pos2 = OffsetInClipNoSnap(gl_in[1].gl_Position, -_thickness * n0 , u_Viewport);
        //     fColor = vColor[0];
        //     vLoc = ClipToScreen(pos2);
        //     vSLine = p1;
        //     vELine = p2;
        //     fThickness = vThickness[0];
        //     gl_Position = pos2;
        //     EmitVertex();


        //     pos3 = gl_in[1].gl_Position;
        //     fColor = vColor[0];
        //     vLoc = ClipToScreen(pos3);
        //     vSLine = p1;
        //     vELine = p2;
        //     fThickness = vThickness[0];
        //     gl_Position = pos3;
        //     EmitVertex();

        //     EndPrimitive();
        // }
}