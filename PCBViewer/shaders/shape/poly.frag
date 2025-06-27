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
    float dist = distance_to_segment(vLoc, vSLine, vELine);
    float alpha;

    if(fThickness == 1.f)
    {
        float aa = max(fwidth(dist), 1.0);
        alpha = smoothstep(0.501, 0.5, dist);
    }
    else
    {
        float _thickness = fThickness * 0.5; // Adjusted thickness for non-cardinal lines
        float aa = max(fwidth(dist), 1.0);
        alpha = smoothstep(_thickness + aa, _thickness - aa, dist);
    }

    if (alpha < 0.01)
        discard;

    FragColor = vec4(fColor.rgb, alpha);
}