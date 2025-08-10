#version 330 core

layout(location = 0) in vec2  a_v2LocalUV;
layout(location = 1) in vec3  a_v3WorldCenterPos;
layout(location = 2) in float a_fRadius;
layout(location = 3) in vec4  a_v4Color;

out vec3  v_v3WorldPos;
out float v_fRadius;
out vec4  v_v4Color;

flat out vec3  vf_v3WorldCenterPos;


// flat out vec4 vCenterClipPos;
// flat out vec4 vClipCenter;
// flat out vec4 vClipEdge;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;
uniform float u_zZoom;

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
    vec3 v3WorldPos = a_v3WorldCenterPos + vec3(a_fRadius * a_v2LocalUV, a_v3WorldCenterPos.z);
    gl_Position = u_Proj * u_View * u_Model * vec4(v3WorldPos, 1.0);

    // vClipCenter = u_Proj * u_View * u_Model * vec4(aCenter, 1.0);

    // vec3 edgeWorld = aCenter + vec3(aRadius, 0.0, 0.0);
    // vClipEdge   = u_Proj * u_View * u_Model * vec4(edgeWorld, 1.0);

    vec3 v3WorldRadius = PixelToWorldOffset(a_v3WorldCenterPos, a_fRadius * u_zZoom, u_Viewport, 1.0);
    float fWorldRadius = length(v3WorldRadius);

    vf_v3WorldCenterPos = a_v3WorldCenterPos;
    v_v3WorldPos = v3WorldPos;
    v_fRadius = a_fRadius;
    v_v4Color = a_v4Color;
}