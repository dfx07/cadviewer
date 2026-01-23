////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>
*                   MIT software Licencs, see the accompanying
************************************************************************************
* @brief : Render asset
* @file  : RenderAssset.h
* @create: Nov 16, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#pragma once

#include "FontManager.h"

class RenderAsset
{
public:
	RenderAsset();
	~RenderAsset();

public:
	const FontAtlasManager* GetFontAtlasMana() const {
		return &m_FontAtlasManager;
	}

	FontAtlasManager* GetFontAtlasMana() {
		return &m_FontAtlasManager;
	}

private:
	FontAtlasManager m_FontAtlasManager;
};