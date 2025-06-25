#version 150 core


in vec4 fColor;
in vec2 vLoc;
flat in vec2 vSLine;
flat in vec2 vELine;
flat in float fThickness;

out vec4 FragColor;

float distance_to_segment(vec2 P, vec2 A, vec2 B)
{
   vec2 g = B - A;
   vec2 h = P - A;
   float d = length(h - g * clamp(dot(g, h) / dot(g,g), 0.0, 1.0));
   return d;
}

bool is_cardinal_direction(vec2 v)
{
    const float epsilon = 0.001;
    return abs(v.x) < epsilon || abs(v.y) < epsilon;
}

void main()
{
    if(fThickness == 1.f)
    {
        float dist = distance_to_segment(vLoc, vSLine, vELine);
        float aa = max(fwidth(dist), 1.0);
        float alpha = smoothstep(0.502, 0.5, dist);

        FragColor = vec4(fColor.rgb, alpha);
    }
    else
    {
        float _thickness = fThickness* 0.5; // Adjusted thickness for non-cardinal lines
        float dist = distance_to_segment(vLoc, vSLine, vELine);
        float aa = max(fwidth(dist), 1.0);
        float alpha = smoothstep(_thickness + aa, _thickness - aa, dist);

        FragColor = vec4(fColor.rgb, alpha);
    }
}