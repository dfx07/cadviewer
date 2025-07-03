////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render engine for OpenGL
* @file  : GLRenderEngine.h
* @create: July 03, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "RenderEngine.h"

class GLRenderEngine : public RenderEngine
{
public:
	GLRenderEngine();
	~GLRenderEngine();

public:
	virtual void DrawRenderData(RenderDataPtr pRenderData, const Mat4& transform = Mat4(1.f));
};

