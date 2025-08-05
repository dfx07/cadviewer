#version 330 core

// model vertex
layout(location = 0) in vec2 aLocalUV;

// world vertex
layout(location = 1) in vec3 aPosCenter;
layout(location = 2) in float aAngleRad;
layout(location = 3) in vec2 aSize;
layout(location = 4) in vec4 aFillColor;

out vec2  vLocalUV;
out vec2  vSize;
out float vThickness;
out vec4  vThinkessColor;
out vec4  vFillColor;

flat out vec4 vPosCenter;
flat out vec4 vRealSize;
flat out vec4 vPosBorder;
flat out vec2 vNegRotAngle;
flat out int nLevelAA;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
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

float PixelToWorld(float pixel, float zoom)
{
    return pixel * zoom;
}

vec3 PixelToWorldOffset(vec3 worldPos, float pixel, vec2 viewSize, float zoom)
{
    // Step 1: Chuyển từ world → clip space
    vec4 clipPos = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    // Step 2: Chuyển từ clip → NDC (Normalized Device Coordinates)
    vec3 ndc = clipPos.xyz / clipPos.w;

    // Step 3: Tính độ lệch NDC tương ứng 1 pixel (theo chiều X)
    vec2 offsetNDC = vec2(pixel) * 2.0 / viewSize;

    // Step 4: Tạo điểm mới lệch 1 pixel trong NDC → đưa về clip space
    vec4 clipOffset = vec4(ndc.xy + offsetNDC, ndc.z, 1.0) * clipPos.w;

    // Step 5: Chuyển clip space → world space bằng cách nghịch đảo MVP
    mat4 invMVP = inverse(u_Proj * u_View * u_Model);
    vec4 offsetWorldPos = invMVP * clipOffset;
    offsetWorldPos /= offsetWorldPos.w;

    // Step 6: Tính hiệu giữa điểm mới và điểm cũ → ra vector lệch trong world space
    vec3 delta = offsetWorldPos.xyz - worldPos;

    // Step 7: Áp dụng zoom (nếu bạn zoom riêng bên ngoài shader)
    return delta / zoom;
}

void main()
{
    vec3 margin3 = PixelToWorldOffset(aPosCenter, 2.0, u_Viewport, 1.0);
    float margin = length(margin3);

    vec2 newLocalUV = RotateAround((aSize + vec2(margin)) * aLocalUV, vec2(0, 0), aAngleRad);

    vec3 worldPos = aPosCenter + vec3(newLocalUV, aPosCenter.z);

    gl_Position = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    vPosCenter = u_Proj * u_View * u_Model * vec4(aPosCenter, 1.0);

    vRealSize = u_Proj * u_View * u_Model * vec4(aSize.x, aSize.y, 0.0, 1.0);

    // vec2 newLocalUV2 = RotateAround((aSize) * vec2(0.5, 0.5), vec2(0, 0), aAngleRad);
    // vec3 worldPosBorder = aPosCenter + vec3(newLocalUV2 * 0.5, aPosCenter.z);

    vec3 worldPosBorder = aPosCenter + vec3(aSize * 0.5, aPosCenter.z);

    vPosBorder = u_Proj * u_View * u_Model * vec4(worldPosBorder, 1.0);

    vLocalUV     = aLocalUV;
    vSize        = aSize;
    vFillColor   = aFillColor;
    vNegRotAngle = vec2(cos(-aAngleRad), sin(-aAngleRad));

    nLevelAA     = 1;
}