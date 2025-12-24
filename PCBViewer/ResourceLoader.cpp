#pragma once

#include "ResourceLoader.h"

FontResourceLoader::FontResourceLoader()
{
}

FontResourceLoader::~FontResourceLoader()
{
}

std::unique_ptr<IResource> FontResourceLoader::Load(const AssetMeta& meta)
{
	return std::unique_ptr<IResource>();
}

ImageResourceLoader::ImageResourceLoader()
{
}

ImageResourceLoader::~ImageResourceLoader()
{
}

std::unique_ptr<IResource> ImageResourceLoader::Load(const AssetMeta& meta)
{
	return std::unique_ptr<IResource>();
}

ShaderResourceLoader::ShaderResourceLoader()
{
}

ShaderResourceLoader::~ShaderResourceLoader()
{
}

std::unique_ptr<IResource> ShaderResourceLoader::Load(const AssetMeta& meta)
{
	return std::unique_ptr<IResource>();
}
