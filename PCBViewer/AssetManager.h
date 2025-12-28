#pragma once

#include <unordered_map>

#include "ResourceDef.h"

class AssetDatabase
{
public:
	void AddAsset(const AssetMeta& meta) {
		m_map[meta.guid] = meta;
	}

	const AssetMeta* GetMeta(const std::string& guid) const {
		auto it = m_map.find(guid);
		return it != m_map.end() ? &it->second : nullptr;
	}

private:
	std::unordered_map<std::string, AssetMeta> m_map;
};

class AssetManager
{
public:
	AssetManager();
	~AssetManager();

private:
	IResource* GetResourceInternal(const std::string& guid);

public:
	template<typename T>
	T* GetResource(const std::string& guid)
	{
		auto pResource = GetResourceInternal(guid);

		return dynamic_cast<T*>(pResource);
	}

public:
	std::unique_ptr<AssetDatabase> m_data_meta{ nullptr };
	std::unique_ptr<FontManager> m_font_manager{ nullptr };

public:
	std::unordered_map<std::string, std::unique_ptr<IResource>> m_data_resources;
	std::unordered_map<AssetType, std::unique_ptr<IResourceLoader>> m_asset_loaders;
};
