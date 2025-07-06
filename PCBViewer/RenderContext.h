////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Class include render context
* @file  : RenderContext
* @create: July 02, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "RenderDef.h"

class RenderContext
{
public:
	Mat4 m_View;
	Mat4 m_Proj;
	Vec2 m_ViewPort;
	float m_Zoom;
};
