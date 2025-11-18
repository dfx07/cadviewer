////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render object
* @file  : GLRenderer.h
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef GLRENDERER_H
#define GLRENDERER_H

#include "RenderDef.h"
#include "PCBViewType.h"

#include "Renderer.h"

class GLRenderer : public Renderer
{
public:
	GLRenderer(CameraPtr pCamera);
	~GLRenderer();

public:
	virtual void SetViewPort(const int x, const int y, const int width, const int height) override;
	virtual void SetClearColor(const float r, const float g, const float b, const float a) override;
	virtual void Render(std::vector<DrawObjectPtr>& model) override;
	virtual void Update(float deltaTime) override;

protected:
	RenderCachePtr m_pRenderCache;
	RenderEnginePtr m_pRenderEngine;

	RenderDataBuilderPtr m_pRenderBuilder;

	RenderResourceManagerPtr m_pRenderResourceManager;

	Vec4 m_v4ClearColor;
	ViewPort m_viewPort;
};

#endif // !GLRENDERER_H
