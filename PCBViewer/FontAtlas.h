////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Interface font atlas
* @file  : IFontAtlas.h
* @create: Nov 16, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "ResourceDef.h"

struct GlyphBase
{
	uint32_t codepoint;
	float advanceX;
	float bearingX, bearingY;
	float width, height;
	float u0, v0, u1, v1;
	virtual ~GlyphBase() = default;
};

_interface IFontAtlas
{
public:
	virtual ~IFontAtlas() = default;

public:
	virtual bool BuildFromFont(const IFont* font, int pixelHeight) = 0;
	virtual int GetAtlasWidth() const = 0;
	virtual int GetAtlasHeight() const = 0;

	virtual const GlyphBase* GetGlyph(uint32_t codepoint) const = 0;
};