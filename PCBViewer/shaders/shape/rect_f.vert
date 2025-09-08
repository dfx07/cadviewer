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

out vec4  v_v4FillColor;

flat out vec2  vf_v2CenterPosPx;
flat out vec2  vf_v2SizePx;
flat out vec2  vf_v2NegRotAngle;

// flat out vec4 vPosCenter;
// flat out vec4 vRealSize;
// flat out vec4 vPosBorder;
// flat out vec2 vRealPosCenter;
// flat out vec2 vNegRotAngle;
// flat out int nLevelAA;

uniform mat4  u_Model;
uniform mat4  u_View;
uniform mat4  u_Proj;
uniform vec2  u_Viewport;
uniform float u_zZoom;

vec2 RotateAround(vec2 point, vec2 center, float angleRad)
{
    // Đưa điểm về hệ tọa độ với tâm là gốc
    vec2 p = point - center;

    // Tạo ma trận xoay
    float c = cos(angleRad);
    float s = sin(angleRad);
    mat2 rot = mat2(c, -s, s, c);

    // Xoay rồi đưa về lại vị trí cũ
    return rot * p + center;
}

vec2 GetRotatedBoundSize(vec2 size, float angleRad)
{
    float w = size.x;
    float h = size.y;

    float c = cos(angleRad);
    float s = sin(angleRad);

    float boundWidth  = abs(w * c) + abs(h * s);
    float boundHeight = abs(w * s) + abs(h * c);

    return vec2(boundWidth, boundHeight);
}

vec3 PixelToWorld(vec3 worldPos, float pixel, vec2 viewSize) // Bỏ tham số zoom
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

vec2 WorldToPixel(vec3 worldPos, vec2 viewSize)
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
    // vec3 margin3 = PixelToWorld(a_v3WorldCenterPos, 2.0, u_Viewport);
    // float margin = length(margin3);

    vec2 v2NewLocalUV = RotateAround(a_v2Size * 0.5f * a_v2LocalUV, vec2(0, 0), a_fAngleRad);

    gl_Position = u_Proj * u_View * u_Model * vec4(a_v3WorldCenterPos + vec3(v2NewLocalUV, a_v3WorldCenterPos.z) , 1.0);

    vf_v2CenterPosPx = WorldToPixel(a_v3WorldCenterPos, u_Viewport);
    vec2 v2EdgePosPx = WorldToPixel(a_v3WorldCenterPos + vec3(a_v2Size, 0.0), u_Viewport);

    vf_v2SizePx = abs(v2EdgePosPx - vf_v2CenterPosPx);
    vf_v2NegRotAngle = vec2(cos(-a_fAngleRad), sin(-a_fAngleRad));


    v_v4FillColor = a_v4FillColor;

    // vec2 newLocalUV = RotateAround((aSize + vec2(margin)) * aLocalUV, vec2(0, 0), aAngleRad);

    // vec3 worldPos = aPosCenter + vec3(newLocalUV, aPosCenter.z);

    // gl_Position = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    // vPosCenter = u_Proj * u_View * u_Model * vec4(aPosCenter, 1.0);

    // vRealSize = u_Proj * u_View * u_Model * vec4(aSize.x, aSize.y, 0.0, 1.0);

    // vec3 worldPosBorder = aPosCenter + vec3(aSize * 0.5, aPosCenter.z);

    // vPosBorder = u_Proj * u_View * u_Model * vec4(worldPosBorder, 1.0);

    // vRealPosCenter = aPosCenter.xy;
    // vRealPos     = newLocalUV + aPosCenter.xy;

    // vLocalUV     = aLocalUV;
    // vSize        = aSize;
    // vFillColor   = aFillColor;
    // vNegRotAngle = vec2(cos(-aAngleRad), sin(-aAngleRad));

    // nLevelAA     = 1;
}