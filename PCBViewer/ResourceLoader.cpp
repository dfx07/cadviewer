#pragma once

#include "ResourceType.h"
#include "ResourceLoader.h"
#include "FontManager.h"
#include "FontLoader.h"

FontResourceLoader::FontResourceLoader(FontManager* pFontManager):
	m_pFontManager(pFontManager)
{
	m_pFactory = new FontFactory();
}

FontResourceLoader::~FontResourceLoader()
{
	SAFE_DELETE(m_pFactory);
}

std::unique_ptr<IResource> FontResourceLoader::Load(const AssetMeta& meta)
{
	std::unique_ptr<FontResource> pFontResouce = std::make_unique<FontResource>();

	auto pLoader = m_pFactory->GetLoader(meta.option);

	if (pLoader == nullptr)
		return nullptr;

	auto pFont = pLoader->Load(meta.path.c_str());

	if (!pFont)
		return nullptr;

	std::string key = pFont->GetGUID();

	m_pFontManager->Add(key, std::move(pFont));

	auto pFontRef = m_pFontManager->Get(key);

	if (pFontRef)
		pFontResouce->Set(pFontRef);

	return pFontResouce;
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
