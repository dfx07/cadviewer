////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define free type font for rendering
* @file  : freetypefont.h
* @create: oct 06, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef FREETYPEFONT_H
#define FREETYPEFONT_H

#include "xfont.h"

class FreeTypeFont : IFont
{
public:
	FreeTypeFont();
	virtual ~FreeTypeFont() = default;

public:
	virtual bool Load(const char* font_path);
	virtual void Unload();
};

#endif // !FREETYPEFONT_H