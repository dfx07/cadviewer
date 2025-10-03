#version 330 core

layout(location = 0) in vec2 a_v2LocalUV;
layout(location = 1) in vec3 a_v3PS;
layout(location = 2) in vec3 a_v3PE;
layout(location = 3) in vec4 a_v4ColS;
layout(location = 4) in vec4 a_v4ColE;
layout(location = 5) in float a_fThickness;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

out vec4  v_v4Color;
out vec3  v_v3WorldPos;

flat out float vf_fThickness;
flat out float vf_fThicknessPx;

flat out vec3 vf_v3PS;
flat out vec3 vf_v3PE;

flat out vec2 vf_v2PSPx;
flat out vec2 vf_v2PEPx;

flat out int  vf_nAA; // 0 (not use AA) | 1 (use AA)

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

int GetVertexIdx(vec2 vlocal)
{
    if(vlocal == vec2(-1.0,  1.0))
        return 0;
    if(vlocal == vec2(1.0,  1.0))
        return 1;
    if(vlocal == vec2(-1.0, -1.0))
        return 2;
    if(vlocal == vec2(1.0, -1.0))
        return 3;

    return 0;
}

vec3 GetNormalXY_CW(vec3 p1, vec3 p2)
{
    vec2 dir = p2.xy - p1.xy;          // vector hướng 2D
    vec2 n   = normalize(vec2(dir.y, -dir.x)); // xoay -90 độ
    return vec3(n, 0.0);
}

vec3 GetNormalXY_CCW(vec3 p1, vec3 p2)
{
    vec2 dir = p2.xy - p1.xy;
    vec2 n   = normalize(vec2(-dir.y, dir.x)); // xoay +90 độ
    return vec3(n, 0.0);
}

void main()
{
    //******************************
	//*  -1.0f, -1.0f, // end-left
	//*   1.0f, -1.0f, // end-right
	//*  -1.0f,  1.0f, // sta-left
	//*   1.0f,  1.0f  // sta-right
    //******************************/

    int nIdx = 0;  // 0(s-l) | 1(s-r) | 2(e-l) | 3(e-r)
    nIdx = GetVertexIdx(a_v2LocalUV);

    vec3 v3NLine, v3WorldPos;
    
    if(nIdx == 0 || nIdx == 2)
        v3NLine = GetNormalXY_CW(a_v3PS, a_v3PE);
    else 
        v3NLine = GetNormalXY_CCW(a_v3PS, a_v3PE);

    float fZ = a_v3PE.z;
    vec3 v3Thickness = px_to_world(a_v3PS, a_fThickness, u_Viewport);
    float fThickness = length(v3Thickness.xy);

    vec3 v3Offset = v3NLine * fThickness * 0.8;
    v3Offset.z = fZ;

    if(nIdx == 0 || nIdx == 1)
    {
        
        v3WorldPos = a_v3PS + v3Offset;
    }
    else 
    {
        
        v3WorldPos = a_v3PE + v3Offset;
    }
    
    // if(gl_InstanceID == 2)
    // {
    //     if(nIdx == 1)
    //     {
    //         v_v4Color = vec4(1.0, 0.0, 0.0, 1.0);
    //     }
    //     else 
    //     {
    //         v_v4Color = vec4(0.0, 0.0, 1.0, 1.0);
    //     }
    // }
    // else 
    // {
    //     v_v4Color = vec4(1.0, 0.0, 0.0, 1.0);
    // }

    v_v3WorldPos = v3WorldPos;

    gl_Position = u_Proj * u_View * u_Model * vec4(v3WorldPos, 1.0);

    // Set the ouput data for fragment shader
    

    vf_fThickness = fThickness;
    vf_fThicknessPx = a_fThickness;

    vf_v3PS = a_v3PS;
    vf_v3PE = a_v3PE;

    vf_v2PSPx = world_to_px(a_v3PS, u_Viewport);
    vf_v2PEPx = world_to_px(a_v3PE, u_Viewport);

    // Verify whether it is the asix
    if(abs(vf_v2PSPx.x - vf_v2PEPx.x) <= 0.001 || abs(vf_v2PSPx.y - vf_v2PEPx.y) <= 0.001)
    {
        vf_nAA = 0;
    }
    else
    {
        vf_nAA = 1;
    }
}