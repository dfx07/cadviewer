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
#include <vector>

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
	enum Flags
	{
		UpdateVertex = 1 << 0,
		UpdateIndex  = 1 << 1,
	};

public:
	GLPolyRenderData();
	virtual ~GLPolyRenderData();

public:
	virtual bool CreateBuffers();
	virtual void UpdateVertexBuffer();
	virtual void ReleaseBuffer();

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
	float m_fCurrentZ = 0.f;
	float m_fZStep = 0.01f;

public:
	float NextZ();

public:
	virtual RenderDataPtr Make(const PolyDrawObjectList* model);
};