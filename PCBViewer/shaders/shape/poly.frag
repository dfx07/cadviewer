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

float signed_distance_to_line(vec2 P, vec2 A, vec2 B)
{
    vec2 dir = normalize(B - A);
    vec2 normal = vec2(-dir.y, dir.x);
    return dot(P - A, normal);
}

float distance_to_line(vec2 P, vec2 A, vec2 B)
{
    return abs(signed_distance_to_line(P, A, B));
}

void main()
{
    float halfthickness = fThickness * 0.5;
    float dist = distance_to_line(vLoc, vSLine, vELine);
    float aa = max(fwidth(dist), 1.0);
    float alpha;

    if(is_cardinal_direction(vSLine - vELine))
    {
        alpha = smoothstep(halfthickness + aa * 0.5, halfthickness - aa * 0.5, dist);

        if(alpha < 0.49) alpha = 0.07f;
        else if( alpha > 0.51) alpha = 1.f;
        else 
        {
            float side = signed_distance_to_line(vLoc, vSLine, vELine);

            if(side >= 0.0)
                alpha = 1.f;
            else
                alpha = 0.07f;
        }
    }
    else
    {
        alpha = smoothstep(halfthickness + aa - 0.3, halfthickness - aa, dist);
    }

    FragColor = vec4(fColor.rgb, alpha);
}