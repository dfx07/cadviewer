#version 330 core

layout(location = 0) in vec2 aLocalUV;
layout(location = 1) in vec3 aPosCenter;
layout(location = 2) in vec2 aSize;
layout(location = 3) in float aThickness;
layout(location = 4) in vec4 aThinknessColor;
layout(location = 5) in vec4 aFillColor;

out vec2  vLocalUV;
out vec2  vSize;
out float vThickness;
out vec4  vThinkessColor;
out vec4  vFillColor;

flat out vec4 vPosCenter;
flat out vec4 vRealSize;
flat out vec4 vPosBorder;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

void main()
{
    vec2 sizeRel;
    vec3 worldPos = aPosCenter + vec3((aSize) * aLocalUV, aPosCenter.z);

    gl_Position = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    vPosCenter = u_Proj * u_View * u_Model * vec4(aPosCenter, 1.0);

    vRealSize = u_Proj * u_View * u_Model * vec4(aSize.x, aSize.y, 0.0, 1.0);

    vec3 worldPosBorder = aPosCenter + vec3((aSize)/2.0, aPosCenter.z);
    vPosBorder = u_Proj * u_View * u_Model * vec4(worldPosBorder, 1.0);

    vLocalUV = aLocalUV;
    vSize = aSize;
    vThickness = aThickness;
    vThinkessColor = aThinknessColor;
    vFillColor = aFillColor;
}