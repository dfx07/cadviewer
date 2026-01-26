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
#include <map>

#include "RenderDataBuilder.h"
#include "PCBViewType.h"
#include "GLRenderData.h"

class GLRenderDataBuilder : public RenderDataBuilder,
	public std::enable_shared_from_this<GLRenderDataBuilder>
{
public:
	GLRenderDataBuilder(RenderAsset* pRenderAsset = nullptr, AssetManager* pAssetManager = nullptr);
	~GLRenderDataBuilder();

private:
	RectVertexData Build(RectDrawObject* pObject);

public:
	float NextZ();

public:
	void SetRenderAsset(RenderAsset* pRenderAsset);

public:
	virtual RenderDataPtr Make(PolyDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, PolyDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(LineDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, LineDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(CircleDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, CircleDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(RectDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, RectDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(TriangleDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, TriangleDrawObjectList* pDrawObject);

	virtual RenderDataPtr Make(TextDrawObjectList* pDrawObject);
	virtual bool Update(RenderDataPtr pRenderData, TextDrawObjectList* pDrawObject);

	virtual RenderDataPtr MakeTextBitmap(TextDrawObject* pDrawObject);
	virtual bool UpdateTextBitmap(RenderDataPtr pRenderData, TextDrawObject* pDrawObject);

	virtual RenderDataPtr MakeTextSdf(TextDrawObject* pDrawObject);
	virtual bool UpdateTextSdf(RenderDataPtr pRenderData, TextDrawObject* pDrawObject);

private:
	int m_nPolyIndex = 0;
	float m_fCurrentZ = 10.f;
	float m_fZStep = 0.01f;

protected:

	RenderAsset*  m_pRenderAsset;

	AssetManager* m_pAssetManager{ nullptr };
};