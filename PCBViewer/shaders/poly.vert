#version 150 core

in vec3 aPos;
in vec4 aColor;
in float aThickness;
in int aPolygonID;

uniform mat4 u_Model;
uniform mat4 u_View;
uniform mat4 u_Proj;

out vec4 vColor;
out float vThickness;
out int vPolygonID;
out vec3 vWorldPos;

void main() {
    gl_Position = u_Proj * u_View * u_Model * vec4(aPos, 1.0);

    vWorldPos = aPos; 
    vColor = aColor;
    vThickness = aThickness;
    vPolygonID = aPolygonID;
}