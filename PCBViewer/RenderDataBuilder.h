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


#include "PCBViewType.h"


class RenderData
{
public:
	virtual ~RenderData()
	{

	}
};

class IRenderDataBuilder
{
public:
	virtual RenderDataPtr Make(const PolyDrawObjectList* model) = 0;
};