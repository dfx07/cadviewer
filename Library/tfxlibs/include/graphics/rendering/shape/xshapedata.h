////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define shape rendering
* @file  : xshapedata.h
* @create: June 19, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XSHAPEDATA_H
#define XSHAPEDATA_H

#include "common/tfxtype.h"
#include <vector>

__BEGIN_NAMESPACE__


struct Vertex
{
	Vec3 position;
	Color4 color;

public:
	Vertex() = default;
	Vertex(const glm::vec3 & p, const glm::vec4 & c)
		: position(p), color(c) {}

};

class PolyShapeDrawData
{
public:
	std::vector<Vertex> m_vertices;
	float m_thickness = 1.f;

public:
	PolyShapeDrawData() = default;

	void AddVertex(const glm::vec3 & pos, const glm::vec4 & col)
	{
		m_vertices.emplace_back(pos, col);
	}

	size_t GetVertexCount() const { return m_vertices.size(); }
};


__END_NAMESPACE__

#endif // !XSHAPEDATA_H