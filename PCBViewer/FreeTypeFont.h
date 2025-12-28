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

#include "freetype/ft2build.h"
#include FT_FREETYPE_H

#include "Font.h"


class FreeTypeFont : public IFont
{
public:
	FreeTypeFont();
	virtual ~FreeTypeFont();

public:
	virtual std::string GetGUID() override;
	virtual void Release() override;

public:
	virtual FT_Face GetHandle() const;
	void SetHandle(FT_Face face);

protected:
	FT_Face m_face;
	std::string m_strGUID;
};

#endif // !FREETYPEFONT_H