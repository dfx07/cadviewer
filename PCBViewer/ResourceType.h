#pragma once

#include "ResourceDef.h"

enum FontLoadType
{
	FreeType,
};

class FontAssetOption : public AssetOption
{
public:
	FontLoadType m_loadType{ FontLoadType::FreeType };
};

class FontResource : public IResource
{
public:

public:
	void Set(IFont* font)
	{
		m_pFont = font;
	}

	IFont* Get() const {
		return m_pFont;
	}

protected:
	IFont* m_pFont;
};

class ImageResource : public IResource
{
public:
	void Set(const char* data, const int width,  const int height)
	{
		m_data = data;

		m_nWidth = width;
		m_nHeight = height;
	}

protected:
	std::string m_data;
	int m_nWidth;
	int m_nHeight;
};