#version 330 core

layout(location = 0) in vec2  a_v2LocalUV;
layout(location = 1) in vec3  a_v3WorldCenterPos;
layout(location = 2) in float a_fRadius;
layout(location = 3) in float a_fThickness;
layout(location = 4) in vec4  a_v4Color;

out vec4 v_v4Color;

out float v_fThickness;

flat out vec2  vf_v2CenterPosPx;
flat out float vf_fRadius;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom;


vec3 PixelToWorld(vec3 worldPos, float pixel, vec2 viewSize, float zoom)
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
    vec3 v3WorldThickness = PixelToWorld(a_v3WorldCenterPos, a_fThickness, u_Viewport, 1.0);
    float fWorldThickness = length(v3WorldThickness);

    vec3 v3WorldPos = a_v3WorldCenterPos + vec3((a_fRadius + fWorldThickness) * a_v2LocalUV, a_v3WorldCenterPos.z);
    gl_Position = u_Proj * u_View * u_Model * vec4(v3WorldPos, 1.0);

    vec2 v2CenterPosPx = WorldToPixel(a_v3WorldCenterPos, u_Viewport);
    vec2 v2EdgePosPx = WorldToPixel(a_v3WorldCenterPos + vec3(vec2(a_fRadius, 0.0), a_v3WorldCenterPos.z), u_Viewport);

    vf_v2CenterPosPx = v2CenterPosPx;
    vf_fRadius = length(v2EdgePosPx - v2CenterPosPx);

    v_v4Color = a_v4Color;
    v_fThickness = a_fThickness;
}