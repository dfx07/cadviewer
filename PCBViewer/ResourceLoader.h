#pragma once

#include "ResourceDef.h"

class FontFactory;

class FontResourceLoader : public IResourceLoader
{
public:
	FontResourceLoader(FontManager* pFontManager);
	virtual ~FontResourceLoader();

public:
	virtual std::unique_ptr<IResource> Load(const AssetMeta& meta);

protected:
	FontFactory* m_pFactory;
	FontManager* m_pFontManager{ nullptr };
};

class ImageResourceLoader : public IResourceLoader
{
public:
	ImageResourceLoader();
	virtual ~ImageResourceLoader();

public:
	virtual std::unique_ptr<IResource> Load(const AssetMeta& meta);
};

class ShaderResourceLoader : public IResourceLoader
{
public:
	ShaderResourceLoader();
	virtual ~ShaderResourceLoader();

public:
	virtual std::unique_ptr<IResource> Load(const AssetMeta& meta);
};
