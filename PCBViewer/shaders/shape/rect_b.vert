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

void main()
{
    gl_Position = u_Proj * u_View * u_Model * vec4(a_v3WorldPos, 1.0);

    vec3 v3Thickness = PixelToWorld(a_v3WorldPos, a_fPixThickness, u_Viewport);
    float fWorldThickness = length(v3Thickness);

    v_v4Color = a_v4Color;
    v_fThickness = a_fPixThickness;
    v_fWorldThickness = fWorldThickness;
    v_nRectID = a_nRectID;
    v_v2WorldPos = a_v3WorldPos.xy;
}