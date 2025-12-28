#include "FontLoader.h"

#include <cassert>

#include "core/tfx_utils.h"
#include "ResourceType.h"
#include "FreeTypeFont.h"

class FreeTypeFontSystem
{
private:
	FreeTypeFontSystem()
	{
		FT_Error err = FT_Init_FreeType(&m_ftLib);
		if (err != 0)
		{
			assert(0);
			m_bInit = false;
			return;
		}

		m_bInit = true;
	}

	~FreeTypeFontSystem()
	{
		if (m_ftLib)
		{
			FT_Done_FreeType(m_ftLib);
			m_ftLib = nullptr;
		}
	}

private:
	FreeTypeFontSystem& operator=(const FreeTypeFontSystem&) = delete;
	FreeTypeFontSystem(const FreeTypeFontSystem&) = delete;

public:
	bool IsInit() const
	{
		return m_ftLib != nullptr;
	}

protected:
	FT_Library m_ftLib;
	bool m_bInit = false;

public:

	static FT_Library GetIntance()
	{
		static FreeTypeFontSystem ftLib;
		return ftLib.m_ftLib;
	}
};

#define FreeTypeSytem FreeTypeFontSystem::GetIntance()

FreeTypeFontLoader::FreeTypeFontLoader()
{
	assert(FreeTypeSytem);
}

FreeTypeFontLoader::~FreeTypeFontLoader()
{

}

std::unique_ptr<IFont> FreeTypeFontLoader::Load(const wchar_t* path)
{
	auto pFTFont = std::make_unique<FreeTypeFont>();

	std::string fileData = tfx::ReadFile(path);
	if (fileData.empty())
		return nullptr;

	FT_Face face{ nullptr };

	auto err = FT_New_Memory_Face(FreeTypeSytem, reinterpret_cast<const FT_Byte*>(fileData.data()),
		static_cast<FT_Long>(fileData.size()), 0, &face);

	if (err != FT_Err_Ok)
		return nullptr;

	pFTFont->SetHandle(face);

	return pFTFont;
}

std::unique_ptr<IFont> FreeTypeFontLoader::Load(const char* path)
{
	auto pFTFont = std::make_unique<FreeTypeFont>();

	FT_Face face{ nullptr };
	if (FT_New_Face(FreeTypeSytem, path, 0, &face))
	{
		return nullptr;
	}

	pFTFont->SetHandle(face);

	return pFTFont;
}

void FreeTypeFontLoader::UnLoad(IFont* pFont)
{
	if (pFont)
		pFont->Release();
}

FontLoader* FontFactory::GetLoader(const AssetOption* pOption)
{
	NULL_RETURN(pOption, nullptr);

	auto pFontAssetOption = dynamic_cast<const FontAssetOption*>(pOption);

	NULL_RETURN(pFontAssetOption, nullptr);

	switch (pFontAssetOption->m_loadType)
	{
	case FontLoadType::FreeType:
	{
		if (!pFreeTypeLoader)
			pFreeTypeLoader = std::make_unique<FreeTypeFontLoader>();

		if (pFreeTypeLoader)
			return pFreeTypeLoader.get();
		break;
	}

	default:
		break;
	};

	return nullptr;
}
