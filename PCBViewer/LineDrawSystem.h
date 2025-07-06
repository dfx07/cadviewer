////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Define interface for LINE
* @file  : LineDrawSystem.h
* @create: July 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "DrawSystem.h"


class LineDrawSystem : public DrawSystem
{
public:
	LineDrawSystem();
	~LineDrawSystem();

public:
	virtual void Draw(RenderDataPtr data, const DrawParams& params);
};