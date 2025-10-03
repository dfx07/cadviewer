#version 330 core

// model vertex
layout(location = 0) in vec2  a_v2Dummy;

// world vertex
layout(location = 1) in vec3  a_v3Pos1;
layout(location = 2) in vec3  a_v3Pos2;
layout(location = 3) in vec3  a_v3Pos3;
layout(location = 4) in float a_fThickness;
layout(location = 5) in vec4  a_v4ThicknessColor;
layout(location = 6) in vec4  a_v4FillColor;

out vec4 v_v4Color;
flat out vec2 vf_v2Pos1Px;
flat out vec2 vf_v2Pos2Px;
flat out vec2 vf_v2Pos3Px;

uniform mat4  u_Model;
uniform mat4  u_View;
uniform mat4  u_Proj;
uniform vec2  u_Viewport;
uniform float u_zZoom;

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

int get_idx(vec2 v2idx)
{
    /*
        0.0f,  0.0f, // Pos1
        1.0f,  1.0f, // Pos2
        2.0f,  2.0f, // Pos3
    */
    if(v2idx.x == 0)
        return 1;
    else if(v2idx.x == 1)
        return 2;
    else 
        return 3;
}

void main()
{
    int idx = get_idx(a_v2Dummy);

    vec3 v3Pos = (idx == 1) ? a_v3Pos1 : ((idx == 2) ? a_v3Pos2 : a_v3Pos3);

    gl_Position = u_Proj * u_View * u_Model * vec4(v3Pos, 1.0);

    vf_v2Pos1Px = world_to_px(a_v3Pos1, u_Viewport);
    vf_v2Pos2Px = world_to_px(a_v3Pos2, u_Viewport);
    vf_v2Pos3Px = world_to_px(a_v3Pos3, u_Viewport);

    v_v4Color = a_v4FillColor;
}