////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define font for rendering
* @file  : xfont.h
* @create: oct 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef XFONT_H
#define XFONT_H

#include <memory>
#include "common/tfxtype.h"

_interface IFont
{
public:
	virtual ~IFont() = default;
public:
	virtual bool Load(const char* font_path) = 0;
	virtual void Unload() = 0;
};

#endif // !XFONT_H