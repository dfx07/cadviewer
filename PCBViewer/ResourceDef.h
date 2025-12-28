#pragma once

#include "core/macros.h"
#include <memory>
#include <string>

#include "core/tfx_type_traits.h"

_interface IFont;
typedef std::shared_ptr<IFont> FontPtr;

_interface IFontAtlas;
typedef std::shared_ptr<IFontAtlas> FontAtlasPtr;

class FontManager; // string + font
class FontAtlasManager; // string + font-atlas
class AssetManager;

enum AssetType
{
	Shader,
	Image,
	Font,
};

class AssetOption
{
public:
	virtual ~AssetOption() = default;
};

class AssetMeta
{
public:
	std::string guid;
	std::string path;
	AssetType type;
	AssetOption* option{ nullptr };
};

_interface IResource
{
public:
	virtual ~IResource() = default;
};

_interface IResourceLoader
{
public:
	virtual ~IResourceLoader() = default;

public:
	virtual std::unique_ptr<IResource> Load(const AssetMeta& meta) = 0;
};