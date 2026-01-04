#pragma once

#include "ResourceDef.h"

class FontLoader
{
public:
	FontLoader() = default;
	virtual ~FontLoader() = default;

public:
	virtual std::unique_ptr<IFont> Load(const wchar_t* path) = 0;
	virtual std::unique_ptr<IFont> Load(const char* path) = 0;
	virtual void UnLoad(IFont* pFont) = 0;
};

class FreeTypeFontLoader : public FontLoader
{
public:
	FreeTypeFontLoader();
	virtual ~FreeTypeFontLoader();

public:
	virtual std::unique_ptr<IFont> Load(const wchar_t* path) override;
	virtual std::unique_ptr<IFont> Load(const char* path) override;
	virtual void UnLoad(IFont* pFont) override;
};

class FontLoaderFactory
{
public:
	FontLoader* GetLoader(const AssetOption* pOption);
public:
	std::unique_ptr<FreeTypeFontLoader> pFreeTypeLoader{ nullptr };
};
