#pragma once

#include <unordered_map>

#include "ResourceDef.h"

class AssetDatabase
{
public:
	void AddAsset(const AssetMeta& meta) {
		m_map[meta.guid] = meta;
	}

	void Remove(const std::string guid)
	{
		m_map.erase(guid);
	}

	const AssetMeta* GetMeta(const std::string& guid) const {
		auto it = m_map.find(guid);
		return it != m_map.end() ? &it->second : nullptr;
	}

	template<typename Func>
	void ForEach(Func&& fn) const
	{
		for (const auto& it : m_map)
		{
			fn(it.first, it.second);
		}
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

	void ReloadResource(std::vector<std::string>* pvecmsg_error);

public:
	FontManager* GetFontManager() const {
		return m_font_manager.get();
	}

public:
	std::unique_ptr<AssetDatabase> m_data_meta{ nullptr };
	std::unique_ptr<FontManager> m_font_manager{ nullptr };

public:
	std::unordered_map<std::string, std::unique_ptr<IResource>> m_data_resources;
	std::unordered_map<AssetType, std::unique_ptr<IResourceLoader>> m_asset_loaders;
};
