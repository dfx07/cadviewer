////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Contain render data cache
* @file  : RenderCache
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "RenderDef.h"

#include "PCBViewType.h"


class RenderCache
{
public:
	RenderCache(RenderDataBuilderPtr pBuilder);
	virtual ~RenderCache();

public:
	RenderDataPtr GetOrCreateRenderData(DrawObjectPtr pModel);
	void Invalidate(DrawObjectPtr model);

public:
	unordered_map_shared_ptr<DrawObject, RenderDataPtr> m_cache;

protected:
	RenderDataBuilderPtr m_builder;
};

