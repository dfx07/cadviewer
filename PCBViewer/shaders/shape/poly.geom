#version 150 core

layout(lines_adjacency) in;
layout(triangle_strip, max_vertices = 4) out;

uniform mat4 uMVP;

in vec4 vColor[];     // from vertex shader
in float vThickness[]; // from vertex shader

out vec4 fColor;

void main() {
    vec2 p0 = gl_in[0].gl_Position.xy;
    vec2 p1 = gl_in[1].gl_Position.xy;
    vec2 p2 = gl_in[2].gl_Position.xy;
    vec2 p3 = gl_in[3].gl_Position.xy;

    vec2 dir = normalize(p2 - p1);
    vec2 normal = vec2(-dir.y, dir.x);

    float thickness = (vThickness[1] + vThickness[2]) * 0.5;
    vec2 offset = normal * (thickness * 0.5);

    vec4 pos1 = vec4(p1 + offset, gl_in[1].gl_Position.zw);
    vec4 pos2 = vec4(p1 - offset, gl_in[1].gl_Position.zw);
    vec4 pos3 = vec4(p2 + offset, gl_in[2].gl_Position.zw);
    vec4 pos4 = vec4(p2 - offset, gl_in[2].gl_Position.zw);

    fColor = vColor[1]; gl_Position = pos1; EmitVertex();
    fColor = vColor[1]; gl_Position = pos2; EmitVertex();
    fColor = vColor[2]; gl_Position = pos3; EmitVertex();
    fColor = vColor[2]; gl_Position = pos4; EmitVertex();
    EndPrimitive();
}