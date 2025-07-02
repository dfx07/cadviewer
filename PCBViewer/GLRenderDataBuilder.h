////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Build object data to use on Render OpenGL engine
* @file  : GLRenderDataBuilder
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/

#pragma once
#include "RenderDataBuilder.h"
#include <memory>

class GLPolyRenderData;
typedef std::shared_ptr<GLPolyRenderData> GLPolyRenderDataPtr;

struct PolyVertexData
{
	Vec3 position;
	Vec4 color;
	float thickness;
	int polygonID;
};

class GLPolyRenderData : public RenderData
{
public:
	GLPolyRenderData();
	virtual ~GLPolyRenderData();

public:
	unsigned int m_nVao = 0;
	unsigned int m_nVbo = 0;
	unsigned int m_nEbo = 0;

	std::vector<PolyVertexData> m_vecRenderData;
	std::vector<unsigned int> m_vecIndices;
};

class GLRenderDataBuilder : public IRenderDataBuilder
{
private:
	int m_nPolyIndex = 0;

public:
	virtual RenderDataPtr Make(const PolyDrawObjectList* model);
};