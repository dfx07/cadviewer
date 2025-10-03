#version 330 core

in vec4  v_v4Color;
in vec3  v_v3WorldPos;


uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

flat in float vf_fThickness;
flat in float vf_fThicknessPx;

flat in vec3  vf_v3PS;
flat in vec3  vf_v3PE;

flat in vec2 vf_v2PSPx;
flat in vec2 vf_v2PEPx;

flat in int  vf_nAA; // 0 (not use AA) | 1 (use AA)

out vec4 FragColor;

float sdSegment( in vec2 p, in vec2 a, in vec2 b )
{
    vec2 pa = p-a, ba = b-a;
    float h = clamp( dot(pa,ba)/dot(ba,ba), 0.0, 1.0 );
    return length( pa - ba*h );
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
    if(vf_nAA == 1)
    {
        float fHalfThicknessPx = (vf_fThicknessPx) * 0.45;
        vec2 v2PosPx = gl_FragCoord.xy;
        float fDist = sdSegment(v2PosPx, vf_v2PSPx, vf_v2PEPx);

        float aa = fwidth(fDist) * 0.8;

        float fAlpha = 1 - smoothstep(fHalfThicknessPx - aa, fHalfThicknessPx + aa, abs(fDist));

        FragColor = vec4(v_v4Color.rgb, v_v4Color.a * fAlpha);
    }
    else
    {
        float fHalfThicknessPx = (vf_fThicknessPx) * 0.5;
        vec2 v2PosPx = gl_FragCoord.xy;

        float fDist = sdSegment(v2PosPx, vf_v2PSPx, vf_v2PEPx);

        if(abs(fDist) <= fHalfThicknessPx)
        {
            FragColor = v_v4Color;
        }
    }
}