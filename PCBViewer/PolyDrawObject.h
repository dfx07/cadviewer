#pragma once

#include "DrawObject.h"
#include <vector>


class PolyDrawObject : public DrawObject
{
public:
	PolyDrawObject();
	virtual ~PolyDrawObject() = default;

public:
	virtual void Remake()
	{
		// Implement the logic to remake the poly draw object
	}

public:
	std::vector<Vec2> m_vecPoints; // Points defining the polygon
	Col4 m_clColor; // Colors for each point
	float m_fThickness{ 1.0f }; // Thickness of the polygon lines
};