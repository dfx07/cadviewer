////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Interface font
* @file  : Font.h
* @create: Nov 16, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "core/macros.h"

#include <string>

_interface IFont
{
public:
	virtual ~IFont() = default;
public:
	virtual bool Load(const char* font_path) = 0;
	virtual void Unload() = 0;
	virtual std::string GetGUID() = 0;
};