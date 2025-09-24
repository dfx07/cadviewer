#version 330 core

// model vertex
layout(location = 0) in vec2  a_v2LocalUV;

// world vertex
layout(location = 1) in vec3  a_v3WorldCenterPos;
layout(location = 2) in float a_fAngleRad;
layout(location = 3) in vec2  a_v2Size;
layout(location = 4) in float a_fThickness;
layout(location = 5) in vec4  a_v4ThicknessColor;
layout(location = 6) in vec4  a_v4FillColor;

out vec4  v_v4ThicknessColor;
out vec3  v_v3Pos;

flat out vec2   vf_v2CenterPosPx;
flat out vec3   vf_v3CenterPos;
flat out vec2   vf_v2SizePx;
flat out vec2   vf_v2Size;
flat out vec2   vf_v2NegRotAngle;
flat out float  vf_fThickness;
flat out float  vf_fThicknessPx;
flat out int    vf_nAA;

uniform mat4  u_Model;
uniform mat4  u_View;
uniform mat4  u_Proj;
uniform vec2  u_Viewport;
uniform float u_zZoom;

vec2 rotate_around(vec2 pt, vec2 pt_center, float angle_r)
{
    // Đưa điểm về hệ tọa độ với tâm là gốc
    vec2 p = pt - pt_center;

    // Tạo ma trận xoay
    float c = cos(angle_r);
    float s = sin(angle_r);
    mat2 rot = mat2(c, -s, s, c);

    // Xoay rồi đưa về lại vị trí cũ
    return rot * p + pt_center;
}

vec3 px_to_world(vec3 worldPos, float pixel, vec2 viewSize) // Bỏ tham số zoom
{
    // Sử dụng ma trận MVP hiện tại, đã bao gồm cả zoom
    mat4 mvp = u_Proj * u_View * u_Model;

    // Step 1: Chuyển từ world → clip space
    vec4 clipPos = mvp * vec4(worldPos, 1.0);

    // Step 2: Chuyển từ clip → NDC (Normalized Device Coordinates)
    vec3 ndc = clipPos.xyz / clipPos.w;

    // Step 3: Tính độ lệch NDC tương ứng với số pixel mong muốn (theo chiều X và Y)
    // NDC có chiều rộng và cao là 2.0 (từ -1 đến 1)
    vec2 offsetNDC = vec2(pixel, pixel) * 2.0 / viewSize;

    // Step 4: Tạo điểm mới trong NDC đã được dịch chuyển, rồi chuyển về clip space
    // Nhân lại với clipPos.w để đảo ngược phép chia phối cảnh
    vec4 clipOffset = vec4(ndc.xy + offsetNDC, ndc.z, 1.0) * clipPos.w;

    // Step 5: Chuyển điểm mới từ clip space → world space
    mat4 invMVP = inverse(mvp);
    vec4 offsetWorldPos = invMVP * clipOffset;
    offsetWorldPos /= offsetWorldPos.w; // Đảo ngược phép chia phối cảnh một lần nữa

    // Step 6: Tính toán vector chênh lệch trong world space
    vec3 delta = offsetWorldPos.xyz - worldPos;

    // KHÔNG cần chia cho zoom. Delta đã là kết quả đúng.
    return delta;
}

vec2 world_to_px(vec3 worldPos, vec2 viewSize)
{
    // Step 1: World → Clip Space
    vec4 clipPos = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    // Step 1: Clip Space → NDC
    vec3 ndc = clipPos.xyz / clipPos.w; // [-1, 1]

    // Step 1: NDC → Pixel Space
    vec2 pixelPos = (ndc.xy * 0.5 + 0.5) * viewSize;

    return pixelPos;
}

void main()
{
    vec3 v3Thickness = px_to_world(a_v3WorldCenterPos, a_fThickness, u_Viewport);
    float fThickness = length(v3Thickness);

    vec2 v2NewLocalUV = rotate_around((a_v2Size + vec2(fThickness + 5.0)) * 0.5f * a_v2LocalUV, vec2(0, 0), a_fAngleRad);
    v_v3Pos = a_v3WorldCenterPos + vec3(v2NewLocalUV, 0.0) ;
    vf_v3CenterPos = a_v3WorldCenterPos;

    gl_Position = u_Proj * u_View * u_Model * vec4(a_v3WorldCenterPos + vec3(v2NewLocalUV, 0.0) , 1.0);

    vf_v2CenterPosPx = world_to_px(a_v3WorldCenterPos, u_Viewport);
    vec2 v2EdgePosPx = world_to_px(a_v3WorldCenterPos + vec3(a_v2Size, 0.0), u_Viewport);

    vf_v2SizePx = abs(v2EdgePosPx - vf_v2CenterPosPx);
    vf_v2Size = a_v2Size;

    vf_fThickness = fThickness;
    vf_fThicknessPx = a_fThickness;

    vf_v2NegRotAngle = vec2(cos(-a_fAngleRad), sin(-a_fAngleRad));
    v_v4ThicknessColor = a_v4ThicknessColor;

    vec3 v3DirLocal = vec3(cos(a_fAngleRad), sin(a_fAngleRad), 0.0);

    vec2 v2Px = world_to_px(a_v3WorldCenterPos + v3DirLocal, u_Viewport);

    if(abs(v2Px.x - vf_v2CenterPosPx.x) <= 0.001 || abs(v2Px.y - vf_v2CenterPosPx.y) <= 0.001)
    {
        vf_nAA = 0;
    }
    else
    {
        vf_nAA = 1;
    }
}