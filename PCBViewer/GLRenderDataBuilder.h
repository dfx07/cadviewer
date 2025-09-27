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

#include <memory>
#include <vector>

#include "RenderDataBuilder.h"
#include "PCBViewType.h"
#include "GLRenderData.h"


class GLRenderDataBuilder : public RenderDataBuilder
{
private:
	RectVertexData Build(RectDrawObject* pObject);

public:
	float NextZ();

public:
	virtual RenderDataPtr Make(PolyDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, PolyDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(LineDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, LineDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(CircleDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, CircleDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(RectDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, RectDrawObjectList* pDrawObject);

private:
	int m_nPolyIndex = 0;
	float m_fCurrentZ = 10.f;
	float m_fZStep = 0.01f;
};