////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render Engine
* @file  : RenderEngine
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "RenderDef.h"

class RenderEngine
{
public:
	virtual ~RenderEngine() = default;

public:
	virtual void DrawRenderData(RenderDataPtr pRenderData, RenderContextPtr pContext, MaterialComponentPtr pMaterial, TransformComponentPtr pTranform) = 0;
};

