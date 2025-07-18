#version 330 core

layout(location = 0) in vec3 aPosition;
layout(location = 1) in vec2 aSize;
layout(location = 2) in float aThickness;
layout(location = 3) in vec4 aThinknessColor;
layout(location = 4) in vec4 aFillColor;

out vec2 vSize;
out float vThickness;
out vec4 vThinkessColor;
out vec4 vFillColor;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

void main()
{
    vec3 worldPos = aCenter + (aRadius + aThickness / 2.f) * aLocalUV;
    gl_Position = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    vClipCenter = u_Proj * u_View * u_Model * vec4(aCenter, 1.0);

    vec3 edgeWorld = aCenter + vec3(aRadius, 0.0, 0.0);
    vClipEdge   = u_Proj * u_View * u_Model * vec4(edgeWorld, 1.0);

    vSize = aSize;
    vThickness = aThickness;
    vThinkessColor = aThinknessColor;
    vFillColor = aFillColor;
}