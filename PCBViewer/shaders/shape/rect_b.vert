#version 330 core

layout(location = 0) in vec3  a_v3WorldPos;
layout(location = 1) in vec4  a_v4Color;
layout(location = 2) in float a_fPixThickness;
layout(location = 3) in int   a_nRectID;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

out vec4  v_v4Color;
out float v_fThickness;
out float v_fWorldThickness;
out int   v_nRectID;
out vec2  v_v2WorldPos;

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
    gl_Position = u_Proj * u_View * u_Model * vec4(a_v3WorldPos, 1.0);

    vec3 v3Thickness = PixelToWorldOffset(a_v3WorldPos, a_fPixThickness, u_Viewport, 1.0);
    float fWorldThickness = length(v3Thickness);

    v_v4Color = a_v4Color;
    v_fThickness = a_fPixThickness;
    v_fWorldThickness = fWorldThickness;
    v_nRectID = a_nRectID;
    v_v2WorldPos = a_v3WorldPos.xy;
}