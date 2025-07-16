#version 150 core

in vec4 fColor;

in vec2 vLoc;
flat in vec2 vSLine;
flat in vec2 vELine;
flat in float fThickness;
flat in int fblur;

flat in int nStartJoin;
flat in int nEndJoin;
flat in int nIsAxis;

out vec4 FragColor;

float distance_to_segment(vec2 P, vec2 A, vec2 B)
{
   vec2 g = B - A;
   vec2 h = P - A;
   float d = length(h - g * clamp(dot(g, h) / dot(g,g), 0.0, 1.0));
   return d;
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
    float alpha;

    if(nIsAxis == 1)
    {
        float dist = distance_to_line(vLoc, vSLine, vELine);

        if(dist <= halfthickness - 0.01)
            alpha = 1.f;
        else if(dist >= halfthickness + 0.01)
            alpha = 0.f;
        else 
        {
            float side = signed_distance_to_line(vLoc, vSLine, vELine);
            if(side >= 0.0)
                alpha = 1.f;
            else
                alpha = 0.f;
        }
    }
    else 
    {
        float dist = distance_to_segment(vLoc, vSLine, vELine);

        if(fblur == 1)
        {
            float aa = max(fwidth(dist), 1.0);
            alpha = smoothstep(halfthickness + aa + 0.01, halfthickness - aa - 0.06, dist) - 0.05;
        }
        else
        {
            if(dist <= halfthickness)
                alpha = 0.5f;
            else 
                alpha = 0.f;
        }
    }

    FragColor = vec4(1.0, 0.0, 0.0, alpha);
}