#pragma once

#include "ResourceDef.h"

class FontResourceLoader : public IResourceLoader
{
public:
	FontResourceLoader();
	virtual ~FontResourceLoader();

public:
	virtual std::unique_ptr<IResource> Load(const AssetMeta& meta);
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
