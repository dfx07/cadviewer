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
private:
	IResource* GetResourceInternal(const std::string& guid)
	{
		auto pMeta = m_data_meta.GetMeta(guid);
		if (pMeta == nullptr)
			return nullptr;

		auto itResource = m_data_resources.find(guid);
		if (itResource != m_data_resources.end())
			return itResource->second.get();

		auto itLoader = m_asset_loaders.find(pMeta->type);
		if (itLoader == m_asset_loaders.end())
			return nullptr;

		std::unique_ptr<IResource> resource = itLoader->second->Load(*pMeta);

		IResource* pResource = resource.get();
		m_data_resources[guid] = std::move(resource);
		return pResource;
	}
public:
	template<typename T>
	T* GetResource(const std::string& guid)
	{
		auto pResource = GetResourceInternal(guid);

		return dynamic_cast<T*>(pResource);
	}

public:
	AssetDatabase m_data_meta;

public:
	std::unordered_map<std::string, std::unique_ptr<IResource>> m_data_resources;
	std::unordered_map<AssetType, std::unique_ptr<IResourceLoader>> m_asset_loaders;
};
