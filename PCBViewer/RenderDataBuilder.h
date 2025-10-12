////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Build object data to use on Render engine
* @file  : RenderDataBuilder
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "RenderDef.h"
#include "PCBViewType.h"

class RenderDataBuilder
{
public:
	virtual RenderDataPtr Make(PolyDrawObjectList* pDrawObject) = 0;
	virtual bool Update(RenderDataPtr pRenderData, PolyDrawObjectList* pDrawObject) = 0;

	virtual RenderDataPtr Make(LineDrawObjectList* pDrawObject) = 0;
	virtual bool Update(RenderDataPtr pRenderData, LineDrawObjectList* pDrawObject) = 0;

	virtual RenderDataPtr Make(CircleDrawObjectList* pDrawObject) = 0;
	virtual bool Update(RenderDataPtr pRenderData, CircleDrawObjectList* pDrawObject) = 0;

	virtual RenderDataPtr Make(RectDrawObjectList* pDrawObject) = 0;
	virtual bool Update(RenderDataPtr pRenderData, RectDrawObjectList* pDrawObject) = 0;

	virtual RenderDataPtr Make(TriangleDrawObjectList* pDrawObject) = 0;
	virtual bool Update(RenderDataPtr pRenderData, TriangleDrawObjectList* pDrawObject) = 0;

	virtual RenderDataPtr Make(TextDrawObjectList* pDrawObject) = 0;
	virtual bool Update(RenderDataPtr pRenderData, TextDrawObjectList* pDrawObject) = 0;
};