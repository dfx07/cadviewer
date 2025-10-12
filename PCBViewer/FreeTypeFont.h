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

#include "graphics/fonts/xfont.h"

#include "freetype/ft2build.h"
#include FT_FREETYPE_H

class FreeTypeFont : IFont
{
public:
	FreeTypeFont();
	virtual ~FreeTypeFont();

public:
	virtual bool Load(const char* font_path);
	virtual void Unload();

public:
	virtual FT_Face GetHandle() const;

protected:
	FT_Face m_face;
};

#endif // !FREETYPEFONT_H