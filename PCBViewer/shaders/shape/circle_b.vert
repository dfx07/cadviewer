#version 330 core

layout(location = 0) in vec2 aLocalUV;
layout(location = 1) in vec3 aCenter;
layout(location = 2) in float aRadius;
layout(location = 3) in float aThickness;
layout(location = 4) in vec4 aBorderColor;
layout(location = 5) in vec4 aFillColor;

out vec2 vLocalUV;
flat out vec4 vCenterClipPos;
out float vRadius;
out float vThickness;
out vec4 vBorderColor;
out vec4 vFillColor;

flat out vec4 vClipCenter;
flat out vec4 vClipEdge;


uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;
uniform vec2 u_Viewport;

void main()
{
    vec3 worldPos = aCenter + vec3((aRadius + aThickness / 2.f) * aLocalUV, aCenter.z);
    gl_Position = u_Proj * u_View * u_Model * vec4(worldPos, 1.0);

    vClipCenter = u_Proj * u_View * u_Model * vec4(aCenter, 1.0);

    vec3 edgeWorld = aCenter + vec3(aRadius, 0.0, 0.0);
    vClipEdge   = u_Proj * u_View * u_Model * vec4(edgeWorld, 1.0);

    vLocalUV = aLocalUV;
    vRadius = aRadius;
    vThickness = aThickness;
    vBorderColor = aBorderColor;
    vFillColor = aFillColor;
}