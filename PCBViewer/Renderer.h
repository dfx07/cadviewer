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
#pragma once

#include "graphics/rendering/xrendertype.h"
#include "graphics/rendering/xctx.h"
#include "PCBViewType.h"

class Renderer
{
public:
	Renderer() :
		m_pCamera(nullptr)
	{

	}

	Renderer(CameraPtr pCamera)
		: m_pCamera(pCamera)
	{

	}

	virtual ~Renderer()
	{

	}

	virtual void SetViewPort(const int x, const int y, const int width, const int height) = 0;
	virtual void SetClearColor(const float r, const float g, const float b, const float a) = 0;
	virtual void Render(std::vector<DrawObjectPtr>& model) = 0;
	virtual void Update(float deltaTime) = 0;

	virtual void SetContext(DeviceContextPtr pContext)
	{
		m_pContext = pContext;
	}

	virtual void SetCamera(CameraPtr pCamera)
	{
		m_pCamera = pCamera;
	}

protected:
	DeviceContextPtr m_pContext = nullptr;
	CameraPtr		 m_pCamera = nullptr;
};