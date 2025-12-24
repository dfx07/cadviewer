#pragma once

#include "ResourceDef.h"

class FontLoader
{
public:
	FontLoader() = default;
	virtual ~FontLoader() = default;

public:
	virtual std::unique_ptr<IFont> Load(const wchar_t* path) = 0;
	virtual void UnLoad(IFont* pFont) = 0;
};

class FreeTypeFontLoader : public FontLoader
{
public:
	FreeTypeFontLoader();
	virtual ~FreeTypeFontLoader();

public:
	virtual std::unique_ptr<IFont> Load(const wchar_t* path) override;
	virtual void UnLoad(IFont* pFont) override;
};

