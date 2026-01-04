#pragma once

#include "AssetManager.h"
#include "FontManager.h"

#include "ResourceLoader.h"


AssetManager::AssetManager()
{
	m_data_meta = std::make_unique<AssetDatabase>();
	m_font_manager = std::make_unique<FontManager>();

	m_asset_loaders[AssetType::Image] = std::make_unique<ImageResourceLoader>();
	m_asset_loaders[AssetType::Font] = std::make_unique<FontResourceLoader>(m_font_manager.get());
	m_asset_loaders[AssetType::Shader] = std::make_unique<ShaderResourceLoader>();
}

AssetManager::~AssetManager()
{
	
}

IResource* AssetManager::GetResourceInternal(const std::string& guid)
{
	auto pMeta = m_data_meta->GetMeta(guid);
	if (pMeta == nullptr)
		return nullptr;

	auto itResource = m_data_resources.find(guid);
	if (itResource != m_data_resources.end())
		return itResource->second.get();

	auto itLoader = m_asset_loaders.find(pMeta->type);
	if (itLoader == m_asset_loaders.end())
		return nullptr;

	std::unique_ptr<IResource> resource = itLoader->second->Load(*pMeta);

	m_data_resources[guid] = std::move(resource);

	return m_data_resources[guid].get();
}

void AssetManager::ReloadResource(std::vector<std::string>* pvecmsg_error)
{
	m_data_meta->ForEach([&] (const std::string& guid, const AssetMeta& meta)
	{
		auto itLoader = m_asset_loaders.find(meta.type);
		if (itLoader == m_asset_loaders.end())
			return;

		std::unique_ptr<IResource> resource = itLoader->second->Load(meta);

		m_data_resources[guid] = std::move(resource);
	});
}
