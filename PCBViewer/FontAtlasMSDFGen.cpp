#include "FontAtlasMSDFGen.h"
#include "FreeTypeFont.h"

#include "freetype/ft2build.h"
#include FT_FREETYPE_H

#undef min
#undef max

#include "msdfgen/msdfgen.h"
#include "msdfgen/msdfgen-ext.h"

FontAtlasMSDFGen::FontAtlasMSDFGen(const int w /*= ATLAS_MSD_WIDTH_DF*/, const int h /*= ATLAS_MSD_HEIGHT_DF*/) :
	m_width(w),
	m_height(h)
{

}

FontAtlasMSDFGen::~FontAtlasMSDFGen()
{
	//msdfgen::destroyFont(font);
}

bool FontAtlasMSDFGen::BuildFromFont(const IFont* font, int pixelHeight)
{
	auto pFTFont = dynamic_cast<const FreeTypeFont*>(font);
	if (pFTFont == nullptr)
	{
		return false;
	}

	FT_Face face = pFTFont->GetHandle();

	msdfgen::FontHandle* fontHandle = msdfgen::adoptFreetypeFont(face);
	if (!fontHandle)
	{
		printf("Failed to load font\n");
		return false;
	}

	FT_UInt glyphIndex;
	FT_ULong charCode = FT_Get_First_Char(face, &glyphIndex);

	const int padding = 4;

	int penX = padding, penY = padding;
	int rowHeight = 0;

	const int atlasWidth = m_width;
	const int atlasHeight = m_height;

	const int nImgSize = atlasWidth * atlasHeight;

	msdfgen::Bitmap<float, 3> atlas_msdf(atlasWidth, atlasHeight);

	m_buffer.resize(nImgSize);

	int count = 0;
	while (glyphIndex != 0)
	{
		count++;

		msdfgen::Shape shape;
		if (msdfgen::loadGlyph(shape, fontHandle, glyphIndex))
		{
			shape.normalize();

			// Kích thước MSDF
			const int glyphW = 64, glyphH = 64;
			msdfgen::Vector2 translate(0.0, 0.0);

			msdfgen::Bitmap<float, 3> msdf(glyphW, glyphH);
			generateMSDF(msdf, shape, m_range, m_scale, translate);

			if (penX + glyphW + padding > atlasWidth)
			{
				penX = padding;
				penY += rowHeight + padding;
				rowHeight = 0;
			}

			if (penY + glyphH + padding > atlasHeight)
				break;

			rowHeight = std::max(rowHeight, glyphH);

			//for (int y = 0; y < glyphH; ++y)
			//{
			//	for (int x = 0; x < glyphW; ++x)
			//	{
			//		const auto& pix = msdf(x, y);
			//		atlas_msdf(penX + x, penY + y)[0] = pix[0];
			//		atlas_msdf(penX + x, penY + y)[1] = pix[1];
			//		atlas_msdf(penX + x, penY + y)[2] = pix[2];
			//	}
			//}

			for (int y = 0; y < glyphH; ++y)
			{
				float* dst = atlas_msdf(penX, penY + y);
				const float* src = msdf(0, penY - padding + y);
				memcpy(dst, src, sizeof(float) * glyphW * 3);
			}

			// Get the metrics from FreeType
			FT_Load_Glyph(face, glyphIndex, FT_LOAD_DEFAULT);
			float advanceX = (float)(face->glyph->advance.x >> 6);
			float offsetX = (float)(face->glyph->bitmap_left);
			float offsetY = (float)(face->glyph->bitmap_top);

			// Create GlyphInfo
			GlyphMSDF g{};
			g.codepoint = charCode;
			g.advanceX = advanceX;
			g.offsetX = offsetX;
			g.offsetY = offsetY;
			g.width = (float)glyphW;
			g.height = (float)glyphH;
			g.u0 = (float)(penX + padding) / atlasWidth;
			g.v0 = (float)(penY + padding) / atlasHeight;
			g.u1 = (float)(penX + glyphW - padding) / atlasWidth;
			g.v1 = (float)(penY + glyphH - padding) / atlasHeight;

			m_glyphs[charCode] = g;

			penX += glyphW + padding;
		}

		charCode = FT_Get_Next_Char(face, charCode, &glyphIndex);
	}

	std::memcpy(m_buffer.data(), (float*)atlas_msdf, nImgSize);

	msdfgen::destroyFont(fontHandle);

	return true;
}

int FontAtlasMSDFGen::GetAtlasWidth() const
{
	return m_width;
}

int FontAtlasMSDFGen::GetAtlasHeight() const
{
	return m_height;
}

const GlyphBase* FontAtlasMSDFGen::GetGlyph(uint32_t codepoint) const
{
	auto itFound = m_glyphs.find(codepoint);

	if (itFound == m_glyphs.end())
		return nullptr;

	return &itFound->second;
}
