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
    // float halfthickness = fThickness * 0.5;

    // float alpha;

    // // if(is_cardinal_direction(vSLine - vELine))
    // // {
    // //     float dist = distance_to_segment(vLoc, vSLine, vELine);
    // //     float aa = max(fwidth(dist), 1.0);

    // //     alpha = smoothstep(halfthickness + aa * 0.5, halfthickness - aa * 0.5, dist);

    // //     if(alpha < 0.49) alpha = 0.07f;
    // //     else if( alpha > 0.51) alpha = 1.f;
    // //     else 
    // //     {
    // //         float side = signed_distance_to_line(vLoc, vSLine, vELine);

    // //         if(side >= 0.0)
    // //             alpha = 1.f;
    // //         else
    // //             alpha = 0.07f;
    // //     }
    // // }
    // // else
    // {
    //     float dist = distance_to_segment(vLoc, vSLine, vELine);
    //     float aa = max(fwidth(dist), 1.0);

    //     // alpha = smoothstep(halfthickness + aa - 0.1, halfthickness - aa - 0.5, dist);
    //     alpha = smoothstep(halfthickness + aa - 0.3, halfthickness - aa - 0.1, dist);
    //     // alpha = smoothstep(halfthickness + aa, halfthickness - aa, dist);

    //     // if(alpha >= 0.3 && alpha < 0.5) alpha = 0.5f;
    //     // if(alpha >= 0.5) alpha = 0.7f;

    //     // halfthickness = halfthickness - 0.4;

    //     // aa = max(fwidth(dist), 0.3f);  // thường rất nhỏ, cỡ 1 pixel hoặc thấp hơn

    //     // // Mịn đều từ rìa ngoài vào trong (0 → 1)
    //     // alpha = clamp(smoothstep(halfthickness + aa, halfthickness - aa - 0.2, dist), 0.f, 1.f);

    //     // // Có thể tăng độ mượt ở viền bằng cách nâng gamma:
    //     // alpha = pow(alpha, 1.0/2.4); // gamma correct (optional)
    // }

    // FragColor = vec4(fColor.rgb, alpha);

    // if(is_cardinal_direction(vSLine - vELine))
    // {
    //     FragColor = vec4(fColor.rgb, 0.5);
    // }
    // else 
    {
        FragColor = vec4(vec3(0.f), 0.5);
    }

}