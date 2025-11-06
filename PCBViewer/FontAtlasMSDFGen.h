////////////////////////////////////////////////////////////////////////////////////
/*!*********************************************************************************
*         Copyright (C) 2023-202x thuong.nv <thuong.nv.mta@gmail.com>               
*                   MIT software Licencs, see the accompanying                      
************************************************************************************
* @brief : Interface define free type font for rendering
* @file  : FontAtlasMSDFGen.h
* @create: oct 12, 2025
* @note  : For conditions of distribution and use, see copyright notice in readme.txt
************************************************************************************/
#ifndef FONTATLASMSDFGEN_H
#define FONTATLASMSDFGEN_H

#include "graphics/fonts/xfontatlas.h"

#include <unordered_map>
#include <vector>


#define ATLAS_MSD_WIDTH_DF 1024
#define ATLAS_MSD_HEIGHT_DF 1024

class GlyphMSDF : public GlyphBase
{

};

class FontAtlasMSDFGen : public IFontAtlas
{
public:
	FontAtlasMSDFGen(const int w = ATLAS_MSD_WIDTH_DF, const int h = ATLAS_MSD_HEIGHT_DF);
	virtual ~FontAtlasMSDFGen();

public:
	virtual bool BuildFromFont(const IFont* font, int pixelHeight) override;
	virtual int GetAtlasWidth() const override;
	virtual int GetAtlasHeight() const override;

	virtual const GlyphBase* GetGlyph(uint32_t codepoint) const override;
	virtual const float* GetBuffer() const;

private:
	uint32_t m_textureID = 0;
	int m_width = 1024;
	int m_height = 1024;
	float m_lineHeight = 0.f;
	double m_range{ 4.0 };
	double m_scale{ 1.0 };
	std::unordered_map<uint32_t, GlyphMSDF> m_glyphs;
	std::vector<float> m_buffer;
};

#endif // !FONTATLASMSDFGEN_H